package com.example.labubu2in1.domain.model

data class Employee(
    val id: Int = 0,
    val lastName: String,
    val firstName: String,
    val middleName: String?,
    val position: String,
    val gender: String,
    val hireDate: String // формат YYYY-MM-DD
)
