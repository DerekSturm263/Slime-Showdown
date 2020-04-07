using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackItem : MonoBehaviour
{
    public enum FoodType
    {
        None, Water, Air, Fire, Earth, Electric
    }

    public FoodType type;

    public string foodName;
    public Sprite image;
}
