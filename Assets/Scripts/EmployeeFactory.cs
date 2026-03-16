using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public static class EmployeeFactory
{
    private static System.Random rng = new System.Random();
    private static EmployeeSpritePool _spritePool;
    private static EmployeeSpritePool SpritePool
    {
        get
        {
            if (_spritePool == null)
            {
                _spritePool = GameObject.FindAnyObjectByType<EmployeeSpritePool>();
            }
            return _spritePool;
        }
    }
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
        Gender gender = GenerateGender();

        Employee e = new Employee();
        e.id = id;
        e.personality = (PersonalityType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PersonalityType)).Length);
        e.gender = gender;
        e.name = GenerateName(gender);
        e.body = EmployeeSpritePool.Instance.GetRandomBody(gender, false);
        e.hair = EmployeeSpritePool.Instance.GetRandomHair(gender);
        e.mouth = EmployeeSpritePool.Instance.GetRandomMouth();
        e.nose = EmployeeSpritePool.Instance.GetRandomNose();
        e.accessory = EmployeeSpritePool.Instance.GetRandomAccessory();
        e.strength = strength;
        e.intelligence = intelligence;
        e.charisma = charisma;
        e.salary = GenerateSalary(strength, intelligence, charisma);
        return e;
    }

    private static Gender GenerateGender()
    {
        double rand = rng.NextDouble();
        switch (rand)
        {
            case double d when d < 0.49:
                return Gender.M;
            case double d when d > 0.51:
                return Gender.F;
            default:
                return Gender.NB;
        }
    }

    private static string GenerateName(Gender gender)
    {
        Name name;
        switch (gender)
        {
            case Gender.F:
                name = (Name)UnityEngine.Random.Range(0, 100);
                break;
            case Gender.M:
                name = (Name)UnityEngine.Random.Range(100, 200);
                break;
            default:
                name = (Name)UnityEngine.Random.Range(0, 200);
                break;
        }

        return name.ToString();
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