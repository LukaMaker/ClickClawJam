using System.Collections;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    private bool inAnimation = false;
    private bool isViewingWarehouse = false;
    private float animationSpeed = 0.2f;

    private void OnEnable()
    {
        EventBus.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        EventBus.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Observing:
                ViewWareHouse();
                break;
            case GameState.Hiring:
                ViewDesk();
                break;
        }
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameState.Hiring && !inAnimation)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ViewWareHouse();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ViewDesk();
            }
        }
    }

    public void ViewWareHouse()
    {
        if (isViewingWarehouse) return;
        isViewingWarehouse = true;
        EventBus.ViewChanged(isViewingWarehouse);
        StartCoroutine(LookAnimation(-35f, animationSpeed));
    }

    public void ViewDesk()
    {
        if (!isViewingWarehouse) return;
        isViewingWarehouse = false;
        EventBus.ViewChanged(isViewingWarehouse);
        StartCoroutine(LookAnimation(35f, animationSpeed));
    }

    private IEnumerator LookAnimation(float addAngle, float seconds)
    {
        inAnimation = true;
        float curTime = 0f;
        while (curTime < seconds)
        {
            transform.eulerAngles += Vector3.right * (addAngle * Time.fixedDeltaTime / seconds);
            curTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        inAnimation = false;
    }

    public bool IsInAnimation()
    {
        return inAnimation;
    }
}
