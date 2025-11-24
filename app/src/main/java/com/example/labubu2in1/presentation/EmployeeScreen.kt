package com.example.labubu2in1.presentation

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.itemsIndexed
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.*
import androidx.compose.material3.Icon
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import com.example.labubu2in1.domain.model.Employee
import java.time.LocalDate
import java.time.Period

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun EmployeeScreen(vm: EmployeeViewModel) {
    var showDialog by remember { mutableStateOf(false) }
    var editEmployee by remember { mutableStateOf<Employee?>(null) }

    // Observe list (mutableStateListOf is observable automatically)
    val list = vm.employees
    val avg = vm.averageYears

    Scaffold(
        topBar = {
            CenterAlignedTopAppBar(
                title = { Text("Сотрудники — средний стаж ${"%.1f".format(avg)} лет") }
            )
        },
        floatingActionButton = {
            FloatingActionButton(onClick = {
                editEmployee = null
                showDialog = true
            }) {
                Icon(Icons.Default.Add, contentDescription = "Добавить")
            }
        }
    ) { padding ->
        Column(modifier = Modifier.padding(padding).fillMaxSize()) {
            if (list.isEmpty()) {
                Box(modifier = Modifier.fillMaxSize(), contentAlignment = Alignment.Center) {
                    Text("Список сотрудников пуст. Нажмите + чтобы добавить")
                }
            } else {
                LazyColumn(modifier = Modifier.fillMaxSize()) {
                    itemsIndexed(list) { idx, emp ->
                        val years = remember(emp.hireDate) {
                            val d = LocalDate.parse(emp.hireDate)
                            val now = LocalDate.now()
                            val p = Period.between(d, now)
                            (p.years * 12 + p.months) / 12.0
                        }

                        val isAbove = vm.isAboveAverage(emp.hireDate)
                        val bg = if (isAbove) Color(0xFFDFF7E0) else Color.White

                        EmployeeItem(
                            employee = emp,
                            years = years,
                            background = bg,
                            onEdit = {
                                editEmployee = emp
                                showDialog = true
                            },
                            onDelete = {
                                vm.delete(emp)
                            }
                        )
                    }
                }
            }
        }
    }

    if (showDialog) {
        AddEditEmployeeDialog(
            initial = editEmployee,
            onSave = { e ->
                if (e.id == 0) vm.add(e) else vm.update(e)
                showDialog = false
            },
            onDismiss = {
                showDialog = false
            }
        )
    }
}