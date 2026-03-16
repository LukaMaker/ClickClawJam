using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepartmentTray : MonoBehaviour
{
    public BaseDepartment department;
    public bool isShred, isRecycle;
    [SerializeField] private Material hightlight, normal;
    [SerializeField] private TextMeshProUGUI prediction;
    private List<Employee> employees = new List<Employee>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Resume>())
        {
            other.GetComponent<Resume>().currentTray = this;
            employees.Add(other.GetComponent<Resume>().Employee);
        }
    }
    private void FixedUpdate()
    {
        GetComponent<MeshRenderer>().material = normal;
        if (prediction)
        {
            prediction.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Resume>())
        {
            GetComponent<MeshRenderer>().material = hightlight;
            if (prediction)
            {
                prediction.enabled = true;
                //incase employee deleted
                for (int i = 0; i < employees.Count; i++)
                {
                    if (employees[i] == null)
                    {
                        employees.RemoveAt(i);
                    }
                }
                prediction.text = "Productivity:<br>" + (department.GetDepartmentProd() * 100).ToString() + "%->" + (department.GetProdPrediction(employees) * 100).ToString() + "%";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Resume>())
        {
            other.GetComponent<Resume>().currentTray = null;
            employees.Remove(other.GetComponent<Resume>().Employee);
        }
    }
}
