using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ObservingCanvasUI : MonoBehaviour
    {
        [SerializeField] private GameObject confirmObservingEndedButton;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.currentState == GameState.Hiring) confirmObservingEndedButton.SetActive(false);
            else if (GameManager.Instance.currentState == GameState.Observing) confirmObservingEndedButton.SetActive(true);
        }
    }
}