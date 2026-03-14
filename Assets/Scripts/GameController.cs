using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MainCameraControl mainCameraControl;
    private int round = 0;
    private string roundPhase = "Selection"; //can change to enum
    void Start()
    {
        
    }

    void Update()
    {
        //"P" to progress round
        if (Input.GetKeyDown(KeyCode.P) && canProgress())
        {
            ProgressRound();
            print(round);
            print(roundPhase);
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
        if (roundPhase == "Selection")
        {
            //done selecting so overview what happens after hiring
            roundPhase = "Overview";
            mainCameraControl.ViewWareHouse();
        } else
        {
            roundPhase = "Selection";
            mainCameraControl.ViewDesk();
            round += 1;
        }
    }
}
