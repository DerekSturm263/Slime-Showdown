using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyable : MonoBehaviour
{
    public enum FoodType
    {
        Water, Air, Fire, Earth, Electric
    }

    public string foodName;
    public FoodType type;
    public uint price;

    public GameObject itemUp;
    public GameObject itemDown;
}
