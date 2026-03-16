using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ObservingCanvasUI : MonoBehaviour
    {
        [SerializeField] private GameObject confirmObservingEndedButton;
        private CanvasGroup canvasGroup;
        private bool isViewingWarehouse;

        private void OnEnable()
        {
            EventBus.OnViewChanged += HandleViewChanged;
        }

        private void OnDisable()
        {
            EventBus.OnViewChanged -= HandleViewChanged;
        }

        private void HandleViewChanged(bool viewingWarehouse)
        {
            isViewingWarehouse = viewingWarehouse;
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
                confirmObservingEndedButton.SetActive(isObservingState);
            }

            if (canvasGroup != null)
            {
                bool shouldBeActive = isViewingWarehouse || isObservingState;
                canvasGroup.interactable = shouldBeActive;
                canvasGroup.blocksRaycasts = shouldBeActive;
                canvasGroup.alpha = shouldBeActive ? 1f : 0f;
            }
        }
    }
}