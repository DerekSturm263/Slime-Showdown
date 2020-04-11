﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeMain : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Ranch"))
        {
            Camera.main.GetComponent<CameraFollow>().cameraTarget = GameObject.FindGameObjectWithTag("RanchBattleSlime");
            GetComponent<SlimeMove>().enabled = true;
        }
    }
}
