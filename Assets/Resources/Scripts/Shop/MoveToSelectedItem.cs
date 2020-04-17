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
        transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, shopManager.selectedItem.transform.position.y, Time.deltaTime * selectSpeed));
    }
}
