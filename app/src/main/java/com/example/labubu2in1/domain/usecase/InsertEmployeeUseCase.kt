package com.example.labubu2in1.domain.usecase

import com.example.labubu2in1.domain.EmployeeRepository
import com.example.labubu2in1.domain.model.Employee

class InsertEmployeeUseCase(private val repo: EmployeeRepository) {
    suspend operator fun invoke(employee: Employee) = repo.insert(employee)
}