using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSelectedItem : MonoBehaviour
{
    private ShopManager shopManager;

    private void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();
    }

    private void Update()
    {
        transform.position = shopManager.selectedItem.gameObject.transform.position + new Vector3(4.9f, 0.49f, 0);
    }
}
