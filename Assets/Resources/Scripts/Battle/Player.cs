﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Note this is the same script for the enemy, the only difference is the moves!
    public string name;
    public string color;
    public string type;
    public int health;
    public int hunger;
    public int currentHP;
    public int dmg;
    

    //This determines how much money the player wins or loses while in battle
    public int VicGold;

    //affinity typing
    public int sourAff;
    public int spicyAff;
    public int seafoodAff;
    public int candyAff;
    public int veggieAff;

    //Determines the size of the slime based on stats
    public float size;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP<= 0)
        {
            return true;
        }
        return false;
    }
    public void Heal(int heal)
    {
        currentHP += heal;
        if (currentHP > health)
        {
            currentHP = health;
        }

    }

}
