using System;

public static class Globals
{

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

    public enum Stats
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
    public class Worker
    {
        public int id;
        public string name;
        public PersonalityType personality;
        public float salary;
        public float strength;
        public float intelligence;
        public float charisma;

        public Department assignedDepartment = Department.None;
        public float earningsMultiplier = 1f;
        public bool isFighting = false;
        public bool isFired = false;
    }

    [Serializable]
    public class FightEvent
    {
        public ObservingEvent eventType = ObservingEvent.Fight;
        public Worker initiator;
        public Worker target;
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