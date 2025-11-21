package com.example.labubu2in1.data.local

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "employees")
data class EmployeeEntity(
    @PrimaryKey(autoGenerate = true) val id: Int = 0,
    val lastName: String,
    val firstName: String,
    val middleName: String?,
    val position: String,
    val gender: String,
    val hireDate: String
)