using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Analytics;
public class EmployeeSpritePool : MonoBehaviour
{
    public static EmployeeSpritePool Instance { get; private set; }

    public Sprite[] fAppBodies; // female applicant bodies
    public Sprite[] fEmpBodies; // female employee bodies
    public Sprite[] mAppBodies; // male applicant bodies
    public Sprite[] mEmpBodies; // male employee bodies
    public Sprite[] nbAppBodies; // nonbinary applicant bodies
    public Sprite[] nbEmpBodies; // nonbinary employee bodies

    public Sprite[] fHairs; // female hairs
    public Sprite[] mHairs; // male hairs
    public Sprite[] aHairs; // all hairs

    public Sprite[] mouths;
    public Sprite[] noses;
    public Sprite[] accessories;
    public Color[] hairColours;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        fAppBodies =    Resources.LoadAll<Sprite>("EmployeeSprites/Bodies/Applicant/F");
        fEmpBodies =    Resources.LoadAll<Sprite>("EmployeeSprites/Bodies/Employee/F");
        mAppBodies =    Resources.LoadAll<Sprite>("EmployeeSprites/Bodies/Applicant/M");
        mEmpBodies =    Resources.LoadAll<Sprite>("EmployeeSprites/Bodies/Employee/M");
        nbAppBodies =   Resources.LoadAll<Sprite>("EmployeeSprites/Bodies/Applicant/NB");
        nbEmpBodies =   Resources.LoadAll<Sprite>("EmployeeSprites/Bodies/Employee/NB");

        fHairs =        Resources.LoadAll<Sprite>("EmployeeSprites/Hairs/F");
        mHairs =        Resources.LoadAll<Sprite>("EmployeeSprites/Hairs/M");
        aHairs =        fHairs.Concat(mHairs).ToArray();

        mouths =        Resources.LoadAll<Sprite>("EmployeeSprites/Mouths");
        noses =         Resources.LoadAll<Sprite>("EmployeeSprites/Noses");
        accessories =   Resources.LoadAll<Sprite>("EmployeeSPrites/Accessories");
    }

    public Sprite GetRandomBody(Globals.Gender gender, bool isEmployee)
    {
        Sprite[] pool = gender switch
        {
            Globals.Gender.F => isEmployee ? fEmpBodies : fAppBodies,
            Globals.Gender.M => isEmployee ? mEmpBodies : mAppBodies,
            _                => isEmployee ? nbEmpBodies : nbAppBodies
        };

        return pool[Random.Range(0, pool.Length)];
    }

    public Sprite GetRandomHair(Globals.Gender gender)
    {
        Sprite[] pool = gender switch
        {
            Globals.Gender.F => fHairs,
            Globals.Gender.M => mHairs,
            _                => aHairs
        };

        return pool[Random.Range(0, pool.Length)];
    }

    public Sprite GetRandomMouth()
    {
        return mouths[Random.Range(0, mouths.Length)];
    }

    public Sprite GetRandomNose()
    {
        return noses[Random.Range(0, noses.Length)];
    }

    public Sprite GetRandomAccessory()
    {
        return accessories[Random.Range(0, accessories.Length)];
    }
    
}