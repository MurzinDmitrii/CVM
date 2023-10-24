package com.example.cvm_exerciseshelper.api

import android.content.Context
import com.example.cvm_exerciseshelper.models.ClientResponse
import com.example.cvm_exerciseshelper.models.ExercisesListResponse
import io.reactivex.Observable
import okhttp3.OkHttpClient
import okhttp3.ResponseBody
import retrofit2.Retrofit
import retrofit2.adapter.rxjava2.RxJava2CallAdapterFactory
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.*

interface IApiClient {
    @GET("MobileClientViews")
    fun getClients(): Observable<List<ClientResponse>>

    @GET("MobileExercisesViews/{id}/")
    fun getExercisesLists(@Path("id") id: Int): Observable<List<ExercisesListResponse>>

    companion object {
        fun create(context: Context): IApiClient {
            val client: OkHttpClient = OkHttpClient.Builder()
                .addInterceptor(ApiInspector(context))
                .build()

            val retrofit = Retrofit.Builder()
                .addCallAdapterFactory(RxJava2CallAdapterFactory.create())
                .addConverterFactory(GsonConverterFactory.create())
                .client(client)
                .baseUrl("http://192.168.0.10:63193/api/")
                .build()

            return retrofit.create(IApiClient::class.java)
        }
    }
}