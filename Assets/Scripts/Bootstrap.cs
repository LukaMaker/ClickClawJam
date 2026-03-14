using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-100)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject resumeContainer;
    public static Dictionary<Employee, Resume> GlobalApplicantPool { get; private set; }

    void Start()
    {
        InitializeApplicants();
        GameManager.Instance.SetState(GameState.Hiring);
    }

    private void InitializeApplicants()
    {
        GlobalApplicantPool = new Dictionary<Employee, Resume>();
        
        List<Employee> employees = EmployeeFactory.CreateGlobalPool(GameConfig.GlobalApplicantPool);
        Resume[] resumes = ResumeFactory.CreateFromEmployees(employees.ToArray(), resumeContainer.transform);

        for (int i = 0; i < employees.Count; i++)
        {
            GlobalApplicantPool.Add(employees[i], resumes[i]);
        }
    }
}