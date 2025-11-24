package com.example.labubu2in1.presentation

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.height
import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.material3.OutlinedTextField
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.labubu2in1.domain.model.Employee

@Composable
fun AddEditEmployeeDialog(
    initial: Employee?,
    onSave: (Employee) -> Unit,
    onDismiss: () -> Unit
) {
    var last by remember { mutableStateOf("") }
    var first by remember  { mutableStateOf("") }
    var middle by remember  { mutableStateOf("") }
    var pos by remember  { mutableStateOf("") }
    var gender by remember { mutableStateOf(initial?.gender ?: "М") }
    var date by remember { mutableStateOf(initial?.hireDate ?: "2020-01-01") }

    AlertDialog(
        onDismissRequest = onDismiss,
        confirmButton = {
            TextButton(onClick = {
                if (last.isBlank() || first.isBlank() || pos.isBlank() || date.isBlank()) {
                    // minimal validation: don't allow empty essential fields
                    return@TextButton
                }
                val e = Employee(
                    id = initial?.id ?: 0,
                    lastName = last.trim(),
                    firstName = first.trim(),
                    middleName = if (middle.isBlank()) null else middle.trim(),
                    position = pos.trim(),
                    gender = gender.trim(),
                    hireDate = date.trim()
                )
                onSave(e)
            }) {
                Text("Сохранить")
            }
        },
        dismissButton = {
            TextButton(onClick = onDismiss) { Text("Отмена") }
        },
        title = { Text(if (initial == null) "Добавить сотрудника" else "Редактировать сотрудника") },
        text = {
            Column {
                OutlinedTextField(value = last, onValueChange = { last = it }, label = { Text("Фамилия") })
                Spacer(Modifier.height(6.dp))
                OutlinedTextField(value = first, onValueChange = { first = it }, label = { Text("Имя") })
                Spacer(Modifier.height(6.dp))
                OutlinedTextField(value = middle, onValueChange = { middle = it }, label = { Text("Отчество") })
                Spacer(Modifier.height(6.dp))
                OutlinedTextField(value = pos, onValueChange = { pos = it }, label = { Text("Должность") })
                Spacer(Modifier.height(6.dp))
                OutlinedTextField(value = gender, onValueChange = { gender = it }, label = { Text("Пол") })
                Spacer(Modifier.height(6.dp))
                OutlinedTextField(value = date, onValueChange = { date = it }, label = { Text("Дата приема YYYY-MM-DD") })
            }
        }
    )
}

