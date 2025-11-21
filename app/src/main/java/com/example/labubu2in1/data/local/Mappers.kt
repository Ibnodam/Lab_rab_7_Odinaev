package com.example.labubu2in1.data.local

import com.example.labubu2in1.data.local.EmployeeEntity
import com.example.labubu2in1.domain.model.Employee

fun EmployeeEntity.toDomain() =
    Employee(id, lastName, firstName, middleName, position, gender, hireDate)

fun Employee.toEntity() =
    EmployeeEntity(id, lastName, firstName, middleName, position, gender, hireDate)
