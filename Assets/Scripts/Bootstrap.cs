using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SetState(GameState.Hiring);
    }
}