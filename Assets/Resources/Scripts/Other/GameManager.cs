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

    [Header("Options")]
    public bool isFullscreen = false;
    public float musicVolume = 1f;
    public float soundVolume = 1f;

    [Header("Player Info")]
    public string playerSlimeColor;
    public string playerSlimeName;
    public string playerSlimeSize;

    [Space(10)]

    public uint goldCount;
    public GameObject[] inventory = new GameObject[27];
    public Vector3 lastPlayerPos = new Vector3(-6.5f, 2, 0);

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

    [Header("MoveList")]
    // Possible moves the slime can learn.
    //public List<MoveClass> PossibleMoves = new List<MoveClass>(16);
    public MoveClass MoveRoll;
    public MoveClass MovePepperSpray;
    public MoveClass MoveFlameShot;
    public MoveClass MoveFireBall;
    public MoveClass MoveSplash;
    public MoveClass MoveWaterBall;
    public MoveClass MoveWaterHose;
    public MoveClass MoveBlow;
    public MoveClass MoveAirCutter;
    public MoveClass MoveWindBlade;
    public MoveClass MovePebbleSpit;
    public MoveClass MoveRockToss;
    public MoveClass MoveSeismicSmash;
    public MoveClass MoveZap;
    public MoveClass MoveShock;
    public MoveClass MoveThunderShock;
    [Header("Player Moves")]
    // Current moves the slime knows.
    //for now using lists till we figure out how to get arrays to work
    public List<MoveClass> PlayerMoves = new List<MoveClass>(3);
}
