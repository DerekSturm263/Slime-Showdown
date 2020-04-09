using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSelectedItem : MonoBehaviour
{
    private ShopManager shopManager;
    public Vector3 offset;

    private void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();
    }

    private void Update()
    {
        transform.position = shopManager.selectedItem.transform.position + offset;
    }
}
