using System.Collections.Generic;
using static Globals;

public static class FlavourText
{
    public static string GetFightText(Fight fight)
    {
        List<string> options = FightText[fight.initiator.personality][fight.target.personality];
        string text = options[UnityEngine.Random.Range(0, options.Count)];
        return $"There's been a conflict... {text} How should this be resolved?"
            .Replace("Employee A", fight.initiator.name)
            .Replace("Employee B", fight.target.name);
    }
    public static Dictionary<PersonalityType, Dictionary<PersonalityType, List<string>>> FightText = new()
    {
        {
            PersonalityType.Psycho, new Dictionary<PersonalityType, List<string>>
            {
                {
                    PersonalityType.Psycho, new List<string>
                    {
                        "Employee A and Employee B have been seen growling at each other during work hours, unsettling customers.",
                        "Employee A and Employee B have been getting into nail gun duels over 'alpha status'. It's making a mess.",
                        "Employee A says that Employee B 'betrayed and murdered me on the battlefield in another life', making work tense."
                    }
                },
                {
                    PersonalityType.Nerd, new List<string>
                    {
                        "Employee A is convinced that Employee B is actually a robot. Employee B fears the look in Employee A's eyes.",
                        "Employee A keeps asking Employee B prying personal questions to discern if they're actually a foreign spy. Employee B is upset.",
                        "Employee A has been randomly attacking Employee B to 'make Employee B less of a beta'. Employee B is upset with this."
                    }
                },
                {
                    PersonalityType.Gossip, new List<string>
                    {
                        "Employee A is convinced that Employee B is part of the illuminati. Employee B fears the look in Employee A's eyes.",
                        "Employee A keeps setting potentially lethal booby traps around the office to 'keep us on our toes'. Employee B is fed up.",
                        "Employee A has released 10,000 huntsman spiders into the Munnings warehouse for 'better ambience'. Employee B is arachnophobic."
                    }
                },
                {
                    PersonalityType.Slacker, new List<string>
                    {
                        "Employee A has become convinced Employee B is a lizardman in disguise. Employee B fears the look in Employee A's eyes.",
                        "Employee A has started hunting Employee B with a crossbow outside of work hours. Employee B is unhappy with this arrangement.",
                        "Employee A is 'disgusted' by Employee B's work ethic, and is threatening violence unless productivity is improved."
                    }
                }
            }
        },
        {
            PersonalityType.Nerd, new Dictionary<PersonalityType, List<string>>
            {
                {
                    PersonalityType.Psycho, new List<string>
                    {
                        "Employee A asked Employee B if they watched any anime. Employee B became enraged at the suggestion.",
                        "Employee A did multi-figure multiplication in their head within earshot of Employee B, triggering an insecure rage.",
                        "Employee A requested that Employee B not shove them into a locker today, and this has hurt Employee B's feelings."
                    }
                },
                {
                    PersonalityType.Nerd, new List<string>
                    {
                        "An argument about whether the trebuchet is the pinnacle of siege weapon technology between Employee A and Employee B has come to blows.",
                        "Employee A called Cowboy Bebop 'mid'. Employee B instantly attacked Employee A.",
                        "Employee A let Employee B's character die in a Dungeons and Dragons game last night and they are no longer on speaking terms."
                    }
                },
                {
                    PersonalityType.Gossip, new List<string>
                    {
                        "Employee A keeps calling Employee B 'my fair maiden'. Employee B is weirded out.",
                        "Employee A wrote a love ballad for Employee B and performed it in front of the whole store. Employee B is weirded out.",
                        "Employee A keeps inviting Employee B to 'Star Trek night' and won't take no for an answer. Employee B is weirded out."
                    }
                },
                {
                    PersonalityType.Slacker, new List<string>
                    {
                        "Employee A keeps loudly blasting Weird Al Yankovic parodies from their phone at work. Employee B is fed up.",
                        "Employee A won't stop calling Employee B a 'Slytherin'. Employee B is tired of it.",
                        "Employee A keeps aggressively striking poses from some weird anime to intimidate Employee B. Employee B is tired of it."
                    }
                }
            }
        },
        {
            PersonalityType.Gossip, new Dictionary<PersonalityType, List<string>>
            {
                {
                    PersonalityType.Psycho, new List<string>
                    {
                        "Employee A has been spreading rumours that Employee B is a biblical demon sent to torment the Munnings workforce. Employee B is furious.",
                        "Employee A has been spreading rumours that Employee B drowns kittens for fun. Employee B is furious.",
                        "Employee A has been spreading rumours that Employee B Employee B is furious."
                    }
                },
                {
                    PersonalityType.Nerd, new List<string>
                    {
                        "Employee A has been spreading rumours that Employee B doesn't shower. Employee B feels humiliated.",
                        "Employee A has been spreading rumours that Employee B keeps their anime figurines in jars. Employee B feels humiliated.",
                        "Employee A has been spreading rumours that Employee B writes saucy Star Wars fanfiction. Employee B feels humiliated."
                    }
                },
                {
                    PersonalityType.Gossip, new List<string>
                    {
                        "Employee A has been spreading rumours that Employee B has a thing for feet. Employee B feels scandalized.",
                        "Employee A has been spreading rumours that Employee B is lying about their age. Employee B feels scandalized.",
                        "Employee A has been spreading rumours that Employee B is wearing a wig. Employee B feels scandalized."
                    }
                },
                {
                    PersonalityType.Slacker, new List<string>
                    {
                        "Employee A has been spreading rumours that Employee B is a slug piloting a human sized mechanical suit. Employee B is tired of it.",
                        "Employee A has been spreading rumours that Employee B does not have a soul. Employee B is tired of it.",
                        "Employee A has been spreading rumours that Employee B can't handle spicy food. Employee B is tired of it."
                    }
                }
            }
        },
        {
            PersonalityType.Slacker, new Dictionary<PersonalityType, List<string>>
            {
                {
                    PersonalityType.Psycho, new List<string>
                    {
                        "Employee A looked at Employee B the wrong way. Employee B is quivering with rage.",
                        "Employee A yawned nearby Employee B. Employee B is quivering with rage.",
                        "Employee A turned down Employee B's offer to start a fitness podcast together. Employee B is quivering with rage."
                    }
                },
                {
                    PersonalityType.Nerd, new List<string>
                    {
                        "Employee A has been making fun of Employee B for having an anime keychain on their bag... Employee B feels humiliated.",
                        "Employee A told Employee B that they need to start wearing deodorant to work. Employee B feels offended and humiliated.",
                        "Employee A described anime as 'those weird Chinese cartoons'. Employee B feels shocked and furious."
                    }
                },
                {
                    PersonalityType.Gossip, new List<string>
                    {
                        "Employee A got so sick of Employee B yammering all the time that they emptied a bucket of paint on them.",
                        "Employee A 'clapped back' too hard against one of Employee B's playful disses. Employee B feels scandalized.",
                        "Employee A heated up fish in the breakroom microwave. Employee B is furious and told them off, but they say they'll do it again."
                    }
                },
                {
                    PersonalityType.Slacker, new List<string>
                    {
                        "Employee A and Employee B came to blows over mutual claims that the other agreed to clean the bathroom. Neither is willing to budge.",
                        "Employee A and Employee B both misplaced thousands of dollars worth of paint, each blame the other.",
                        "Employee A agreed to cover Employee B's shift, but pulled out the morning of. Employee B is furious."
                    }
                }
            }
        }
    };
}