using UnityEngine;

public class ResumeFactory
{
    public static Resume[] CreateFromEmployees(Employee[] employees, Transform container = null)
    {
        Resume[] resumes = new Resume[employees.Length];
        for (int i = 0; i < employees.Length; i++)
        {
            var resumeObject = new GameObject($"Resume_{employees[i].name}");
            if (container != null) resumeObject.transform.SetParent(container, false);
            resumes[i] = resumeObject.AddComponent<Resume>();
            resumes[i].Initialize(employees[i]);
        }

        return resumes;
    }
}