using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public Employee Employee { get; private set; }
    public DepartmentTray currentTray = null;
    [SerializeField] private Image portrait, strength, intelligence, charisma;
    [SerializeField] private TextMeshProUGUI name, salary, personality;
    private float statWidth = 60, statHeight = 5; //just for bar chart UI
    public void Initialize(Employee employee)
    {
        Employee = employee;
        portrait.sprite = Employee.getPortrait();
        strength.rectTransform.sizeDelta = new Vector2(statWidth * (Employee.strength/100), statHeight);
        intelligence.rectTransform.sizeDelta = new Vector2(statWidth * (Employee.intelligence / 100), statHeight);
        charisma.rectTransform.sizeDelta = new Vector2(statWidth * (Employee.charisma / 100), statHeight);

        name.text = Employee.name;
        salary.text = "$" + Employee.salary.ToString();
        personality.text = Employee.personality.ToString();
    }
}
