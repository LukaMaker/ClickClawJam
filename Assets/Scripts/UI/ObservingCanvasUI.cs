using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [System.Serializable]
    public struct DepartmentFightAlert
    {
        public BaseDepartment department;
        public Button alertButton;
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class ObservingCanvasUI : MonoBehaviour
    {
        [SerializeField] private Button confirmObservingEndedButton;
        [SerializeField] private GameObject otherButtonsContainer; 
        [SerializeField] private GameObject quarterlyReportPanel;
        [SerializeField] private Button proceedToNextStageButton;
        [SerializeField] private DepartmentFightAlert[] departmentAlerts;

        private CanvasGroup canvasGroup;
        private bool isViewingWarehouse;
        private bool isShowingReport = false;
        private bool isResolvingFight = false;

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

            if (departmentAlerts != null)
            {
                foreach (var alert in departmentAlerts)
                {
                    if (alert.alertButton != null && alert.department != null)
                    {
                        BaseDepartment cachedDept = alert.department;
                        alert.alertButton.onClick.AddListener(() => FightResolvePanel.Instance.ShowNextFight(cachedDept));
                    }
                }
            }
        }

        private void OnEnable()
        {
            EventBus.OnViewChanged += HandleViewChanged;
            EventBus.OnGameStateChanged += HandleGameStateChanged;
            EventBus.OnFightPanelOpened += HandleFightPanelOpened;
            EventBus.OnFightPanelClosed += HandleFightPanelClosed;
        }

        private void OnDisable()
        {
            EventBus.OnViewChanged -= HandleViewChanged;
            EventBus.OnGameStateChanged -= HandleGameStateChanged;
            EventBus.OnFightPanelOpened -= HandleFightPanelOpened;
            EventBus.OnFightPanelClosed -= HandleFightPanelClosed;
        }

        private void HandleFightPanelOpened()
        {
            isResolvingFight = true;
        }

        private void HandleFightPanelClosed()
        {
            isResolvingFight = false;
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

        private bool HasPendingFights()
        {
            if (GameManager.Instance == null) return false;
            foreach (var dept in GameManager.Instance.departments)
            {
                if (dept != null && dept.pendingFights.Count > 0)
                    return true;
            }
            return false;
        }

        void Update()
        {
            bool isObservingState = GameManager.Instance.currentState == GameState.Observing;
            
            if (confirmObservingEndedButton != null)
            {
                confirmObservingEndedButton.gameObject.SetActive(isObservingState && !isShowingReport && !HasPendingFights() && !isResolvingFight);
            }

            if (departmentAlerts != null)
            {
                foreach (var alert in departmentAlerts)
                {
                    if (alert.alertButton != null && alert.department != null)
                    {
                        bool hasFight = isObservingState && alert.department.pendingFights.Count > 0 && !isResolvingFight;
                        alert.alertButton.gameObject.SetActive(hasFight);
                    }
                }
            }

            if (otherButtonsContainer != null)
            {
                otherButtonsContainer.SetActive(!isShowingReport);
            }

            // Toggle report panel visibility
            if (quarterlyReportPanel != null)
            {
                quarterlyReportPanel.SetActive(isObservingState && isShowingReport && !isResolvingFight);
            }

            if (proceedToNextStageButton != null)
            {
                proceedToNextStageButton.gameObject.SetActive(isObservingState && isShowingReport && !isResolvingFight);
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
            quarterlyReportPanel.GetComponent<QuarterlyReport>().UpdateReportText();
            isShowingReport = true;
        }

        private void ProceedToNextStage()
        {
            isShowingReport = false;
            GameManager.Instance.NextState();
        }
    }
}