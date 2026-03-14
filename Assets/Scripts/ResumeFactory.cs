public class ResumeFactory
{
    public static Resume CreateFromEmployee(Employee employee)
    {
        Resume resume = new Resume(employee);
    }
    public static List<Resume> CreateFromEmployees(List<Employee> employees)
    {
        List<Resume> resumes = new List<Resume>();
        foreach (Employee emp in employees)
        {
            resumes.Add(new Resume(emp));
        }
    }
}