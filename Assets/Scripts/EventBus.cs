using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class EventBus
{
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<int> OnRoundChanged;
    public static event Action<bool> OnViewChanged;

    public static void ViewChanged(bool isViewingWarehouse)
    {
        OnViewChanged?.Invoke(isViewingWarehouse);
    }

    public static void StateChanged(GameState newState)
    {
        OnGameStateChanged?.Invoke(newState);
    }

    public static void RoundChanged(int round)
    {
        OnRoundChanged?.Invoke(round);
    }
}
