using System;
using System.Collections.Generic;

public static class Globals
{
    public static Dictionary<Employee, Resume> GlobalWorkerPool;

    public enum Department
    {
        None,
        FrontDesk,
        Replenishment,
        Garden,
        HelpDesk
    }

    public enum PersonalityType
    {
        A, B, C
    }

    public enum Trait
    {
        Strength,
        Intelligence,
        Charisma
    }

    public enum ObservingEvent
    {
        Fight, DptBuff, DptDebuff
    }

    [Serializable]
    public class FightEvent
    {
        public ObservingEvent eventType = ObservingEvent.Fight;
        public Employee initiator;
        public Employee target;
        public Department department;
    }

    [Serializable]
    public class DepartmentEvent
    {
        public ObservingEvent eventType;
        public Department department;
        public float statMultiplier;
    }
}