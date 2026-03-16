using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FightResolvePanel : MonoBehaviour
{
    private static FightResolvePanel instance;
    public static FightResolvePanel Instance
    {
        get
        {
            if (instance == null)
            {
                var allPanels = Resources.FindObjectsOfTypeAll<FightResolvePanel>();
                foreach (var panel in allPanels)
                {
                    if (panel != null && panel.gameObject.scene.IsValid())
                    {
                        instance = panel;
                        break;
                    }
                }
            }

            return instance;
        }
        private set => instance = value;
    }

    [SerializeField] private GameObject panelObject;
    [SerializeField] private Button hireInitiatorButton;
    [SerializeField] private Button hireTargetButton;
    [SerializeField] private Resume initiatorResume;
    [SerializeField] private Resume targetResume;

    private BaseDepartment currentDepartment;
    private Fight currentFight;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            return;
        }

        Instance = this;
        
        if (panelObject != null) panelObject.SetActive(false);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        hireInitiatorButton.onClick.AddListener(FireTarget);
        hireTargetButton.onClick.AddListener(FireInitiator);
    }

    public void ShowNextFight(BaseDepartment department)
    {
        if (department == null) return;

        currentDepartment = department;

        if (currentDepartment.pendingFights.Count > 0)
        {
            gameObject.SetActive(true); // Reactivate before showing panel!
            transform.SetAsLastSibling();
            currentFight = currentDepartment.pendingFights.Dequeue();
            
            if (panelObject != null) panelObject.SetActive(true);

            var panelCanvasGroup = panelObject != null ? panelObject.GetComponent<CanvasGroup>() : null;
            if (panelCanvasGroup != null)
            {
                panelCanvasGroup.interactable = true;
                panelCanvasGroup.blocksRaycasts = true;
                panelCanvasGroup.alpha = 1f;
            }

            if (hireInitiatorButton != null) hireInitiatorButton.interactable = true;
            if (hireTargetButton != null) hireTargetButton.interactable = true;

            EventBus.FightPanelOpened();

            // Populate resumes here
            if (initiatorResume != null) initiatorResume.Initialize(currentFight.initiator);
            if (targetResume != null) targetResume.Initialize(currentFight.target);
        }
        else
        {
            if (panelObject != null) panelObject.SetActive(false);
            
            gameObject.SetActive(false); // Hide the whole thing once fights are done
            EventBus.FightPanelClosed();
            EventBus.FightsResolved(department);
        }
    }

    private void FireTarget()
    {
        FightManager.Instance.Resolve(currentFight, Globals.FightOutcome.TargTerminated);
        ShowNextFight(currentDepartment);
    }

    private void FireInitiator()
    {
        FightManager.Instance.Resolve(currentFight, Globals.FightOutcome.InstTerminated);
        ShowNextFight(currentDepartment);
    }
}
