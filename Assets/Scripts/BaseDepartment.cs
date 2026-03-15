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
    
    [SerializeField] private GameObject employeePrefab;
    [SerializeField] private Collider areaBounds;

    private int employeeCount;
    private float productivity;
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
        assignedEmployees.AddRange(newEmployees);
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
}