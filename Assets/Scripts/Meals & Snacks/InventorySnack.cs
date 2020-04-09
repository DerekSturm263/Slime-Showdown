using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySnack : MonoBehaviour
{
    public enum FoodType
    {
        Water, Air, Fire, Earth, Electric
    }

    public FoodType type;
    public float hungerIncrease;
}
