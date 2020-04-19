using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { Start, PlayTurn, EnTurn, Win, Lose }

public class BattleSystem : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public BattleState state;

    public Transform playSpawn;
    public Transform EnemySpawn;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;
    public int playerTypeHigh;
    public int enemyTypeHigh;

    Player playerUnit;
    Player enemyUnit;
    SlimeMove sm;

    public GameObject enemHealthBarFill;
    public GameObject playHealthBarFill;
    public GameObject optionButtons;
    // Start is called before the first frame update
    void Start()
    {
        playHealthBarFill.SetActive(true);
        enemHealthBarFill.SetActive(true);
        #region pulling from gm player
        //note from afterwards, I should've assigned them all to variables for shorter calls during the check
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerPrefab.GetComponent<Player>().name = gameManager.GetComponent<GameManager>().playerSlimeName;
        playerPrefab.GetComponent<Player>().candyAff = (int)gameManager.GetComponent<GameManager>().playCandyAff;
        playerPrefab.GetComponent<Player>().sourAff = (int)gameManager.GetComponent<GameManager>().playSourAff;
        playerPrefab.GetComponent<Player>().spicyAff = (int)gameManager.GetComponent<GameManager>().playSpicyAff;
        playerPrefab.GetComponent<Player>().veggieAff = (int)gameManager.GetComponent<GameManager>().playVeggieAff;
        playerPrefab.GetComponent<Player>().seafoodAff = (int)gameManager.GetComponent<GameManager>().playSeafoodAff;
        playerPrefab.GetComponent<Player>().VicGold = gameManager.GetComponent<Player>().VicGold;
       if( playerPrefab.GetComponent<Player>().candyAff> playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().candyAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().candyAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().candyAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Air";
            playerTypeHigh = playerPrefab.GetComponent<Player>().candyAff;
        }
        else if(playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Electric";
            playerTypeHigh = playerPrefab.GetComponent<Player>().sourAff;
        }
        else if (playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Fire";
            playerTypeHigh = playerPrefab.GetComponent<Player>().spicyAff;
        }
        else if (playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Earth";
            playerTypeHigh = playerPrefab.GetComponent<Player>().veggieAff;
        }
        else if (playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().veggieAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Water";
            playerTypeHigh = playerPrefab.GetComponent<Player>().seafoodAff;
        }
        else
        {
            playerPrefab.GetComponent<Player>().type = "Type:Normal";
            playerTypeHigh = (playerPrefab.GetComponent<Player>().seafoodAff + playerPrefab.GetComponent<Player>().veggieAff + playerPrefab.GetComponent<Player>().spicyAff + playerPrefab.GetComponent<Player>().sourAff + playerPrefab.GetComponent<Player>().candyAff) / 5;
        }
        playerPrefab.GetComponent<Player>().health = 100 + playerTypeHigh;
        playerPrefab.GetComponent<Player>().currentHP = playerPrefab.GetComponent<Player>().health;
        #endregion
        #region pulling from gm enemy
        enemyPrefab.GetComponent<Player>().name = gameManager.GetComponent<GameManager>().enemySlimeName;
        enemyPrefab.GetComponent<Player>().candyAff = (int)gameManager.GetComponent<GameManager>().enemyCandyAff;
        enemyPrefab.GetComponent<Player>().sourAff = (int)gameManager.GetComponent<GameManager>().enemySourAff;
        enemyPrefab.GetComponent<Player>().spicyAff = (int)gameManager.GetComponent<GameManager>().enemySpicyAff;
        enemyPrefab.GetComponent<Player>().veggieAff = (int)gameManager.GetComponent<GameManager>().enemyVeggieAff;
        enemyPrefab.GetComponent<Player>().seafoodAff = (int)gameManager.GetComponent<GameManager>().enemySeafoodAff;
        enemyPrefab.GetComponent<Player>().VicGold = gameManager.GetComponent<Player>().VicGold;
        if (enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Air";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().candyAff;
        }
        else if (enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Electric";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().sourAff;
        }
        else if (enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Fire";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().spicyAff;
        }
        else if (enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Earth";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().veggieAff;
        }
        else if (enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().veggieAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Water";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().seafoodAff;
        }
        else
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Normal";
            enemyTypeHigh = (enemyPrefab.GetComponent<Player>().seafoodAff + enemyPrefab.GetComponent<Player>().veggieAff + enemyPrefab.GetComponent<Player>().spicyAff + enemyPrefab.GetComponent<Player>().sourAff + enemyPrefab.GetComponent<Player>().candyAff) / 5;
        }
        enemyPrefab.GetComponent<Player>().health = 100 + playerTypeHigh;
        enemyPrefab.GetComponent<Player>().currentHP = enemyPrefab.GetComponent<Player>().health;
        #endregion
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
            enemHealthBarFill.SetActive(false);
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
            playHealthBarFill.SetActive(false);
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
            dialogueText.text = "You have won the battle! you earn " + (uint)enemyPrefab.GetComponent<Player>().VicGold + "Gold";
            gameManager.GetComponent<GameManager>().goldCount += (uint)enemyPrefab.GetComponent<Player>().VicGold;
            SceneManager.LoadScene("Ranch");
        } else if (state == BattleState.Lose)
        {
            dialogueText.text = "You have lost...";
            SceneManager.LoadScene("Ranch");
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
