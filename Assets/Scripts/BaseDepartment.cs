using UnityEngine;

[System.Serializable]
public struct StatWeight
{
    public Globals.Stats Stat;
    [Range(0f, 1f)]
    public float Weighting;
}

public class BaseDepartment : MonoBehaviour
{
    [SerializeField] private Globals.DepartmentType DepartmentType;
    [SerializeField] private StatWeight[] DesiredStats = new StatWeight[2];

    private int employeeCount;
    private float productivity;

    
}