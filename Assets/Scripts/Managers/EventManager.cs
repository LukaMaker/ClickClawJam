using UnityEngine;

// might be temp idk
public class EventManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.NextState();
        }
    }
}