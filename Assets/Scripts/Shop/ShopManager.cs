using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject selectedItem;
    private bool usingAxisY = false;

    private void Update()
    {
        if (!usingAxisY)
        {
            if (Input.GetAxisRaw("Vertical") > 0 && selectedItem.GetComponent<Buyable>().itemUp != null)
            {
                usingAxisY = true;
                selectedItem = selectedItem.GetComponent<Buyable>().itemUp;
            }
            else if (Input.GetAxisRaw("Vertical") < 0 && selectedItem.GetComponent<Buyable>().itemDown != null)
            {
                usingAxisY = true;
                selectedItem = selectedItem.GetComponent<Buyable>().itemDown;
            }
        }

        if (Input.GetAxisRaw("Vertical") == 0)
            usingAxisY = false;
    }
}
