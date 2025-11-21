package com.example.labubu2in1.domain.usecase

import com.example.labubu2in1.domain.EmployeeRepository

class GetEmployeesUseCase(private val repo: EmployeeRepository) {
    suspend operator fun invoke() = repo.getAll()
}