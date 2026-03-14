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
        if (Input.GetKeyDown(KeyCode.P))
        {
            ProgressRound();
            print(round);
            print(roundPhase);
        }
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
