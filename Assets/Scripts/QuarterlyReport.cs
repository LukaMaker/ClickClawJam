using TMPro;
using UnityEngine;

public class QuarterlyReport : MonoBehaviour
{
    [SerializeField] private Transform department, gross, salary, net;
    [SerializeField] private TextMeshProUGUI quarterlyProfit, runningProfit;
    
    public void UpdateReportText()
    {
        BaseDepartment[] departments = GameManager.Instance.departments;
        for (int i = 0; i < gross.childCount; i++)
        {
            gross.GetChild(i).GetComponent<TextMeshProUGUI>().text = "$" + departments[i].GetDepartmentGross().ToString();
        }
        for (int i = 0; i < salary.childCount; i++)
        {
            //salary.GetChild(i).GetComponent<TextMeshProUGUI>().text = "-$" + departments[i].GetDepartmentSalary().ToString();
        }
    }
}
