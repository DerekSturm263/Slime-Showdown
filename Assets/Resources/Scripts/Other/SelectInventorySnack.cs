using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInventorySnack : MonoBehaviour
{
    public BattleSystem battleSystem;

    private void Update()
    {
        transform.position = new Vector2(battleSystem.inventorySlots[battleSystem.selectedInventoryNumber].transform.position.x, battleSystem.inventorySlots[battleSystem.selectedInventoryNumber].transform.position.y);
    }
}
