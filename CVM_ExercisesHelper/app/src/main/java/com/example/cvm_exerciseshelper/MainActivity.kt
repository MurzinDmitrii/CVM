package com.example.cvm_exerciseshelper

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.view.KeyEvent
import android.view.View
import android.widget.EditText
import android.widget.ListView
import android.widget.Toast
import com.example.cvm_exerciseshelper.Adapters.ClientAdapter
import com.example.cvm_exerciseshelper.api.IApiClient
import com.example.cvm_exerciseshelper.exception.ErrorHandling
import com.example.cvm_exerciseshelper.models.ClientResponse
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlin.properties.Delegates

class MainActivity : AppCompatActivity() {
    private val client by lazy {
        IApiClient.create(this@MainActivity)
    }
    lateinit var clientAdapter: ClientAdapter
    var LoadEnd: Boolean = false
    private lateinit var clientsList: List<ClientResponse>
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        var findEditText = findViewById<EditText>(R.id.findEditText)
        var clientsListView = findViewById<ListView>(R.id.clientsListView)
        fun Loaded(){
            CoroutineScope(Dispatchers.IO).launch {
                client.getClients()
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe ({
                            result -> clientsList = result
                        clientsList = clientsList.sortedByDescending { it.ClientId }
                        clientAdapter = ClientAdapter(this@MainActivity, R.layout.client_list_item, clientsList)
                        clientsListView.setAdapter(clientAdapter)
                        LoadEnd = true
                    },
                        { error ->
                            val errorMessage = ErrorHandling.requestError(error)
                            Toast.makeText(this@MainActivity, errorMessage, Toast.LENGTH_LONG).show()
                        }
                    )
            }
        }
        Loaded()
        findEditText.setOnKeyListener(View.OnKeyListener { v, keyCode, event ->
            if (keyCode == KeyEvent.KEYCODE_ENTER && event.action == KeyEvent.ACTION_UP) {
                var newClientsList: List<ClientResponse> = clientsList
                lateinit var newClientsAdapter: ClientAdapter
                if(findEditText.text.isBlank() != true){
                    newClientsList = clientsList.filter{it.ClientFullName.contains(findEditText.text)
                    }
                    newClientsAdapter = ClientAdapter(this@MainActivity, R.layout.client_list_item, newClientsList)
                }
                if(newClientsList.count() == 0){
                    val client: ClientResponse = ClientResponse();
                    client.ClientFullName = "Результатов нет"
                    client.Photo = " "
                    var newClientList: ArrayList<ClientResponse> = ArrayList<ClientResponse>()
                    newClientList.add(client)
                    newClientsAdapter = ClientAdapter(this@MainActivity, R.layout.client_list_item, newClientsList)
                }
                if(findEditText.text.isBlank() == true){
                    newClientsAdapter = ClientAdapter(this@MainActivity, R.layout.client_list_item, clientsList)
                }
                clientsListView.setAdapter(newClientsAdapter)
                return@OnKeyListener true
            }
            false
        })
        clientsListView.setOnItemClickListener { parent, view, position, id ->
            val element = clientsListView.getItemAtPosition(position)
            val intent = Intent(this, ExercisesListActivity::class.java)
            val extraId = parent.getItemAtPosition(position) as ClientResponse
            intent.putExtra("ClientId", extraId.ClientId)
            startActivity(intent)
        }
    }
}