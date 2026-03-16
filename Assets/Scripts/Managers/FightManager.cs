using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void HandleFightPhase(BaseDepartment department)
    {
        List<Fight> fights = GenerateFights(department);
        if (fights.Count > 0)
        {
            Debug.Log($"Created {fights.Count} fights for department: {department.gameObject.name}");
        }
        foreach (var fight in fights)
        {
            department.pendingFights.Enqueue(fight);
        }
    }

    public List<Fight> GenerateFights(BaseDepartment department, int maxFights = 2)
    {
        List<Employee> employees = department.GetEmployees();
        List<Fight> fights = new();

        // check each pair
        for (int i = 0; i < employees.Count; i++)
        {
            for (int j = i + 1; j < employees.Count; j++)
            {
                Employee a = employees[i];
                Employee b = employees[j];

                float chanceAInitiates = Globals.ConflictMatrix[a.personality][b.personality];
                float chanceBInitiates = Globals.ConflictMatrix[b.personality][a.personality];

                if (UnityEngine.Random.value < chanceAInitiates)
                {
                    fights.Add(new Fight(a, b, department));
                }

                if (UnityEngine.Random.value < chanceBInitiates)
                {
                    fights.Add(new Fight(b, a, department));
                }
            }
        }

        // randomly pick maxFight number
        return fights
            .OrderBy(_ => UnityEngine.Random.value)
            .Take(maxFights)
            .ToList();
    }

    public void Resolve(Fight fight, Globals.FightOutcome outcome)
    {
        Employee instigator = fight.initiator;
        Employee target = fight.target;
        BaseDepartment department = fight.department;

        switch (outcome)
        {
            case Globals.FightOutcome.InstTerminated:
                // remove instigator
                if (department.GetEmployees().Contains(instigator))
                {
                    department.RemoveEmployee(instigator);
                }
                break;
            case Globals.FightOutcome.TargTerminated:
                // remove target
                if (department.GetEmployees().Contains(target))
                {
                    department.RemoveEmployee(target);
                }
                break;
        }
    }
}