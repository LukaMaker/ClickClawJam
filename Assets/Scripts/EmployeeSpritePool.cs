using UnityEngine;

[CreateAssetMenu(fileName = "EmployeeSpritePool", menuName = "Employee/Sprite Pool")]
public class EmployeeSpritePool : ScriptableObject
{
    public Sprite[] bodies;
    public Sprite[] mouths;
    public Sprite[] noses;
    public Sprite[] hairs;
    public Sprite[] accessories;
    public Sprite[] hairColours;
}