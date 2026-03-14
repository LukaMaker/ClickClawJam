using UnityEngine;

[System.Serializable]
public struct StatWeight
{
    public Globals.Trait Stat;
    [Range(0f, 1f)]
    public float Weighting;
}

public class BaseDepartment : MonoBehaviour
{
    [SerializeField] private Globals.Department DepartmentType;
    [SerializeField] private StatWeight[] DesiredStats = new StatWeight[2];

    private int employeeCount;
    private float productivity;

    
}