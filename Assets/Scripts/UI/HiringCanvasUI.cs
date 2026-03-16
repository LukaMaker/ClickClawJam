using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{
    // when ApplicantManager.HandleHiringRound is first called in GameManager, it calls StartNewRoundUI() (located here) once the resume data is prepared.
    // StartNewRoundUI() spawns the resume UI elements and the player can do whatever with them. once the player is done
    // they can confrim the batch which calls ApplicantManager to get the next batch for UI rendering. this cont until 
    // GameConfig.ResumesPerReached is reached, the nthey can confirm the round and it moves to the next game state. 
    // the acutal department allocation and all that is not implemented yet.
    public class HiringCanvasUI : MonoBehaviour
    {
        public static HiringCanvasUI Instance { get; private set; }
        [SerializeField] private Transform resumeSlotsParent;
        [SerializeField] private GameObject resumePrefab;
        [SerializeField] private Button confirmBatchButton;
        [SerializeField] private Button confirmRoundButton;

        private List<GameObject> activeResumeUIElements = new List<GameObject>();

        private void Awake()
        {
            Instance = this;

            confirmBatchButton.onClick.AddListener(OnConfirmBatchClicked);
            confirmRoundButton.onClick.AddListener(OnConfirmRoundClicked);
        }

        private void Update()
        {
            if (confirmBatchButton.gameObject.activeSelf)
            {
                confirmBatchButton.interactable = (resumeSlotsParent != null && resumeSlotsParent.childCount == 0);
            }
        }

        public void StartNewRoundUI()
        {
            confirmRoundButton.interactable = false;
            confirmBatchButton.interactable = true;
        }

        private void OnConfirmBatchClicked()
        {
            ApplicantManager.Instance.AssignEmployees();
            if (ApplicantManager.Instance.isEmpty())
            {
                confirmRoundButton.interactable = true;
            }
        }

        private void OnConfirmRoundClicked()
        {
            Debug.Log("Round complete");

            ApplicantManager.Instance.EndRound();

            confirmRoundButton.interactable = false;
            GameManager.Instance.NextState();
        }
    }
}