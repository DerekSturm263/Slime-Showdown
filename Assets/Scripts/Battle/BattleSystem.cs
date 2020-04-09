﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { Start, PlayTurn, EnTurn, Win, Lose }

public class BattleSystem : MonoBehaviour
{
    private GameObject gameManager;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public BattleState state;

    public Transform playSpawn;
    public Transform EnemySpawn;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;

    Player playerUnit;
    Player enemyUnit;
    SlimeMove sm;

    public GameObject optionButtons;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerPrefab.GetComponent<Player>().name = gameManager.GetComponent<GameManager>().playerSlimeName;

        state = BattleState.Start;
       StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        optionButtons.SetActive(false);
       GameObject playerGO = Instantiate(playerPrefab, playSpawn);
        playerUnit = playerGO.GetComponent<Player>();
        sm = playerUnit.GetComponent<SlimeMove>();
        sm.enabled = false;

       GameObject enemyGO =Instantiate(enemyPrefab, EnemySpawn);
        enemyUnit = enemyGO.GetComponent<Player>();

        dialogueText.text = "An enemy slime approaches....";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);


        yield return new WaitForSeconds(2f);

        state = BattleState.PlayTurn;
        PlayerTurn();
    }
    IEnumerator PlayerAttack()
    {
        //damage enemy and check if dead
        bool isDead = enemyUnit.TakeDamage(playerUnit.dmg);
        //updates enemy stats
        enemyHUD.SetHealth(enemyUnit.currentHP);
        dialogueText.text = "The attack hit!";
       /* this stops stuff for 2 seconds for read time*/ yield return new WaitForSeconds(2f);
        if (isDead)
        {
            state = BattleState.Win;
            EndBattle();
            sm.enabled = true;
        }
        else
        {
            state = BattleState.EnTurn;
            StartCoroutine(EnemyTurn());
        }

        
    }
    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.name + " attacks!";
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.dmg);
        playerHUD.SetHealth(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.Lose;
            EndBattle();
            sm.enabled = true;
            SceneManager.LoadScene("Ranch");
        }
        else
        {
            state = BattleState.PlayTurn;
            PlayerTurn();
        }
    }
    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHealth(playerUnit.currentHP);
        dialogueText.text = "You feel better after your snack";

        yield return new WaitForSeconds(2f);

        state = BattleState.EnTurn;
        StartCoroutine(EnemyTurn());

    }
    void EndBattle()
    {
        if (state == BattleState.Win)
        {
            dialogueText.text = "You have won the battle!";
        } else if (state == BattleState.Lose)
        {
            dialogueText.text = "You have lost...";
        }
    }
    void PlayerTurn()
    {
        
        dialogueText.text = "Choose an action: ";
        dialogueText.enabled = false;
        optionButtons.SetActive(true);
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PlayTurn)
        {
            return;
        }
        optionButtons.SetActive(false);
        dialogueText.enabled = true;
        StartCoroutine(PlayerAttack());
        
    }
    public void OnSnackButton()
    {
        if (state != BattleState.PlayTurn)
        {
            return;
        }
        optionButtons.SetActive(false);
        dialogueText.enabled = true;
        StartCoroutine(PlayerHeal());
        
    }
    public void OnFleeButton()
    {
        if (state != BattleState.PlayTurn)
        {
            return;
        }
        SceneManager.LoadScene("Ranch");
    }

}
