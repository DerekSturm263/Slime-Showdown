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

    [Header("Ranch Stuff")]
    public uint goldCount;
    public GameObject[] inventory = new GameObject[27];
    public Vector3 lastPlayerPos = new Vector3(-6.5f, 2, 0);

    [Header("Player Slime Stuff")]
    public string playerSlimeColor;
    public string playerSlimeName;
    public string playerSlimeSize;

    [Header("Player Affinities")]
    public float playSeafoodAff;
    public float playCandyAff;
    public float playSpicyAff;
    public float playVeggieAff;
    public float playSourAff;

    [Header("Enemy Stats")]
    public string enemySlimeColor;
    public string enemySlimeName;
    public string enemySlimeType;
    public int enemyHealth;
    public int enemyHunger;
    public int enemyCurrentHP;
    public int enemyDmg;
    public int enemyVicGold;
    public int enemySourAff;
    public int enemySpicyAff;
    public int enemySeafoodAff;
    public int enemyCandyAff;
    public int enemyVeggieAff;
    public float enemySize;
}
