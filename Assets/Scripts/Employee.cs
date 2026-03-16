using UnityEngine;
using static Globals;

public class Employee
{
    // identifying information
    public int id;
    public string name;
    public PersonalityType personality;
    public Gender gender;

    // base stats
    public int salary;
    public float strength;
    public float intelligence;
    public float charisma;

    // sprites
    public Sprite body;
    public Sprite hair;
    public Sprite mouth;
    public Sprite nose;
    public Sprite accessory;
    public Color hairColour;
    
    public Department assignedDepartment = Department.None;
    public float earningsMultiplier = 1f;
    public bool isFighting = false;
    public bool isFired = false;

    public Sprite getPortrait()
    {
        return null;
    }
}
