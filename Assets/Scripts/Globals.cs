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
        Psychopath,
        Nerd,
        Narcissists,
        Suckups
    }

    public enum Gender
    {
        M,
        F,
        NB
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

    public enum FightOutcome
    {
        Unresolved,
        InstTerminated,
        TargTerminated
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

    public static Dictionary<PersonalityType, Dictionary<PersonalityType, float>> ConflictMatrix = new()
    {
        {
            PersonalityType.Psychopath, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psychopath,   0.15f },
                { PersonalityType.Nerd,         0.10f },
                { PersonalityType.Narcissists,  0.12f },
                { PersonalityType.Suckups,      0.05f }
            }
        },
        {
            PersonalityType.Nerd, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psychopath,   0.08f },
                { PersonalityType.Nerd,         0.03f },
                { PersonalityType.Narcissists,  0.06f },
                { PersonalityType.Suckups,      0.01f }
            }
        },
        {
            PersonalityType.Narcissists, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psychopath,   0.12f },
                { PersonalityType.Nerd,         0.07f },
                { PersonalityType.Narcissists,  0.10f },
                { PersonalityType.Suckups,      0.04f }
            }
        },
        {
            PersonalityType.Suckups, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psychopath,   0.03f },
                { PersonalityType.Nerd,         0.02f },
                { PersonalityType.Narcissists,  0.04f },
                { PersonalityType.Suckups,      0.01f }
            }
        }
    };

    public static Dictionary<PersonalityType, float> ProductivityMatrix = new()
    {
        { PersonalityType.Psychopath,   4.0f },
        { PersonalityType.Nerd,         2.8f },
        { PersonalityType.Narcissists,  1.5f },
        { PersonalityType.Suckups,      0.4f }
    };
}