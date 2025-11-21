package com.example.labubu2in1.data

import com.example.labubu2in1.domain.model.Employee
import com.example.labubu2in1.data.local.EmployeeDao
import com.example.labubu2in1.data.local.toDomain
import com.example.labubu2in1.data.local.toEntity
import com.example.labubu2in1.domain.EmployeeRepository


class EmployeeRepositoryImpl(
    private val dao: EmployeeDao
) : EmployeeRepository {

    override suspend fun getAll(): List<Employee> =
        dao.getAll().map { it.toDomain() }

    override suspend fun insert(employee: Employee) =
        dao.insert(employee.toEntity())

    override suspend fun update(employee: Employee) =
        dao.update(employee.toEntity())

    override suspend fun delete(employee: Employee) =
        dao.delete(employee.toEntity())
}

