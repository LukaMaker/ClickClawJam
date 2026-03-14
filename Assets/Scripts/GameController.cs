using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MainCameraControl mainCameraControl;
    [SerializeField] private BaseDepartment[] departments = new BaseDepartment[4];
    private int round = 0;
    private GameState currentGameState;

    public enum GameState
    {
        Hiring,
        Observing,
        Win,
        Lose
    }

    void Start()
    {
        currentGameState = GameState.Hiring;
        mainCameraControl.currentView = MainCameraControl.CurrentView.Desk;
        if (round == 0)
        {
            print("First round, enter hiring state");
        }
    }

    void Update()
    {
        //"P" to progress round
        if (Input.GetKeyDown(KeyCode.P) && canProgress())
        {
            ProgressRound();
            print("Current round: " + round);
            print("Entering game state: " + currentGameState);
        }

        if (currentGameState == GameState.Hiring)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !mainCameraControl.IsInAnimation() && mainCameraControl.currentView == MainCameraControl.CurrentView.Desk)
            {
                mainCameraControl.ViewWareHouse();
                mainCameraControl.currentView = MainCameraControl.CurrentView.WareHouse;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !mainCameraControl.IsInAnimation() && mainCameraControl.currentView == MainCameraControl.CurrentView.WareHouse)
            {
                mainCameraControl.ViewDesk();
                mainCameraControl.currentView = MainCameraControl.CurrentView.Desk;
            }
        }
    }
    private bool canProgress()
    {
        //just to prevend players from going to next round before everything is finished eg. all animations are done / conflicts are resolved
        if (mainCameraControl.IsInAnimation())
        {
            return false;
        }
        return true;
    }
    private void ProgressRound()
    {
        if (currentGameState == GameState.Hiring)
        {
            //done selecting so overview what happens after hiring
            currentGameState = GameState.Observing;
            mainCameraControl.ViewWareHouse();
        } else
        {
            currentGameState = GameState.Hiring;
            mainCameraControl.ViewDesk();
            round += 1;
        }
    }
}
