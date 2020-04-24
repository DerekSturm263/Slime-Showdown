using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInventorySnack : MonoBehaviour
{
    public BattleSystem battleSystem;

    private void Update()
    {
        transform.position = new Vector2(battleSystem.selectedInventorySlot.transform.position.x, battleSystem.selectedInventorySlot.transform.position.y);
    }
}
