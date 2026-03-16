using System.Collections.Generic;
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
    private float baseProd;
    private float prodMultiplier;
    private List<Employee> assignedEmployees = new List<Employee>();

    public void AssignNewEmployees(List<Employee> newEmployees)
    {
        foreach (Employee employee in newEmployees)
        {
            AssignEmployee(employee);
        }
    }

    private void AssignEmployee(Employee newEmployee)
    {
        assignedEmployees.Add(newEmployee);
        float newEmpProdMult = Globals.ProductivityMatrix[newEmployee.personality];
        prodMultiplier = ((prodMultiplier * employeeCount) + newEmpProdMult)/(employeeCount + 1);
        employeeCount += 1;
    }
}