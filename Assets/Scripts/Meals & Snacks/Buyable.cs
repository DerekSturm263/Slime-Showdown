﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyable : MonoBehaviour
{
    public enum FoodType
    {
        None, Water, Air, Fire, Earth, Electric
    }

    public FoodType type;

    public string foodName;
    [Multiline()]
    public string description;
    public Sprite image;
    public uint price;

    public GameObject itemUp;
    public GameObject itemDown;
}
