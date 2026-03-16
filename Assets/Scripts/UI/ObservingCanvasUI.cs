using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ObservingCanvasUI : MonoBehaviour
    {
        [Header("Observing Phase UI")]
        [SerializeField] private Button confirmObservingEndedButton;
        [Tooltip("Assign an empty GameObject here containing other UI buttons you want to hide when the report shows.")]
        [SerializeField] private GameObject otherButtonsContainer; 

        [Header("Quarterly Report UI")]
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
            // Reset the report view when we re-enter the observing state
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
            
            // Toggle observing button visibility (hidden when reading report)
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

            // Canvas group interaction completely controls general viewing visibility
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