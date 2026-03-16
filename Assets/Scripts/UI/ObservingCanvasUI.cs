using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ObservingCanvasUI : MonoBehaviour
    {
        [SerializeField] private Button confirmObservingEndedButton;
        [SerializeField] private GameObject otherButtonsContainer; 
        [SerializeField] private GameObject quarterlyReportPanel;
        [SerializeField] private Button proceedToNextStageButton;

        private CanvasGroup canvasGroup;
        private bool isViewingWarehouse;
        private bool isShowingReport = false;

        private void Awake()
        {
            if (confirmObservingEndedButton != null)
            {
                confirmObservingEndedButton.onClick.AddListener(ShowQuarterlyReport);
            }

            if (proceedToNextStageButton != null)
            {
                proceedToNextStageButton.onClick.AddListener(ProceedToNextStage);
            }
        }

        private void OnEnable()
        {
            EventBus.OnViewChanged += HandleViewChanged;
            EventBus.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            EventBus.OnViewChanged -= HandleViewChanged;
            EventBus.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void HandleViewChanged(bool viewingWarehouse)
        {
            isViewingWarehouse = viewingWarehouse;
        }

        private void HandleGameStateChanged(GameState state)
        {
            if (state == GameState.Observing)
            {
                isShowingReport = false;
            }
        }

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        void Update()
        {
            bool isObservingState = GameManager.Instance.currentState == GameState.Observing;
            
            if (confirmObservingEndedButton != null)
            {
                confirmObservingEndedButton.gameObject.SetActive(isObservingState && !isShowingReport);
            }

            if (otherButtonsContainer != null)
            {
                otherButtonsContainer.SetActive(!isShowingReport);
            }

            // Toggle report panel visibility
            if (quarterlyReportPanel != null)
            {
                quarterlyReportPanel.SetActive(isObservingState && isShowingReport);
            }

            if (proceedToNextStageButton != null)
            {
                proceedToNextStageButton.gameObject.SetActive(isObservingState && isShowingReport);
            }

            if (canvasGroup != null)
            {
                bool shouldBeActive = isViewingWarehouse || isObservingState;
                canvasGroup.interactable = shouldBeActive;
                canvasGroup.blocksRaycasts = shouldBeActive;
                canvasGroup.alpha = shouldBeActive ? 1f : 0f;
            }
        }

        private void ShowQuarterlyReport()
        {
            isShowingReport = true;
        }

        private void ProceedToNextStage()
        {
            isShowingReport = false;
            GameManager.Instance.NextState();
        }
    }
}