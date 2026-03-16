using UnityEngine;
using Assets.Scripts.Managers;
using System.Collections.Generic;

// controls core game loop/state machine. when in GameState.Hiring, ApplicantManager.HandleHiringRound()
// is called and prepares resume data from the Globals.GlobalWorkerPool that contains all the "cards" in the game basically.
public class GameManager : MonoBehaviour
{
    public BaseDepartment[] departments = new BaseDepartment[4];
    public static GameManager Instance { get; private set; }
    public GameState currentState { get; private set; }
    public int currentRound { get; private set; } = 1;

    public int currentProfit { get; private set; }
    public int requiredProfit = GameConfig.TargetProfit;

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
        Debug.Log($"Current round: {currentRound} of {GameConfig.NumRounds} rounds");
        EventBus.StateChanged(newState);

        switch (newState)
        {
            case GameState.Hiring:
                ApplicantManager.Instance.HandleHiringRound();
                break;
            case GameState.Observing:
                foreach (BaseDepartment department in departments)
                {
                    department.SpawnEmployees();
                    FightManager.Instance.HandleFightPhase(department);
                }
                FightManager.Instance.HandleFightPhase(fights);
                break;
        }
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
                if (currentRound > GameConfig.NumRounds)
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
        currentRound = 1;
        currentProfit = 0;
        EventBus.RoundChanged(currentRound);
        EventBus.ProfitChanged(currentProfit);
        SetState(GameState.Hiring);
    }
}