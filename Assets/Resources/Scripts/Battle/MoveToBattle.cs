using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToBattle : MonoBehaviour
{
    private GameManager gameManager;
    private Player stats;
    private GameObject player;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        stats = GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("RanchBattleSlime");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RanchBattleSlime"))
        {
            gameManager.enemySlimeColor = stats.color;
            gameManager.enemySlimeName = stats.name;
            gameManager.enemySlimeType = stats.type;
            gameManager.enemyHealth = stats.health;
            gameManager.enemyHunger = stats.hunger;
            gameManager.enemyCurrentHP = stats.currentHP;
            gameManager.enemyDmg = stats.dmg;
            gameManager.enemyVicGold = stats.VicGold;
            gameManager.enemySourAff = stats.sourAff;
            gameManager.enemySpicyAff = stats.spicyAff;
            gameManager.enemySeafoodAff = stats.seafoodAff;
            gameManager.enemyCandyAff = stats.candyAff;
            gameManager.enemyVeggieAff = stats.veggieAff;
            gameManager.enemySize = stats.size;

            gameManager.lastPlayerPos = player.transform.position;

            Camera.main.GetComponent<CameraFollow>().enabled = false;
            Camera.main.GetComponent<AudioListener>().enabled = false; // Need to reenable after a battle.
            SceneManager.LoadScene("Battle");
        }
    }
}
