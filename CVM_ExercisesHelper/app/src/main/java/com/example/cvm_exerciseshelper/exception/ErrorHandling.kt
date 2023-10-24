package com.example.cvm_exerciseshelper.exception

import retrofit2.HttpException

class ErrorHandling {
    companion object {
        fun requestError(error: Any): String {
            return when (error) {
                is NoConnectivityException -> {
                    error.message
                }
                is HttpException -> {
                    when (error.code()) {
                        403 -> "Это действие невозможно выполнить"
                        else -> "Неизвестная ошибка"
                    }
                }
                else -> {
                    "Произошла неизвестная ошибка"
                }
            }
        }
    }
}