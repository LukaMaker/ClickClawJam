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
        Professional,
        Liability,
        Mediator
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
                { PersonalityType.Professional, 0.10f },
                { PersonalityType.Liability,    0.12f },
                { PersonalityType.Mediator,     0.05f }
            }
        },
        {
            PersonalityType.Professional, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psychopath,   0.08f },
                { PersonalityType.Professional, 0.03f },
                { PersonalityType.Liability,    0.06f },
                { PersonalityType.Mediator,     0.01f }
            }
        },
        {
            PersonalityType.Liability, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psychopath,   0.12f },
                { PersonalityType.Professional, 0.07f },
                { PersonalityType.Liability,    0.10f },
                { PersonalityType.Mediator,     0.04f }
            }
        },
        {
            PersonalityType.Mediator, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psychopath,   0.03f },
                { PersonalityType.Professional, 0.02f },
                { PersonalityType.Liability,    0.04f },
                { PersonalityType.Mediator,     0.01f }
            }
        }
    };
}