using UnityEngine;

public class CharacterPortrait : MonoBehaviour
{
    [Header("Part Renderers")]
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer mouth;
    [SerializeField] private SpriteRenderer nose;
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private SpriteRenderer accessory;

    [Header("Hair Colour")]
    public Color[] hairColours;

    public void BuildPortrait(Employee employee)
    {
        body.sprite =       employee.body;
        mouth.sprite =      employee.mouth;
        nose.sprite =       employee.nose;
        hair.sprite =       employee.hair;
        accessory.sprite =  employee.accessory;
    }
}