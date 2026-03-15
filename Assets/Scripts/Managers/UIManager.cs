using System.Collections;
using UnityEngine;
using Assets.Scripts.UI;

public class UIManager : MonoBehaviour
{
    // hook up the UI canvases
    [SerializeField] HiringCanvasUI hiringCanvasUI;
    [SerializeField] ObservingCanvasUI observingCanvasUI;
    [SerializeField] GameOverUI gameOverUI;

    private void OnEnable()
    {
        EventBus.OnGameStateChanged += HandleGameStateChanged;
        EventBus.OnRoundChanged += HandleRoundChanged;
        EventBus.OnViewChanged += HandleViewChanged;
    }

    private void OnDisable()
    {
        EventBus.OnGameStateChanged -= HandleGameStateChanged;
        EventBus.OnRoundChanged -= HandleRoundChanged;
        EventBus.OnViewChanged -= HandleViewChanged;
    }

    void HandleGameStateChanged(GameState newState)
    {
        hiringCanvasUI.gameObject.SetActive(newState == GameState.Hiring);
        observingCanvasUI.gameObject.SetActive(newState == GameState.Observing);
        gameOverUI.gameObject.SetActive(newState == GameState.GameOver);

        switch (newState)
        {
            case GameState.Hiring:
                break;
            case GameState.Observing:
                break;
            case GameState.GameOver:
                break;
        }
    }

    void HandleRoundChanged(int newRound)
    {
        // update the round number in the UI or smth
    }

    void HandleViewChanged(bool isViewingWarehouse)
    {
        if (GameManager.Instance.currentState == GameState.Hiring)
        {
            hiringCanvasUI.gameObject.SetActive(!isViewingWarehouse);
            observingCanvasUI.gameObject.SetActive(isViewingWarehouse);
        }
    }
}