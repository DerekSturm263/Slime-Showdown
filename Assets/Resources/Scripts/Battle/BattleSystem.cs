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
    public string playerAffTypeString; //for enemy damage
    public int enemyTypeHigh;
    public string enemyAffType;
    public string enemyResType;
    public string enemyCritType;
    public int enemyPowerLevel = 1;//this will be how we make the enemy stronger overtime

    Player playerUnit;
    Player enemyUnit;
    SlimeMove sm;
    bool isDead;
    public GameObject enemHealthBarFill;
    public GameObject playHealthBarFill;
    public GameObject optionButtons;
    public GameObject moveSelect;
    
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
        playerPrefab.GetComponent<Player>().VicGold = gameManager.GetComponent<GameManager>().enemyVicGold;
       if( playerPrefab.GetComponent<Player>().candyAff> playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().candyAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().candyAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().candyAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Air";
            playerAffTypeString = "Air";
            playerTypeHigh = playerPrefab.GetComponent<Player>().candyAff;
        }
        else if(playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Electric";
            playerAffTypeString = "Electric";
            playerTypeHigh = playerPrefab.GetComponent<Player>().sourAff;
        }
        else if (playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Fire";
            playerAffTypeString = "Fire";
            playerTypeHigh = playerPrefab.GetComponent<Player>().spicyAff;
        }
        else if (playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Earth";
            playerAffTypeString = "Earth";
            playerTypeHigh = playerPrefab.GetComponent<Player>().veggieAff;
        }
        else if (playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().veggieAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type:Water";
            playerAffTypeString = "Water";
            playerTypeHigh = playerPrefab.GetComponent<Player>().seafoodAff;
        }
        else
        {
            playerPrefab.GetComponent<Player>().type = "Type:Normal";
            playerTypeHigh = (playerPrefab.GetComponent<Player>().seafoodAff + playerPrefab.GetComponent<Player>().veggieAff + playerPrefab.GetComponent<Player>().spicyAff + playerPrefab.GetComponent<Player>().sourAff + playerPrefab.GetComponent<Player>().candyAff) / 5;
        }
        playerPrefab.GetComponent<Player>().health = 50 + playerTypeHigh;
        playerPrefab.GetComponent<Player>().currentHP = playerPrefab.GetComponent<Player>().health;
        #endregion
        #region pulling from gm enemy
        enemyPrefab.GetComponent<Player>().name = gameManager.GetComponent<GameManager>().enemySlimeName;
        enemyPrefab.GetComponent<Player>().candyAff = (int)gameManager.GetComponent<GameManager>().enemyCandyAff;
        enemyPrefab.GetComponent<Player>().sourAff = (int)gameManager.GetComponent<GameManager>().enemySourAff;
        enemyPrefab.GetComponent<Player>().spicyAff = (int)gameManager.GetComponent<GameManager>().enemySpicyAff;
        enemyPrefab.GetComponent<Player>().veggieAff = (int)gameManager.GetComponent<GameManager>().enemyVeggieAff;
        enemyPrefab.GetComponent<Player>().seafoodAff = (int)gameManager.GetComponent<GameManager>().enemySeafoodAff;
        enemyPrefab.GetComponent<Player>().VicGold = gameManager.GetComponent<GameManager>().enemyVicGold;
        if (enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().candyAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            //move table reference Electric>Water>Fire>Air>Earth>Electric example on how it works, Electric crits against water and electric res when hit by water
            enemyPrefab.GetComponent<Player>().type = "Type:Air";
            enemyAffType = "Air";
            enemyCritType = "Earth";
            enemyResType = "Fire";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().candyAff;
        }
        else if (enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Electric";
            enemyAffType = "Electric";
            enemyCritType = "Water";
            enemyResType = "Earth";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().sourAff;
        }
        else if (enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Fire";
            enemyAffType = "Fire";
            enemyCritType = "Air";
            enemyResType = "Water";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().spicyAff;
        }
        else if (enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Earth";
            enemyAffType = "Earth";
            enemyCritType = "Electric";
            enemyResType = "Air";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().veggieAff;
        }
        else if (enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().veggieAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Water";
            enemyAffType = "Water";
            enemyCritType = "Fire";
            enemyResType = "Electric";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().seafoodAff;
        }
        else
        {
            enemyPrefab.GetComponent<Player>().type = "Type:Normal";
            enemyCritType = "NONE";
            enemyResType = "NONE";
            enemyTypeHigh = (enemyPrefab.GetComponent<Player>().seafoodAff + enemyPrefab.GetComponent<Player>().veggieAff + enemyPrefab.GetComponent<Player>().spicyAff + enemyPrefab.GetComponent<Player>().sourAff + enemyPrefab.GetComponent<Player>().candyAff) / 5;
        }
        enemyPrefab.GetComponent<Player>().health = 30 + playerTypeHigh;
        enemyPrefab.GetComponent<Player>().currentHP = enemyPrefab.GetComponent<Player>().health;
        #endregion
        state = BattleState.Start;
       StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        moveSelect.SetActive(false);
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
    IEnumerator PlayerAttack(MoveClass attack)
    {

        if (attack.AffType == "Fire")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().spicyAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            enemyHUD.SetHealth(enemyUnit.currentHP);
            dialogueText.text = "The attack hit!";
            //playerUnit.GetComponent<Player>().spicyAff;
        }
        else if (attack.AffType == "Water")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().seafoodAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            enemyHUD.SetHealth(enemyUnit.currentHP);
            dialogueText.text = "The attack hit!";
            // playerUnit.GetComponent<Player>().seafoodAff;
        }
        else if (attack.AffType == "Earth")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().veggieAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            enemyHUD.SetHealth(enemyUnit.currentHP);
            dialogueText.text = "The attack hit!";
            //playerUnit.GetComponent<Player>().veggieAff;
        }
        else if (attack.AffType == "Air")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().candyAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            enemyHUD.SetHealth(enemyUnit.currentHP);
            dialogueText.text = "The attack hit!";
            // playerUnit.GetComponent<Player>().candyAff;
        }
        else if (attack.AffType == "Electric")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().sourAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            enemyHUD.SetHealth(enemyUnit.currentHP);
            dialogueText.text = "The attack hit!";
            // playerUnit.GetComponent<Player>().sourAff;
        }
        else if(attack.AffType == "Normal")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack , playerTypeHigh, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            enemyHUD.SetHealth(enemyUnit.currentHP);
            dialogueText.text = "The attack hit!";
        }
        else
        {
            Debug.Log("You named something wrong Joe");
        }
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

        isDead = playerUnit.TakeDamage(gameManager.GetComponent<GameManager>().MoveRoll.MoveUse(enemyResType,enemyCritType,enemyPowerLevel, enemyTypeHigh, playerTypeHigh, playerAffTypeString));
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
            Camera.main.GetComponent<CameraFollow>().enabled = true;
            Camera.main.GetComponent<AudioListener>().enabled = true;

            SceneManager.LoadScene("Ranch");
        } else if (state == BattleState.Lose)
        {
            dialogueText.text = "You have lost...";
            Camera.main.GetComponent<CameraFollow>().enabled = true;
            Camera.main.GetComponent<AudioListener>().enabled = true;
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
        moveSelect.SetActive(true);
        
        
    }
    public void OnMoveOne()
    {
        moveSelect.SetActive(false);
        dialogueText.enabled = true;
        StartCoroutine(PlayerAttack(gameManager.GetComponent<GameManager>().PlayerMoves[0]));
        
    }
    public void OnMoveTwo()
    {
        moveSelect.SetActive(false);
        dialogueText.enabled = true;
        StartCoroutine(PlayerAttack(gameManager.GetComponent<GameManager>().PlayerMoves[1]));
        
    }
    public void OnMoveThree()
    {
        moveSelect.SetActive(false);
        dialogueText.enabled = true;
        StartCoroutine(PlayerAttack(gameManager.GetComponent<GameManager>().PlayerMoves[2]));
        
    }
    public void OnReturnButton()
    {
        //this method will be reused for the snack select
        moveSelect.SetActive(false);
        optionButtons.SetActive(true);
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
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        Camera.main.GetComponent<AudioListener>().enabled = true;
        SceneManager.LoadScene("Ranch");
    }

}
