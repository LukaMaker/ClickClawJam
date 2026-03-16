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
            salary.GetChild(i).GetComponent<TextMeshProUGUI>().text = "-$" + departments[i].GetDepartmentSalary().ToString();
        }
        for (int i = 0; i < net.childCount; i++)
        {
            if (departments[i].GetNetMoney() >= 0)
            {
                net.GetChild(i).GetComponent<TextMeshProUGUI>().text = "$" + departments[i].GetNetMoney().ToString();
            } else
            {
                net.GetChild(i).GetComponent<TextMeshProUGUI>().text = "-$" + (departments[i].GetNetMoney()*-1).ToString();
            }
        }
        int profit = 0;
        foreach (BaseDepartment baseDepartment in departments)
        {
            profit += baseDepartment.GetNetMoney();
        }
        quarterlyProfit.text = "QUARTERLY PROFIT: $" + profit.ToString();
        GameManager.Instance.AddProfit(profit);
        runningProfit.text = "RUNNING PROFIT: $" + GameManager.Instance.currentProfit.ToString();
    }
}
