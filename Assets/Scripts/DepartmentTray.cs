using UnityEngine;

public class DepartmentTray : MonoBehaviour
{
    public BaseDepartment department;
    public bool isShred, isRecycle;
    [SerializeField] private Material hightlight, normal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Resume>())
        {
            other.GetComponent<Resume>().currentTray = this;
        }
    }
    private void FixedUpdate()
    {
        GetComponent<MeshRenderer>().material = normal;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Resume>())
        {
            GetComponent<MeshRenderer>().material = hightlight;
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
