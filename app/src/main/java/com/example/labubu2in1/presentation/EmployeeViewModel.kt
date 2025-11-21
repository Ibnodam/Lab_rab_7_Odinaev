package com.example.labubu2in1.presentation

import androidx.compose.runtime.mutableStateListOf
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.labubu2in1.App
import com.example.labubu2in1.domain.model.Employee
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.time.LocalDate
import java.time.Period
import kotlin.math.roundToInt

class EmployeeViewModel(private val app: App) : ViewModel() {

    // UI-observable list (Compose friendly)
    val employees = mutableStateListOf<Employee>()

    // average tenure in years (double)
    var averageYears: Double = 0.0
        private set

    init {
        load()
    }

    private fun yearsSince(hireDate: String): Int {
        return try {
            val d = LocalDate.parse(hireDate)
            Period.between(d, LocalDate.now()).years
        } catch (t: Throwable) {
            0
        }
    }

    private fun recalcAverage(list: List<Employee>) {
        averageYears = if (list.isEmpty()) 0.0 else {
            val avg = list.map { yearsSince(it.hireDate).toDouble() }.average()
            // keep 1 decimal precision
            (avg * 10.0).roundToInt() / 10.0
        }
    }

    fun load() {
        viewModelScope.launch(Dispatchers.IO) {
            val list = app.getEmployees()
            employees.clear()
            employees.addAll(list)
            recalcAverage(list)
        }
    }

    fun add(e: Employee, onDone: (() -> Unit)? = null) {
        viewModelScope.launch(Dispatchers.IO) {
            app.insertEmployee(e)
            load()
            onDone?.invoke()
        }
    }

    fun update(e: Employee, onDone: (() -> Unit)? = null) {
        viewModelScope.launch(Dispatchers.IO) {
            app.updateEmployee(e)
            load()
            onDone?.invoke()
        }
    }

    fun delete(e: Employee, onDone: (() -> Unit)? = null) {
        viewModelScope.launch(Dispatchers.IO) {
            app.deleteEmployee(e)
            load()
            onDone?.invoke()
        }
    }

    // helper for UI to check if employee is above-average
    fun isAboveAverage(hireDate: String): Boolean {
        return yearsSince(hireDate) > averageYears
    }
}