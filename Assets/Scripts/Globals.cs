using System;
using System.Collections.Generic;

public static class Globals
{
    public static List<Employee> GlobalWorkerPool;

    public enum Department
    {
        FrontDesk,
        Replenishment,
        Garden,
        HelpDesk
    }

    public enum PersonalityType
    {
        Psycho,
        Nerd,
        Gossips,
        Slackers
    }

    public static Dictionary<PersonalityType, string> PersonalityDesc = new()
    {
        { PersonalityType.Psycho,   "Brings in results, but impossible to work with." },
        { PersonalityType.Nerd,     "Knows everything, avoids interaction if possible." },
        { PersonalityType.Gossips,  "Spends more time annoying people than working." },
        { PersonalityType.Slackers, "Just here for the paycheck. They'd rather go home." }
    };

    public enum Gender
    {
        M,
        F,
        NB
    }

    public enum Name
    {
        CHARLOTTE, ISLA, OLIVIA, AMELIA, HARPER,
        HAZEL, VIOLET, ELSIE, LILY, RUBY,
        DAISY, GRACE, MIA, IVY, LUCY,
        WILLOW, ELLA, SOPHIA, SIENNA, AVA,
        MATILDA, AURORA, EVELYN, ISABELLA, MILA,
        MAEVE, MILLIE, SOPHIE, SADIE, FLORENCE,
        POPPY, SOFIA, CHLOE, ELLIE, ARIA,
        EVIE, ELEANOR, ELOISE, SCARLETT, BILLIE,
        LAYLA, MAISIE, ALICE, ZARA, AUDREY,
        MAYA, NORA, ADELINE, AYLA, LOTTIE,
        MARGOT, FREYA, HALLIE, DELILAH, IMOGEN,
        MAGGIE, GRACIE, ISABELLE, HANNAH, SUMMER,
        EMMA, EVA, HARRIET, REMI, ROSIE,
        ZOE, LOLA, LUNA, OLIVE, STELLA,
        BONNIE, PENELOPE, ELIANA, EMILIA, LILAH,
        MACKENZIE, AMARA, ELODIE, EMILY, LYLA,
        EDEN, GEORGIA, RILEY, FRANKIE, HARLOW,
        MABEL, ABIGAIL, ELENA, LILIANA, LILLY,
        PIPER, INDIE, LAINEY, SARAH, SAVANNAH,
        ADA, ADDISON, GIA, ISABEL, JASMINE,
        OLIVER, NOAH, HENRY, LEO, THEODORE,
        LUCA, CHARLIE, LEVI, ELIJAH, JACK,
        HUGO, OSCAR, HUDSON, WILLIAM, GEORGE,
        HARRISON, LUCAS, ARCHER, ARCHIE, HARVEY,
        ARTHUR, SEBASTIAN, DARCY, AUSTIN, HARRY,
        JAMES, ARLO, ISAAC, ALEXANDER, LIAM,
        BEAU, KAI, EDWARD, MUHAMMAD, LOGAN,
        PARKER, LOUIE, LOUIS, MAX, RORY,
        KEVIN, CARTER, JASPER, MASON, BILLY,
        SAMUEL, SONNY, THEO, THOMAS, BENJAMIN,
        ELLIOT, JUDE, LENNY, LUKA, MICHAEL,
        MILES, COOPER, PATRICK, LACHLAN, BODHI,
        EZRA, FLETCHER, JORDAN, JOSHUA, FINN,
        LINCOLN, MYLES, BOWIE, ELIAS, GRAYSON,
        ISAIAH, JOSEPH, MATTEO, SPENCER, ANGUS,
        CALEB, HUNTER, RYDER, ASHER, VINCENT,
        ALFRED, ETHAN, FINLEY, FELIX, HAMISH,
        KOA, OLLIE, XAVIER, ALFIE, DANIEL,
        JACKSON, MALAKAI, OAKLEY, OTIS, FLYNN,
        JACOB, MICAH, BOBBY, FREDDIE, ALI
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
    public class DepartmentEvent
    {
        public ObservingEvent eventType;
        public Department department;
        public float statMultiplier;
    }

    public static Dictionary<PersonalityType, Dictionary<PersonalityType, float>> ConflictMatrix = new()
    {
        {
            PersonalityType.Psycho, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psycho,   0.15f },
                { PersonalityType.Nerd,         0.10f },
                { PersonalityType.Gossips,  0.12f },
                { PersonalityType.Slackers,      0.05f }
            }
        },
        {
            PersonalityType.Nerd, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psycho,   0.08f },
                { PersonalityType.Nerd,         0.03f },
                { PersonalityType.Gossips,  0.06f },
                { PersonalityType.Slackers,      0.01f }
            }
        },
        {
            PersonalityType.Gossips, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psycho,   0.12f },
                { PersonalityType.Nerd,         0.07f },
                { PersonalityType.Gossips,  0.10f },
                { PersonalityType.Slackers,      0.04f }
            }
        },
        {
            PersonalityType.Slackers, new Dictionary<PersonalityType, float>
            {
                { PersonalityType.Psycho,   0.03f },
                { PersonalityType.Nerd,         0.02f },
                { PersonalityType.Gossips,  0.04f },
                { PersonalityType.Slackers,      0.01f }
            }
        }
    };

    public static Dictionary<PersonalityType, float> ProductivityMatrix = new()
    {
        { PersonalityType.Psycho,   4.0f },
        { PersonalityType.Nerd,         2.8f },
        { PersonalityType.Gossips,  1.5f },
        { PersonalityType.Slackers,      0.4f }
    };
}