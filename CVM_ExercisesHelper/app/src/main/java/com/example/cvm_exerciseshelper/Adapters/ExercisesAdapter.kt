package com.example.cvm_exerciseshelper.Adapters

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView
import com.example.cvm_exerciseshelper.R
import com.example.cvm_exerciseshelper.models.ExercisesListResponse

class ExercisesAdapter (context: Context, resource: Int, exercises: List<ExercisesListResponse>) :
    ArrayAdapter<ExercisesListResponse?>(context, resource, exercises) {
    private val inflater: LayoutInflater
    private val layout: Int
    private val exercises: List<ExercisesListResponse>

    init {
        this.exercises = exercises
        this.layout = resource
        this.inflater = LayoutInflater.from(context)
    }

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val view = inflater.inflate(layout, parent, false)
        val exercisesTextView = view.findViewById<TextView>(R.id.exercisesTextView)
        val quantityTextView = view.findViewById<TextView>(R.id.quantityTextView)
        val exercise: ExercisesListResponse = exercises[position]
        exercisesTextView.text = exercise.ExercisesName
        if(exercise.Quantity != -1){
            quantityTextView.text = exercise.Quantity.toString()
        }
        else{
            quantityTextView.text = ""
        }
        return view
    }
}