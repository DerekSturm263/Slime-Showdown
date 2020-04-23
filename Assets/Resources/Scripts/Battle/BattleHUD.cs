using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text typeText;
    public Slider hpSlider;
    public Slider hungerSlider;

    public void SetHUD(Player player)
    {
        // Player info.
        nameText.text = player.name;
        typeText.text = player.type;
        hpSlider.maxValue = player.health;
        hpSlider.value = player.currentHP;

        if (player.gameObject.name == "Player Slime(Clone)")
        {
            hungerSlider.maxValue = player.hunger;
            hungerSlider.value = hungerSlider.maxValue;
        }
    }

    public void SetHealth(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetHunger(int hunger)
    {
        hungerSlider.value = hunger;
    }
}
