using UnityEngine;

public class CharacterPortrait : MonoBehaviour
{
    [Header("Part Renderers")]
    public SpriteRenderer body;
    public SpriteRenderer mouth;
    public SpriteRenderer nose;
    public SpriteRenderer hair;
    public SpriteRenderer accessory;

    [Header("Hair Colour")]
    public Color[] hairColours;
}