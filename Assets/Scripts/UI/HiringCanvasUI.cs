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

            //SpawnNextBatch();
        }

        private void SpawnNextBatch()
        {/*
            ClearActiveResumes();

            List<Resume> currentBatch = ApplicantManager.Instance.GetNextBatch();

            foreach (Resume assignedResume in currentBatch)
            {
                GameObject resumeObject = Instantiate(resumePrefab, resumeSlotsParent);
                DraggableItem draggable = resumeObject.GetComponent<DraggableItem>();
                if (draggable != null)
                {
                    draggable.AssignedResume = assignedResume;
                }
                activeResumeUIElements.Add(resumeObject);
            }*/
        }

        private void OnConfirmBatchClicked()
        {/*
            ApplicantManager.Instance.ConfirmBatch();

            if (ApplicantManager.Instance.IsRoundComplete())
            {
                confirmBatchButton.interactable = false;
                confirmRoundButton.interactable = true;
            }
            else
            {
                SpawnNextBatch();
            }*/
        }

        private void OnConfirmRoundClicked()
        {
            Debug.Log("Round complete");
            
            Dictionary<BaseDepartment, List<Employee>> hiredEmployees = new Dictionary<BaseDepartment, List<Employee>>();
            UISlot[] allSlots = FindObjectsOfType<UISlot>();

            foreach (UISlot slot in allSlots)
            {
                if (slot.department != null && slot.slottedItems.Count > 0)
                {
                    if (!hiredEmployees.ContainsKey(slot.department))
                    {
                        hiredEmployees[slot.department] = new List<Employee>();
                    }

                    foreach (DraggableItem item in slot.slottedItems)
                    {
                        if (item.AssignedResume != null && item.AssignedResume.Employee != null)
                        {
                            hiredEmployees[slot.department].Add(item.AssignedResume.Employee);
                        }
                    }
                }
            }

            EventBus.HireRoundEnded(hiredEmployees);

            confirmRoundButton.interactable = false;
            ClearActiveResumes();
            GameManager.Instance.NextState();
        }

        private void ClearActiveResumes()
        {
            foreach (var uiElement in activeResumeUIElements)
            {
                if (uiElement != null) Destroy(uiElement);
            }
            activeResumeUIElements.Clear();
        }
    }
}