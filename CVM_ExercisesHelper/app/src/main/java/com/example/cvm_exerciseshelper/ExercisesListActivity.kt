package com.example.cvm_exerciseshelper

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ListView
import android.widget.Toast
import com.example.cvm_exerciseshelper.Adapters.ClientAdapter
import com.example.cvm_exerciseshelper.Adapters.ExercisesAdapter
import com.example.cvm_exerciseshelper.api.IApiClient
import com.example.cvm_exerciseshelper.exception.ErrorHandling
import com.example.cvm_exerciseshelper.models.ClientResponse
import com.example.cvm_exerciseshelper.models.ExercisesListResponse
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class ExercisesListActivity : AppCompatActivity() {
    private val client by lazy {
        IApiClient.create(this@ExercisesListActivity)
    }
    lateinit var exercisesAdapter: ExercisesAdapter
    private lateinit var exercisesList: List<ExercisesListResponse>
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_exercises_list)
        val exercisesListView = findViewById<ListView>(R.id.exercisesListView)
        val extraId: Int = intent.getIntExtra("ClientId", 0)
        CoroutineScope(Dispatchers.IO).launch {
            client.getExercisesLists(extraId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe({ result ->
                    exercisesList = result
                    exercisesAdapter = ExercisesAdapter(
                        this@ExercisesListActivity,
                        R.layout.exercises_list_item,
                        exercisesList
                    )
                    if(exercisesList.count() == 0){
                        val ex: ExercisesListResponse = ExercisesListResponse();
                        ex.ExercisesName = "Этому пациенту еще не назначили упражнения"
                        var newExercisesList: ArrayList<ExercisesListResponse> = ArrayList<ExercisesListResponse>()
                        newExercisesList.add(ex)
                        exercisesList = newExercisesList
                        exercisesAdapter = ExercisesAdapter(this@ExercisesListActivity, R.layout.exercises_list_item, exercisesList)
                    }
                    exercisesListView.setAdapter(exercisesAdapter)
                },
                    { error ->
                        val errorMessage = ErrorHandling.requestError(error)
                        Toast.makeText(this@ExercisesListActivity, errorMessage, Toast.LENGTH_LONG)
                            .show()
                    }
                )
        }
    }
}