using UnityEngine;
using Assets.Scripts.Managers;

// controls core game loop/state machine. when in GameState.Hiring, ApplicantManager.HandleHiringRound()
// is called and prepares resume data from the Globals.GlobalWorkerPool that contains all the "cards" in the game basically.
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

        if (newState == GameState.Hiring)
        {
            ApplicantManager.Instance.HandleHiringRound();
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