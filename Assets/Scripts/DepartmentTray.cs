using UnityEngine;

public class DepartmentTray : MonoBehaviour
{
    public BaseDepartment department;
    public bool isShred, isRecycle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Resume>())
        {
            other.GetComponent<Resume>().currentTray = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Resume>())
        {
            other.GetComponent<Resume>().currentTray = null;
        }
    }
}
