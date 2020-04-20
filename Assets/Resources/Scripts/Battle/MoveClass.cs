using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveClass : MonoBehaviour
{
    //basics for moves
    public string Name;
    public string AffType;
    public string ResAff;
    public string CritAff;
    public int PowerLV;//this will range from 1-3 for the 3 stages of affinity type

    //the constructor will let us compile the moves into a list of type MoveClass
    public MoveClass(string name, string affType, string resAff, string critAff, int powerLV)
    {
        Name = name;
        AffType = affType;
        ResAff = resAff;
        CritAff = critAff;
        PowerLV = powerLV;
    }

    //this method will take the parameters for a move then return the damage for the take damage method the player class has
    // public int MoveUse()
    //  {

    // }
}
