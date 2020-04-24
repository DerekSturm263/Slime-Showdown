using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] inventorySlots = new GameObject[11];

    private void Start()
    {
        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");

        Array.Sort(inventorySlots, delegate (GameObject x, GameObject y)
        { 
            return x.name.CompareTo(y.name);
        });
    }
}
