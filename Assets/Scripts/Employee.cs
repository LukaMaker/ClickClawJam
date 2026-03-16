using UnityEngine;
using static Globals;

public class Employee
{
    public int id;
    public string name;
    public PersonalityType personality;
    public Gender gender;
    public float salary;
    public float strength;
    public float intelligence;
    public float charisma;

    public Department assignedDepartment = Department.None;
    public float earningsMultiplier = 1f;
    public bool isFighting = false;
    public bool isFired = false;
}
