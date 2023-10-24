package com.example.cvm_exerciseshelper.Adapters

import android.content.Context
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.util.Base64
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ImageView
import android.widget.TextView
import com.example.cvm_exerciseshelper.R
import com.example.cvm_exerciseshelper.models.ClientResponse

class ClientAdapter (context: Context, resource: Int, clients: List<ClientResponse>) :
    ArrayAdapter<ClientResponse?>(context, resource, clients) {
    private val inflater: LayoutInflater
    private val layout: Int
    private val clients: List<ClientResponse>

    init {
        this.clients = clients
        this.layout = resource
        this.inflater = LayoutInflater.from(context)
    }
    @Throws(IllegalArgumentException::class)
    fun convert(base64Str: String): Bitmap? {
        val decodedBytes: ByteArray = Base64.decode(
            base64Str.substring(base64Str.indexOf(",") + 1),
            Base64.DEFAULT
        )
        return BitmapFactory.decodeByteArray(decodedBytes, 0, decodedBytes.size)
    }

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val view = inflater.inflate(layout, parent, false)
        val fullName = view.findViewById<TextView>(R.id.fullNameTextView)
        val gender = view.findViewById<TextView>(R.id.genderTextView)
        val photo = view.findViewById<ImageView>(R.id.imageView)
        val client: ClientResponse = clients[position]
        fullName.text = client.ClientFullName
        if(client.CLientGender == false){
            gender.text = "мужской"
        }
        else{
            gender.text = "женский"
        }
        photo.setImageBitmap(convert(client!!.Photo)) // Мб стоит поменять
        return view
    }
}