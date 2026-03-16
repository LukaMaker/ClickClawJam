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
    [SerializeField] public Globals.Department DepartmentType;
    [SerializeField] private StatWeight[] DesiredStats = new StatWeight[2];
    
    [SerializeField] private GameObject employeePrefab;
    [SerializeField] private Collider areaBounds;

    public int employeeCount { get; private set; }
    public float baseProd;
    public float prodMultiplier;
    private List<Employee> assignedEmployees = new List<Employee>();
    private HashSet<Employee> spawnedEmployees = new HashSet<Employee>();

    private void OnEnable()
    {
        EventBus.OnHireRoundEnded += HandleHireRoundEnded;
    }

    private void OnDisable()
    {
        EventBus.OnHireRoundEnded -= HandleHireRoundEnded;
    }

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

    private void HandleHireRoundEnded(Dictionary<BaseDepartment, List<Employee>> hiredEmployees)
    {
        if (hiredEmployees.TryGetValue(this, out List<Employee> newEmployees))
        {
            foreach (var employee in newEmployees)
            {
                if (!spawnedEmployees.Contains(employee))
                {
                    SpawnEmployee(employee);
                }
            }
        }
    }

    private void SpawnEmployee(Employee employee)
    {
        Bounds bounds = areaBounds.bounds;
        Vector3 randomPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            areaBounds.transform.position.y + 0.5f,
            Random.Range(bounds.min.z, bounds.max.z)
        );

        GameObject newEmployeeObj = Instantiate(employeePrefab, randomPosition, Quaternion.identity, transform);
        
        EmployeeWander wanderer = newEmployeeObj.GetComponent<EmployeeWander>();
        if (wanderer == null)
            wanderer = newEmployeeObj.AddComponent<EmployeeWander>();

        wanderer.walkArea = areaBounds;
        spawnedEmployees.Add(employee);
    }
    public int GetDepartmentGross()
    {
        int gross = 0;
        float totalStr = 0, totalInt = 0, totalChr = 0;
        foreach (Employee emp in assignedEmployees)
        {
            totalStr += emp.strength;
            totalInt += emp.intelligence;
            totalChr += emp.charisma;
        }
        //need to multiply by desired stats scaling
        return gross;
    }
}