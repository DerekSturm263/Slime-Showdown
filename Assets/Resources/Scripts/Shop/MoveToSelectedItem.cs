using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSelectedItem : MonoBehaviour
{
    private ShopManager shopManager;
    public float selectSpeed;

    private void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, shopManager.selectedItem.transform.position, Time.deltaTime * selectSpeed);
    }
}
