using System.Runtime.CompilerServices;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public List<Fight> GenerateFights(List<Employee> employees, Globals.Department department)
    {
        // check each pair
        // return list of triggered fights
    }

    public void Resolve(Fight fight, FightOutcome outcome, Employee reallocated = null)
    {
        switch (outcome)
        {
            case FightOutcome.Ignored:
                // roll for voluntary leave chance
                break;
            case FightOutcome.Terminated:
                // remove initiator
                // apply productivity boost to target
                break;
            case FightOutcome.Reallocated:
                // change reallocated employee department
                // apply productivity reduction to reallocated
                break;
        }
    }
}