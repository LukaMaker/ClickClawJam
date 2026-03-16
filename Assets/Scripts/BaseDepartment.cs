using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int totalSalary;
    private List<Employee> assignedEmployees = new();
    private List<GameObject> spawnedEmployees = new List<GameObject>();

    private void OnEnable()
    {
        EventBus.OnHireRoundEnded += HandleHireRoundEnded;
    }

    private void OnDisable()
    {
        EventBus.OnHireRoundEnded -= HandleHireRoundEnded;
    }

    public List<Employee> GetEmployees()
    {
        return assignedEmployees;
    }

    public void RemoveEmployee(Employee firedEmployee)
    {
        //remove employee from lists
        assignedEmployees.Remove(firedEmployee);

        // remove employee prod multiplier from average
        float empProdMult = Globals.ProductivityMatrix[firedEmployee.personality];
        prodMultiplier = ((prodMultiplier * employeeCount) - empProdMult)/(employeeCount - 1);

        //remove employee from count
        employeeCount -= 1;

        //remove employee salary from total
        totalSalary -= firedEmployee.salary;
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
        totalSalary += newEmployee.salary;
    }

    private void HandleHireRoundEnded(Dictionary<BaseDepartment, List<Employee>> hiredEmployees)
    {
        /*if (hiredEmployees.TryGetValue(this, out List<Employee> newEmployees))
        {
            foreach (var employee in newEmployees)
            {
                if (!spawnedEmployees.Contains(employee))
                {
                    SpawnEmployee(employee);
                }
            }
        }*/
    }
    public void SpawnEmployees()
    {
        foreach (GameObject employee in spawnedEmployees)
        {
            Destroy(employee);
        }
        spawnedEmployees.Clear();
        foreach (Employee employee in assignedEmployees)
        {
            SpawnEmployee(employee);
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
        spawnedEmployees.Add(newEmployeeObj);

        //set visuals
        newEmployeeObj.transform.GetChild(0).GetComponent<RawImage>().texture = employee.body;
        newEmployeeObj.transform.GetChild(0).GetComponent<RawImage>().color = employee.colours[0];
        newEmployeeObj.transform.GetChild(1).GetComponent<RawImage>().texture = employee.nose;
        newEmployeeObj.transform.GetChild(1).GetComponent<RawImage>().color = employee.colours[1];
        newEmployeeObj.transform.GetChild(2).GetComponent<RawImage>().texture = employee.mouth;
        newEmployeeObj.transform.GetChild(2).GetComponent<RawImage>().color = employee.colours[2];
        newEmployeeObj.transform.GetChild(3).GetComponent<RawImage>().texture = employee.hair;
        newEmployeeObj.transform.GetChild(3).GetComponent<RawImage>().color = employee.colours[3];
        newEmployeeObj.transform.GetChild(4).GetComponent<RawImage>().texture = employee.accessory;
        newEmployeeObj.transform.GetChild(4).GetComponent<RawImage>().color = employee.colours[4];
    }
    public float GetDepartmentProd()
    {
        float totalStr = 0, totalInt = 0, totalChr = 0, totalProd = 0;
        foreach (Employee emp in assignedEmployees)
        {
            totalStr += emp.strength;
            totalInt += emp.intelligence;
            totalChr += emp.charisma;
        }

        //need to multiply by desired stats scaling
        foreach (StatWeight statweight in DesiredStats)
        {
            switch (statweight.Stat)
            {
                case Globals.Trait.Charisma:
                    totalProd += (totalChr*statweight.Weighting);
                    break;
                case Globals.Trait.Intelligence:
                    totalProd += (totalInt*statweight.Weighting);
                    break;
                case Globals.Trait.Strength:
                    totalProd += (totalStr*statweight.Weighting);
                    break;
            }
        }

        totalProd *= prodMultiplier;
        return totalProd;
    }

    public int GetDepartmentGross()
    {
        float totalProd = GetDepartmentProd();
        int gross = (int)totalProd*GameConfig.ProductivityRatio;
        return gross;
    }
}