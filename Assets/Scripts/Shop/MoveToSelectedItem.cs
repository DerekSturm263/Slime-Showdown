using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSelectedItem : MonoBehaviour
{
    private ShopManager shopManager;
    public float zOffset;

    private void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, shopManager.selectedItem.gameObject.transform.position.y + 0.49f, zOffset);
    }
}
