using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class DepartmentInfoButton : MonoBehaviour
    {
        [SerializeField] private BaseDepartment associatedDepartment;
        [SerializeField] private DepartmentInfoPanel departmentInfoPanel;
        [SerializeField] private Button button;

        // Use this for initialization
        void Start()
        {
            button.onClick.AddListener(() => departmentInfoPanel.ShowInfo(associatedDepartment));
        }
    }
}