using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    #region GameData

    public string version;

    #endregion

    public enum FoodType
    {
        Water, Air, Fire, Earth, Electric
    }

    [Header("Slime Stuff")]
    public string playerSlimeColor;
    public string playerSlimeName;

    [Header("Affinities")]
    public float playSeafoodAff;
    public float playCandyAff;
    public float playSpicyAff;
    public float playVeggieAff;
    public float playSourAff;

    [Header("Ranch Stuff")]
    public uint goldCount;

    public GameObject[] inventory = new GameObject[27];
}
