using UnityEngine;

public class Resume : MonoBehaviour
{
    public Employee Employee { get; private set; }

    public void Initialize(Employee employee)
    {
        Employee = employee;
    }
}
