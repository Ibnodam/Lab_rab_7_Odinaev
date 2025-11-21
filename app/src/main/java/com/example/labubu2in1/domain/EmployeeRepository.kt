package com.example.labubu2in1.domain

import com.example.labubu2in1.domain.model.Employee

interface EmployeeRepository {
    suspend fun getAll(): List<Employee>
    suspend fun insert(employee: Employee)
    suspend fun update(employee: Employee)
    suspend fun delete(employee: Employee)
}
