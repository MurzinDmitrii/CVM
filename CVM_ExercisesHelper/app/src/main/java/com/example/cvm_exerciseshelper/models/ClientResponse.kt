package com.example.cvm_exerciseshelper.models

import com.google.gson.annotations.SerializedName

class ClientResponse {
    @SerializedName("ClientId")
    var ClientId: Int = -1
    @SerializedName("ClientFullName")
    var ClientFullName: String = ""
    @SerializedName("CLientGender")
    var CLientGender: Boolean = false
    @SerializedName("Photo")
    var Photo: String = ""
}