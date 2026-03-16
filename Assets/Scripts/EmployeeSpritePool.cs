using UnityEngine;

[CreateAssetMenu(fileName = "EmployeeSpritePool", menuName = "Employee/SpritePool")]
public class EmployeeSpritePool : ScriptableObject
{
    public Sprite[] bodies;
    public Sprite[] mouths;
    public Sprite[] noses;
    public Sprite[] hairs;
    public Sprite[] accessories;
    public Color[] hairColours;
    
}