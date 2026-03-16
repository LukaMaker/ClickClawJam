public class Fight
{
    private Employee initiator;
    private Employee target;
    private Globals.Department department;
    private Globals.FightOutcome outcome;

    public Fight(Employee a, Employee b, Globals.Department type)
    {
        initiator = a;
        target = b;
        department = type;
    }
}