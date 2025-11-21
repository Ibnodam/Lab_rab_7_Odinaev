package com.example.labubu2in1

import android.app.Application
import androidx.room.Room
import com.example.labubu2in1.data.EmployeeRepositoryImpl
import com.example.labubu2in1.data.local.AppDatabase
import com.example.labubu2in1.domain.usecase.DeleteEmployeeUseCase
import com.example.labubu2in1.domain.usecase.GetEmployeesUseCase
import com.example.labubu2in1.domain.usecase.InsertEmployeeUseCase
import com.example.labubu2in1.domain.usecase.UpdateEmployeeUseCase

class App : Application() {

    lateinit var db: AppDatabase
    lateinit var repository: EmployeeRepositoryImpl

    lateinit var getEmployees: GetEmployeesUseCase
    lateinit var insertEmployee: InsertEmployeeUseCase
    lateinit var updateEmployee: UpdateEmployeeUseCase
    lateinit var deleteEmployee: DeleteEmployeeUseCase

    override fun onCreate() {
        super.onCreate()

        db = Room.databaseBuilder(
            this,
            AppDatabase::class.java,
            "employees.db"
        ).build()

        repository = EmployeeRepositoryImpl(db.employeeDao())

        getEmployees = GetEmployeesUseCase(repository)
        insertEmployee = InsertEmployeeUseCase(repository)
        updateEmployee = UpdateEmployeeUseCase(repository)
        deleteEmployee = DeleteEmployeeUseCase(repository)
    }
}
