using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    private GameManager gameManager;

    public Text nameText;
    public Text typeText;
    public Slider hpSlider;

    public Text move1Text;
    public Text move2Text;
    public Text move3Text;

    private List<GameObject> moveList = new List<GameObject>();
    private List<Text> moveTextList = new List<Text>();

    public Color waterColor;
    public Color fireColor;
    public Color airColor;
    public Color earthColor;
    public Color electricColor;
    public Color normalColor;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        moveList.Add(GameObject.FindGameObjectWithTag("Move1"));
        moveList.Add(GameObject.FindGameObjectWithTag("Move2"));
        moveList.Add(GameObject.FindGameObjectWithTag("Move3"));

        moveTextList.Add(move1Text);
        moveTextList.Add(move2Text);
        moveTextList.Add(move3Text);

        for (int i = 0; i < 3; i++)
        {
            if (gameManager.PlayerMoves[i].AffType == "Water")
                moveList[i].GetComponent<Image>().color = waterColor;
            else if (gameManager.PlayerMoves[i].AffType == "Fire")
                moveList[i].GetComponent<Image>().color = fireColor;
            else if (gameManager.PlayerMoves[i].AffType == "Air")
                moveList[i].GetComponent<Image>().color = airColor;
            else if (gameManager.PlayerMoves[i].AffType == "Earth")
                moveList[i].GetComponent<Image>().color = earthColor;
            else if (gameManager.PlayerMoves[i].AffType == "Electric")
                moveList[i].GetComponent<Image>().color = electricColor;
            else
                moveList[i].GetComponent<Image>().color = normalColor;

            moveTextList[i].text = gameManager.PlayerMoves[i].Name;
        }
    }

    public void SetHUD(Player player)
    {
        // Player info.
        nameText.text = player.name;
        typeText.text = player.type;
        hpSlider.maxValue = player.health;
        hpSlider.value = player.currentHP;
    }

    public void SetHealth(int hp)
    {
        hpSlider.value = hp;
    }
}
