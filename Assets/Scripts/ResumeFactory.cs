public class ResumeFactory
{
    public static Resume[] CreateFromEmployees(Employee[] employees)
    {
        Resume[] resumes = new Resume[employees.Length];
        for (int i = 0; i < employees.Length; i++)
        {
            resumes[i] = new Resume(employees[i]);
        }

        return resumes;
    }
}