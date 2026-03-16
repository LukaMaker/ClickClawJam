using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class DepartmentInfoPanel : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI dptName;
        [SerializeField] private TextMeshProUGUI numEmp;
        [SerializeField] private TextMeshProUGUI prodMult;
        [SerializeField] private GameObject employeeDesc;
        [SerializeField] private Transform employeeList;

        void Start()
        {
            closeButton.onClick.AddListener(() => gameObject.SetActive(false));
            gameObject.SetActive(false);
        }

        public void ShowInfo(BaseDepartment department)
        {
            gameObject.SetActive(true);
            dptName.text = department.DepartmentType.ToString();
            numEmp.text = "Num. Employees: " + department.employeeCount.ToString();
            prodMult.text = "Productivity Multiplier: " + department.GetDepartmentProd().ToString("F2");
            for (int i = 0; i < employeeList.childCount; i++)
            {
                Destroy(employeeList.GetChild(i).gameObject);
            }
            for (int i = 0; i < department.GetEmployees().Count; i++)
            {
                Employee emp = department.GetEmployees()[i];
                GameObject go = Instantiate(employeeDesc, Vector3.zero, Quaternion.identity, employeeList);
                go.transform.localPosition = new Vector3(0, -i * 50, 0);
                print(go.transform.localPosition);
                go.GetComponent<TextMeshProUGUI>().text = emp.name + ": " + emp.personality.ToString();
            }
        }
    }
}