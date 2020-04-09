using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    #region GameData

    public string version;

    #endregion

    [Header("Slime Stuff")]
    public string playerSlimeColor;
    public string playerSlimeName;

    public int playSourAff;
    public int playSpicyAff;
    public int playSeafoodAff;
    public int playCandyAff;
    public int payVeggieAff;

    [Header("Ranch Stuff")]
    public uint goldCount;

    public float sunRotSpeed;
    public float sunRot;

    public GameObject[] inventory = new GameObject[27];

    private void FixedUpdate()
    {
        sunRot += sunRotSpeed * Time.deltaTime;
    }
}
