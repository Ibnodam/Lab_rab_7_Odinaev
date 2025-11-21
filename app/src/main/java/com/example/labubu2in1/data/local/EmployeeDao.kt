package com.example.labubu2in1.data.local

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.Query
import androidx.room.Update


@Dao
interface EmployeeDao {

    @Query("SELECT * FROM employees")
    suspend fun getAll(): List<EmployeeEntity>

    @Insert
    suspend fun insert(employee: EmployeeEntity)

    @Update
    suspend fun update(employee: EmployeeEntity)

    @Delete
    suspend fun delete(employee: EmployeeEntity)
}