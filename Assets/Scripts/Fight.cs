public class Fight
{
    public Employee initiator { get; private set; }
    public Employee target { get; private set; }
    public BaseDepartment department { get; private set; }
    private Globals.FightOutcome outcome;

    public Fight(Employee a, Employee b, BaseDepartment type)
    {
        initiator = a;
        target = b;
        department = type;
    }
}