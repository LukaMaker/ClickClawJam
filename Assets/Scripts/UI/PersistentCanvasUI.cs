using UnityEngine;
using TMPro;

namespace Assets.Scripts.UI
{
    public class PersistentCanvasUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private TextMeshProUGUI profitText;

        private void OnEnable()
        {
            EventBus.OnRoundChanged += HandleRoundChanged;
            EventBus.OnProfitChanged += HandleProfitChanged; 
        }

        private void OnDisable()
        {
            EventBus.OnRoundChanged -= HandleRoundChanged;
            EventBus.OnProfitChanged -= HandleProfitChanged;
        }

        private void Start()
        {
            UpdateRoundText(GameManager.Instance.currentRound);
            UpdateProfitText(GameManager.Instance.currentProfit, GameManager.Instance.requiredProfit);
        }

        private void HandleRoundChanged(int currentRound)
        {
            UpdateRoundText(currentRound);
        }

        private void HandleProfitChanged(int currentProfit)
        {
            UpdateProfitText(currentProfit, GameManager.Instance.requiredProfit);
        }

        private void UpdateRoundText(int round)
        {
            if (roundText != null)
            {
                roundText.text = $"Round: {round}/{GameManager.totalRounds}";
            }
        }

        private void UpdateProfitText(int currentProfit, int requiredProfit)
        {
            if (profitText != null)
            {
                profitText.text = $"Profit: ${currentProfit}/${requiredProfit}";
            }
        }
    }
}