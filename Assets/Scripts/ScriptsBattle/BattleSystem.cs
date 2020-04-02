using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, PlayTurn, EnTurn, Win, Lose }

public class BattleSystem : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
       StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
       GameObject playerGO = Instantiate(playerPrefab, playSpawn);
        playerUnit = playerGO.GetComponent<Player>();


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
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PlayTurn)
        {
            return;
        }
        StartCoroutine(PlayerAttack());
    }
    public void OnSnackButton()
    {
        if (state != BattleState.PlayTurn)
        {
            return;
        }
        StartCoroutine(PlayerHeal());
    }


}
