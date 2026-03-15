using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-100)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject resumeContainer;

    void Start()
    {
        InitialiseWorkers();
        GameManager.Instance.SetState(GameState.Hiring);
    }

    private void InitialiseWorkers()
    {
        Globals.GlobalWorkerPool = new List<Employee>();
        
        List<Employee> employees = EmployeeFactory.CreateGlobalPool(GameConfig.NumWorkers);
        Resume[] resumes = ResumeFactory.CreateFromEmployees(employees.ToArray(), resumeContainer.transform);

        for (int i = 0; i < employees.Count; i++)
        {
            Globals.GlobalWorkerPool.Add(employees[i]);
        }
    }
}