using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class Employee
{
    // identifying information
    public int id;
    public string name;
    public PersonalityType personality;
    public string description;
    public Gender gender;

    // base stats
    public int salary;
    public float strength;
    public float intelligence;
    public float charisma;

    // sprites
    public Texture body;
    public Texture hair;
    public Texture mouth;
    public Texture nose;
    public Texture accessory;
    public List<Color> colours;
    
    public BaseDepartment assignedDepartment = null;
    public float earningsMultiplier = 1f;
    public bool isFighting = false;
    public bool isFired = false;

    public Sprite getPortrait()
    {
        return null;
    }
}
