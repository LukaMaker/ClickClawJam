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
        }
    }
}