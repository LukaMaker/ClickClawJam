using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class FightManager : MonoBehaviour
{
    public List<Fight> GenerateFights(List<Employee> employees, Globals.Department department, int maxFights)
    {
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

    public void Resolve(Fight fight, Globals.FightOutcome outcome, Employee reallocated = null)
    {
        switch (outcome)
        {
            case Globals.FightOutcome.InstTerminated:
                // remove instigator
                // apply productivity boost to target
                break;
            case Globals.FightOutcome.TargTerminated:
                // remove target
                // apply productivity boost to instigator?
                break;
        }
    }
}