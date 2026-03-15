using System;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public static class EmployeeFactory
{
    private static System.Random rng = new System.Random();
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
        int strength = GenerateStat();
        int intelligence = GenerateStat();
        int charisma = GenerateStat();

        return new Employee()
        {
            id = id,
            name = "Employee " + id,
            personality = (PersonalityType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PersonalityType)).Length),
            strength = strength,
            intelligence = intelligence,
            charisma = charisma,
            salary = GenerateSalary(strength, intelligence, charisma)
        };
    }

    private static int GenerateStat(float mean = 50f, float stdDev = 15f)
    /*
    Box Muller Transform for normally distributed stat
    generation.
    */
    {
        float u1 = 1f - (float)rng.NextDouble();
        float u2 = 1f - (float)rng.NextDouble();
        float normal = MathF.Sqrt(-2f * Mathf.Log(u1)) * MathF.Sin(2f * Mathf.PI * u2);

        return (int)Math.Clamp(mean + stdDev * normal, 0f, 100f);
    }

    private static int GenerateSalary(int strength, int intelligence, int charisma)
    {
        float trueValue = (strength + intelligence + charisma) / 300f; // true value evaluation
        float baseSalary = Mathf.Lerp(30000f, 150000f, trueValue);

        float bias = (float)(rng.NextDouble() * 0.8f + 0.6f);
        return (int)Mathf.Round(baseSalary * bias);
    }
}