using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public int totalSalary;
    private List<Employee> assignedEmployees = new();
    private List<GameObject> spawnedEmployees = new List<GameObject>();
    private Dictionary<Employee, GameObject> spawnedEmployeeLookup = new Dictionary<Employee, GameObject>();
    public Queue<Fight> pendingFights = new Queue<Fight>();

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
        if (firedEmployee == null) return;

        if (!assignedEmployees.Contains(firedEmployee))
        {
            DespawnEmployee(firedEmployee);
            return;
        }

        //remove employee from lists
        assignedEmployees.Remove(firedEmployee);

        // remove employee prod multiplier from average
        float empProdMult = Globals.ProductivityMatrix[firedEmployee.personality];

        //remove employee from count
        employeeCount -= 1;

        //remove employee salary from total
        totalSalary -= firedEmployee.salary;

        DespawnEmployee(firedEmployee);
    }

    public void AssignNewEmployees(List<Employee> newEmployees)
    {
        foreach (Employee employee in newEmployees)
        {
            AssignEmployee(employee);
        }
    }
    public void RemoveEmployees(List<Employee> newEmployees)
    {
        foreach (Employee employee in newEmployees)
        {
            RemoveEmployee(employee);
        }
    }

    private void AssignEmployee(Employee newEmployee) 
    {
        assignedEmployees.Add(newEmployee);
        float newEmpProdMult = Globals.ProductivityMatrix[newEmployee.personality];
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
        spawnedEmployeeLookup.Clear();
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
        spawnedEmployeeLookup[employee] = newEmployeeObj;

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

    private void DespawnEmployee(Employee employee)
    {
        if (employee == null) return;

        if (spawnedEmployeeLookup.TryGetValue(employee, out GameObject employeeObject))
        {
            if (employeeObject != null)
            {
                spawnedEmployees.Remove(employeeObject);
                Destroy(employeeObject);
            }

            spawnedEmployeeLookup.Remove(employee);
        }
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

        //totalProd *= prodMultiplier;
        totalProd /= 100; //since all stats 0-100
        return totalProd;
    }
    public float GetProdPrediction(List<Employee> emps)
    {
        AssignNewEmployees(emps);
        float prod = GetDepartmentProd();
        RemoveEmployees(emps);
        return prod;
    }

    public int GetDepartmentGross()
    {
        float totalProd = GetDepartmentProd();
        print(totalProd);
        int gross = (int)(totalProd*GameConfig.ProductivityRatio);
        return gross;
    }
    public int GetDepartmentSalary()
    {
        int salary = 0;
        foreach(Employee emp in assignedEmployees)
        {
            salary += emp.salary;
        }
        return salary;
    }
    public int GetNetMoney()
    {
        return GetDepartmentGross() - GetDepartmentSalary();
    }
}