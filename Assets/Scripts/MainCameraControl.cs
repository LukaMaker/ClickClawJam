using System.Collections;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    private bool inAnimation = false; //checks if is in an animation
    public void ViewWareHouse()
    {
        StartCoroutine(LookAnimation(-35, 1.5f));
    }
    public void ViewDesk()
    {
        StartCoroutine(LookAnimation(35, 1.5f));
    }
    private IEnumerator LookAnimation(float addAngle, float seconds) {
        inAnimation = true;
        float curTime = 0;
        while (curTime < seconds)
        {
            transform.eulerAngles = transform.eulerAngles + Vector3.right * addAngle * Time.fixedDeltaTime / seconds;
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
