using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState currentState { get; private set; }
    public int currentRound { get; private set; }
    public const int totalRounds = 3;

    public int currentProfit { get; private set; }
    public int requiredProfit = 1000;

    private void Awake()
    {
        Instance = this;
    }

    public void AddProfit(int amount)
    {
        currentProfit += amount;
        EventBus.ProfitChanged(currentProfit);
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"Game state changed to: {currentState}");
        Debug.Log($"Current round: {currentRound} of {totalRounds} rounds");
        EventBus.StateChanged(newState);
    }

    public void NextState()
    {
        switch(currentState)
        {
            case GameState.Hiring:
                SetState(GameState.Observing);
                break;
            case GameState.Observing:
                currentRound++;
                EventBus.RoundChanged(currentRound);
                if (currentRound > totalRounds)
                {
                    SetState(GameState.GameOver);
                }
                else
                {
                    SetState(GameState.Hiring);
                }
                break;
            case GameState.GameOver:
                Restart();
                break;
        }
    }

    public void Restart()
    {
        currentRound = 0;
        currentProfit = 0;
        EventBus.RoundChanged(currentRound);
        EventBus.ProfitChanged(currentProfit);
        SetState(GameState.Hiring);
    }
}