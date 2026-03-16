using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Analytics;
public class EmployeeSpritePool : MonoBehaviour
{
    public static EmployeeSpritePool Instance { get; private set; }

    public Texture[] fAppBodies; // female applicant bodies
    public Texture[] fEmpBodies; // female employee bodies
    public Texture[] mAppBodies; // male applicant bodies
    public Texture[] mEmpBodies; // male employee bodies
    public Texture[] nbAppBodies; // nonbinary applicant bodies
    public Texture[] nbEmpBodies; // nonbinary employee bodies

    public Texture[] fHairs; // female hairs
    public Texture[] mHairs; // male hairs
    public Texture[] aHairs; // all hairs

    public Texture[] mouths;
    public Texture[] noses;
    public Texture[] accessories;
    public Color[] hairColours;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        fAppBodies =    Resources.LoadAll<Texture>("EmployeeSprites/Bodies/Applicant/F");
        fEmpBodies =    Resources.LoadAll<Texture>("EmployeeSprites/Bodies/Employee/F");
        mAppBodies =    Resources.LoadAll<Texture>("EmployeeSprites/Bodies/Applicant/M");
        mEmpBodies =    Resources.LoadAll<Texture>("EmployeeSprites/Bodies/Employee/M");
        nbAppBodies =   Resources.LoadAll<Texture>("EmployeeSprites/Bodies/Applicant/NB");
        nbEmpBodies =   Resources.LoadAll<Texture>("EmployeeSprites/Bodies/Employee/NB");

        fHairs =        Resources.LoadAll<Texture>("EmployeeSprites/Hairs/F");
        mHairs =        Resources.LoadAll<Texture>("EmployeeSprites/Hairs/M");
        aHairs =        fHairs.Concat(mHairs).ToArray();

        mouths =        Resources.LoadAll<Texture>("EmployeeSprites/Mouths");
        noses =         Resources.LoadAll<Texture>("EmployeeSprites/Noses");
        accessories =   Resources.LoadAll<Texture>("EmployeeSPrites/Accessories");
    }

    public Texture GetRandomBody(Globals.Gender gender, bool isEmployee)
    {
        Texture[] pool = gender switch
        {
            Globals.Gender.F => isEmployee ? fEmpBodies : fAppBodies,
            Globals.Gender.M => isEmployee ? mEmpBodies : mAppBodies,
            _                => isEmployee ? nbEmpBodies : nbAppBodies
        };

        return pool[Random.Range(0, pool.Length)];
    }

    public Texture GetRandomHair(Globals.Gender gender)
    {
        Texture[] pool = gender switch
        {
            Globals.Gender.F => fHairs,
            Globals.Gender.M => mHairs,
            _                => aHairs
        };

        return pool[Random.Range(0, pool.Length)];
    }

    public Texture GetRandomMouth()
    {
        return mouths[Random.Range(0, mouths.Length)];
    }

    public Texture GetRandomNose()
    {
        return noses[Random.Range(0, noses.Length)];
    }

    public Texture GetRandomAccessory()
    {
        return accessories[Random.Range(0, accessories.Length)];
    }
    
}