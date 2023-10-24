package com.example.cvm_exerciseshelper.models

import com.google.gson.annotations.SerializedName

class ExercisesListResponse {
    @SerializedName("ClientId")
    var ClientId: Int = -1
    @SerializedName("ExercisesName")
    var ExercisesName: String = ""
    @SerializedName("Quantity")
    var Quantity: Int = -1
}