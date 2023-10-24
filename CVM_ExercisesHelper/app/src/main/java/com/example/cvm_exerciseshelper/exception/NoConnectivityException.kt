package com.example.cvm_exerciseshelper.exception

import java.io.IOException

class NoConnectivityException : IOException() {
    override val message: String
        get() = "Нет соединения с интернетом"
}