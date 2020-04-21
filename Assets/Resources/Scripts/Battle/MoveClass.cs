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
     public int MoveUse(string ResAffinity, string CritAff, int PowerLV, int userAffOfType, int EnemyAff, string EnemyAffType) //the weakness here is the player needs a check for their move type
     {
        int damage = 0;
        //damage equation remember if crit 2(moveaff*powerLV)-enemyAff
        //crit
        if (CritAff == EnemyAffType)//naming convention is only capital first letter no spaces just type ie "Fire"
        {
            damage = 2*(userAffOfType + PowerLV) - EnemyAff;
        }
        //res
        else if(ResAffinity == EnemyAffType)
        {
            damage = (1/2) * (userAffOfType + PowerLV) - EnemyAff;
        }
        //normal
        else
        {
        damage = (userAffOfType + PowerLV);
        }
        Debug.Log(damage + "This is the damage");
        return damage;
     }
    //overload
    public int MoveUse(MoveClass move, int userAffOfType, int EnemyAff, string EnemyAffType)
    {
        int damage = 0;
        if (move.CritAff == EnemyAffType)
        {
            damage = 2 * (userAffOfType + move.PowerLV) - EnemyAff;
        }
        else if (move.ResAff == EnemyAffType)
        {
            damage = (1 / 2) * (userAffOfType + move.PowerLV) - EnemyAff;
        }
        else
        {
            damage = (userAffOfType + move.PowerLV);
        }
        Debug.Log(damage + "This is the damage");
        return damage;
    }
}
