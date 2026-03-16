using System;
using System.Collections.Generic;

public static class EventBus
{
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<int> OnRoundChanged;
    public static event Action<bool> OnViewChanged;
    public static event Action<int> OnProfitChanged;
    public static event Action OnResumesConfirmed;
    public static event Action<   Dictionary<BaseDepartment, List<Employee>>   > OnHireRoundEnded;
    public static event Action<BaseDepartment, List<Employee>> OnEmployeesAssigned;
    public static event Action<BaseDepartment> OnFightsResolved;
    public static event Action OnFightPanelOpened;
    public static event Action OnFightPanelClosed;

    public static void FightPanelOpened()
    {
        OnFightPanelOpened?.Invoke();
    }

    public static void FightPanelClosed()
    {
        OnFightPanelClosed?.Invoke();
    }

    public static void FightsResolved(BaseDepartment department)
    {
        OnFightsResolved?.Invoke(department);
    }

    public static void EmployeesAssigned(BaseDepartment department, List<Employee> employees)
    {
        OnEmployeesAssigned?.Invoke(department, employees);
    }

    public static void HireRoundEnded(Dictionary<BaseDepartment, List<Employee>> hiredEmployees)
    {
        OnHireRoundEnded?.Invoke(hiredEmployees);
    }

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

    public static void ProfitChanged(int currentProfit)
    {
        OnProfitChanged?.Invoke(currentProfit);
    }
}
