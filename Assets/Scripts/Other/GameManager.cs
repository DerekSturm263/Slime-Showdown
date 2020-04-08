using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    #region GameData

    public string version;

    #endregion

    public string playerSlimeColor;
    public string playerSlimeName;

    public uint goldCount;

    public GameObject[] inventory = new GameObject[27];
}
