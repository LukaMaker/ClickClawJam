using System;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public static class EmployeeFactory
{
    public static List<Employee> CreateGlobalPool(int count)
    {
        List<Employee> pool = new List<Employee>(count);
        for (int i = 0; i < count; i++)
        {
            pool.Add(CreateEmployee(i));
        }
        return pool;
    }

    private static Employee CreateEmployee(int id)
    {
        return new Employee()
        {
            id = id,
            name = "Employee " + id,
            personality = (PersonalityType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PersonalityType)).Length),
            salary = UnityEngine.Random.Range(30000f, 150000f),
            strength = UnityEngine.Random.Range(1f, 10f),
            intelligence = UnityEngine.Random.Range(1f, 10f),
            charisma = UnityEngine.Random.Range(1f, 10f)
        };
    }
}