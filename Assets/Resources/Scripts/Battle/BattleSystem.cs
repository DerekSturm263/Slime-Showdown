using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    bool isDead;
    public GameObject enemHealthBarFill;
    public GameObject playHealthBarFill;
    public GameObject optionButtons;
    public GameObject moveSelect;

    public GameObject inventory;
    public GameObject backButton;
    public GameObject background;
    public GameObject snackButton;

    public GameObject move1;
    public GameObject move2;
    public GameObject move3;

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

    private bool isSnacksOpen = false;

    public GameObject selectedInventorySlot;

    private void Start()
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
            playerPrefab.GetComponent<Player>().type = "Type: Air";
            playerAffTypeString = "Air";
            playerTypeHigh = playerPrefab.GetComponent<Player>().candyAff;
        }
        else if(playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().sourAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type: Electric";
            playerAffTypeString = "Electric";
            playerTypeHigh = playerPrefab.GetComponent<Player>().sourAff;
        }
        else if (playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().veggieAff && playerPrefab.GetComponent<Player>().spicyAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type: Fire";
            playerAffTypeString = "Fire";
            playerTypeHigh = playerPrefab.GetComponent<Player>().spicyAff;
        }
        else if (playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().veggieAff > playerPrefab.GetComponent<Player>().seafoodAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type: Earth";
            playerAffTypeString = "Earth";
            playerTypeHigh = playerPrefab.GetComponent<Player>().veggieAff;
        }
        else if (playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().candyAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().sourAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().spicyAff && playerPrefab.GetComponent<Player>().seafoodAff > playerPrefab.GetComponent<Player>().veggieAff)
        {
            playerPrefab.GetComponent<Player>().type = "Type: Water";
            playerAffTypeString = "Water";
            playerTypeHigh = playerPrefab.GetComponent<Player>().seafoodAff;
        }
        else
        {
            playerPrefab.GetComponent<Player>().type = "Type: Normal";
            playerTypeHigh = (playerPrefab.GetComponent<Player>().seafoodAff + playerPrefab.GetComponent<Player>().veggieAff + playerPrefab.GetComponent<Player>().spicyAff + playerPrefab.GetComponent<Player>().sourAff + playerPrefab.GetComponent<Player>().candyAff) / 5;
        }
        playerPrefab.GetComponent<Player>().health = 50 + playerTypeHigh;
        playerPrefab.GetComponent<Player>().currentHP = playerPrefab.GetComponent<Player>().health;
        playerPrefab.GetComponent<Player>().hunger = 50 + playerTypeHigh;
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
            enemyPrefab.GetComponent<Player>().type = "Type: Air";
            enemyAffType = "Air";
            enemyCritType = "Earth";
            enemyResType = "Fire";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().candyAff;
        }
        else if (enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().sourAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type: Electric";
            enemyAffType = "Electric";
            enemyCritType = "Water";
            enemyResType = "Earth";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().sourAff;
        }
        else if (enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().veggieAff && enemyPrefab.GetComponent<Player>().spicyAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type: Fire";
            enemyAffType = "Fire";
            enemyCritType = "Air";
            enemyResType = "Water";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().spicyAff;
        }
        else if (enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().veggieAff > enemyPrefab.GetComponent<Player>().seafoodAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type: Earth";
            enemyAffType = "Earth";
            enemyCritType = "Electric";
            enemyResType = "Air";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().veggieAff;
        }
        else if (enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().candyAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().sourAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().spicyAff && enemyPrefab.GetComponent<Player>().seafoodAff > enemyPrefab.GetComponent<Player>().veggieAff)
        {
            enemyPrefab.GetComponent<Player>().type = "Type: Water";
            enemyAffType = "Water";
            enemyCritType = "Fire";
            enemyResType = "Electric";
            enemyTypeHigh = enemyPrefab.GetComponent<Player>().seafoodAff;
        }
        else
        {
            enemyPrefab.GetComponent<Player>().type = "Type: Normal";
            enemyCritType = "NONE";
            enemyResType = "NONE";
            enemyTypeHigh = (enemyPrefab.GetComponent<Player>().seafoodAff + enemyPrefab.GetComponent<Player>().veggieAff + enemyPrefab.GetComponent<Player>().spicyAff + enemyPrefab.GetComponent<Player>().sourAff + enemyPrefab.GetComponent<Player>().candyAff) / 5;
        }
        enemyPrefab.GetComponent<Player>().health = 30 + playerTypeHigh;
        enemyPrefab.GetComponent<Player>().currentHP = enemyPrefab.GetComponent<Player>().health;
        #endregion
        state = BattleState.Start;


        moveList.Add(move1);
        moveList.Add(move2);
        moveList.Add(move3);

        moveTextList.Add(move1Text);
        moveTextList.Add(move2Text);
        moveTextList.Add(move3Text);

        Debug.Log(gameManager.GetComponent<GameManager>().PlayerMoves[0].Name);
        Debug.Log(gameManager.GetComponent<GameManager>().PlayerMoves[1].Name);
        Debug.Log(gameManager.GetComponent<GameManager>().PlayerMoves[2].Name);

        Debug.Log(moveTextList[0].text);
        Debug.Log(moveTextList[1].text);
        Debug.Log(moveTextList[2].text);

        
        for (int i = 0; i <= 2; i++)
        {
            if (gameManager.GetComponent<GameManager>().PlayerMoves[i].AffType == "Water")
                moveList[i].GetComponent<Image>().color = waterColor;
            else if (gameManager.GetComponent<GameManager>().PlayerMoves[i].AffType == "Fire")
                moveList[i].GetComponent<Image>().color = fireColor;
            else if (gameManager.GetComponent<GameManager>().PlayerMoves[i].AffType == "Air")
                moveList[i].GetComponent<Image>().color = airColor;
            else if (gameManager.GetComponent<GameManager>().PlayerMoves[i].AffType == "Earth")
                moveList[i].GetComponent<Image>().color = earthColor;
            else if (gameManager.GetComponent<GameManager>().PlayerMoves[i].AffType == "Electric")
                moveList[i].GetComponent<Image>().color = electricColor;
            else
                moveList[i].GetComponent<Image>().color = normalColor;

            moveTextList[i].text = gameManager.GetComponent<GameManager>().PlayerMoves[i].Name;
        }

        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && isSnacksOpen)
        {
            CloseSnacks();
        }
    }

    IEnumerator SetupBattle()
    {
        string playerAnimName = "Animations/Slime/Battle/" + gameManager.GetComponent<GameManager>().playerSlimeColor + " Slime/slime_" + gameManager.GetComponent<GameManager>().playerSlimeColor.ToLower() + "_battle_con";
        string enemyAnimName = "Animations/Slime/Battle/" + gameManager.GetComponent<GameManager>().enemySlimeColor + " Slime/slime_" + gameManager.GetComponent<GameManager>().enemySlimeColor.ToLower() + "_battle_con";

        GameObject playerGO = Instantiate(playerPrefab, playSpawn);
        playerUnit = playerGO.GetComponent<Player>();

        GameObject enemyGO =Instantiate(enemyPrefab, EnemySpawn);
        enemyUnit = enemyGO.GetComponent<Player>();

        dialogueText.text = enemyUnit.name + " challenges you to a battle!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        playerGO.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(playerAnimName) as RuntimeAnimatorController;
        enemyGO.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(enemyAnimName) as RuntimeAnimatorController;

        playerGO.transform.localScale = new Vector2(gameManager.GetComponent<GameManager>().playerSize * 2f, gameManager.GetComponent<GameManager>().playerSize * 2f);
        enemyGO.transform.localScale = new Vector2(gameManager.GetComponent<GameManager>().enemySize * 2f, gameManager.GetComponent<GameManager>().enemySize * 2f);

        enemyGO.GetComponent<SpriteRenderer>().flipX = true;

        yield return new WaitForSeconds(2.5f);

        #region Inventory Setup

        for (int i = 0; i < gameManager.GetComponent<GameManager>().inventory.Length; i++)
        {
            if (gameManager.GetComponent<GameManager>().inventory[i] != null)
            {
                inventory.GetComponent<InventoryManager>().inventorySlots[i].GetComponent<Image>().sprite = gameManager.GetComponent<GameManager>().inventory[i].GetComponent<SpriteRenderer>().sprite;

                break;
            }
        }

        #endregion

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
            if (isDead)
            {
                enemHealthBarFill.SetActive(false);

            }
            else
            {
                enemyHUD.SetHealth(enemyUnit.currentHP);
            }
            dialogueText.text = "The " + attack.Name + " hit!";
            //playerUnit.GetComponent<Player>().spicyAff;
        }
        else if (attack.AffType == "Water")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().seafoodAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            if (isDead)
            {
                enemHealthBarFill.SetActive(false);

            }
            else
            {
                enemyHUD.SetHealth(enemyUnit.currentHP);
            }
            dialogueText.text = "The " + attack.Name +  " hit!";
            // playerUnit.GetComponent<Player>().seafoodAff;
        }
        else if (attack.AffType == "Earth")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().veggieAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            if (isDead)
            {
                enemHealthBarFill.SetActive(false);

            }
            else
            {
                enemyHUD.SetHealth(enemyUnit.currentHP);
            }
            dialogueText.text = "The " + attack.Name + " hit!";
            //playerUnit.GetComponent<Player>().veggieAff;
        }
        else if (attack.AffType == "Air")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().candyAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            if (isDead)
            {
                enemHealthBarFill.SetActive(false);

            }
            else
            {
                enemyHUD.SetHealth(enemyUnit.currentHP);
            }
            dialogueText.text = "The " + attack.Name + " hit!";
            // playerUnit.GetComponent<Player>().candyAff;
        }
        else if (attack.AffType == "Electric")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack, playerUnit.GetComponent<Player>().sourAff, enemyTypeHigh, enemyAffType));
            //updates enemy stats
            if (isDead)
            {
                enemHealthBarFill.SetActive(false);

            }
            else
            {
                enemyHUD.SetHealth(enemyUnit.currentHP);
            }
            dialogueText.text = "The " + attack.Name + " hit!";
            // playerUnit.GetComponent<Player>().sourAff;
        }
        else if(attack.AffType == "Normal")
        {
            //damage enemy and check if dead
            isDead = enemyUnit.TakeDamage(attack.MoveUse(attack , playerTypeHigh, enemyTypeHigh, enemyAffType));
            if (isDead)
            {
                enemHealthBarFill.SetActive(false);

            }
            //updates enemy stats
            else
            {
                enemyHUD.SetHealth(enemyUnit.currentHP);
            }
            dialogueText.text = "The " + attack.Name + " hit!";
        }
        else
        {
            Debug.Log("You named something wrong Joe.");
        }
       /* this stops stuff for 2 seconds for read time*/ yield return new WaitForSeconds(2.5f);
        if (isDead)
        {
           
            state = BattleState.Win;
            
            StartCoroutine(EndBattle());
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
        yield return new WaitForSeconds(2.5f);

        isDead = playerUnit.TakeDamage(gameManager.GetComponent<GameManager>().MoveRoll.MoveUse(enemyResType,enemyCritType,enemyPowerLevel, enemyTypeHigh, playerTypeHigh, playerAffTypeString));
        playerHUD.SetHealth(playerUnit.currentHP);
        yield return new WaitForSeconds(2.5f);

        if (isDead)
        {
            playHealthBarFill.SetActive(false);
            state = BattleState.Lose;
            StartCoroutine(EndBattle());
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
        dialogueText.text = "You feel better after your snack.";

        yield return new WaitForSeconds(2.5f);

        state = BattleState.EnTurn;
        StartCoroutine(EnemyTurn());

    }
    private IEnumerator EndBattle()
    {
        if (state == BattleState.Win)
        {
            dialogueText.text = "You beat " + enemyUnit.name + " and earned " + (uint)enemyPrefab.GetComponent<Player>().VicGold + " gold!";
            gameManager.GetComponent<GameManager>().goldCount += (uint)enemyPrefab.GetComponent<Player>().VicGold;

            MusicPlayer.Stop();
            SoundPlayer.Play("music_victoryTheme");

            yield return new WaitForSeconds(2.5f);

            Camera.main.GetComponent<CameraFollow>().enabled = true;
            Camera.main.GetComponent<AudioListener>().enabled = true;
            SceneManager.LoadScene("Ranch");
        }
        else if (state == BattleState.Lose)
        {
            dialogueText.text = "You lost against " + enemyUnit.name + "...";

            yield return new WaitForSeconds(2.5f);

            Camera.main.GetComponent<CameraFollow>().enabled = true;
            Camera.main.GetComponent<AudioListener>().enabled = true;
            SceneManager.LoadScene("Ranch");
        }
    }
    void PlayerTurn()
    {
        dialogueText.GetComponent<Animation>().Play("ui_button_floatOut");
        optionButtons.GetComponent<Animation>().Play("ui_button_floatIn");

        EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("AttackButton"));
    }
    
    public void OnAttackButton()
    {
        if(state != BattleState.PlayTurn)
        {
            return;
        }
        optionButtons.GetComponent<Animation>().Play("ui_button_floatOut");
        moveSelect.GetComponent<Animation>().Play("ui_button_floatIn");

        EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Move1"));
    }

    public void OnMoveOne()
    {
        StartCoroutine(MoveOneCoroutine());
    }

    public void OnMoveTwo()
    {
        StartCoroutine(MoveTwoCoroutine());
    }

    public void OnMoveThree()
    {
        StartCoroutine(MoveThreeCoroutine());
    }

    private IEnumerator MoveOneCoroutine()
    {
        if (playerUnit.hunger < gameManager.GetComponent<GameManager>().PlayerMoves[0].HungerCost)
        {
            moveSelect.GetComponent<Animation>().Play("ui_button_floatOut");
            dialogueText.GetComponent<Animation>().Play("ui_button_floatIn");
            dialogueText.text = "You're too hungry to use this move! You should eat...";
            yield return new WaitForSeconds(2.5f);
            dialogueText.GetComponent<Animation>().Play("ui_button_floatOut");
            moveSelect.GetComponent<Animation>().Play("ui_button_floatIn");
            EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Move1"));

        }
        else
        {
            moveSelect.GetComponent<Animation>().Play("ui_button_floatOut");
            dialogueText.GetComponent<Animation>().Play("ui_button_floatIn");
            StartCoroutine(PlayerAttack(gameManager.GetComponent<GameManager>().PlayerMoves[0]));
            playerUnit.hunger -= gameManager.GetComponent<GameManager>().PlayerMoves[0].HungerCost;
            playerHUD.SetHunger(playerUnit.hunger);
        }
    }

    private IEnumerator MoveTwoCoroutine()
    {
        if (playerUnit.hunger < gameManager.GetComponent<GameManager>().PlayerMoves[1].HungerCost)
        {
            moveSelect.GetComponent<Animation>().Play("ui_button_floatOut");
            dialogueText.GetComponent<Animation>().Play("ui_button_floatIn");
            dialogueText.text = "You're too hungry to use this move! You should eat...";
            yield return new WaitForSeconds(2.5f);
            dialogueText.GetComponent<Animation>().Play("ui_button_floatOut");
            moveSelect.GetComponent<Animation>().Play("ui_button_floatIn");
            EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Move2"));

        }
        else
        {
            moveSelect.GetComponent<Animation>().Play("ui_button_floatOut");
            dialogueText.GetComponent<Animation>().Play("ui_button_floatIn");
            StartCoroutine(PlayerAttack(gameManager.GetComponent<GameManager>().PlayerMoves[1]));
            playerUnit.hunger -= gameManager.GetComponent<GameManager>().PlayerMoves[1].HungerCost;
            playerHUD.SetHunger(playerUnit.hunger);
        }
    }

    private IEnumerator MoveThreeCoroutine()
    {
        if (playerUnit.hunger < gameManager.GetComponent<GameManager>().PlayerMoves[2].HungerCost)
        {
            moveSelect.GetComponent<Animation>().Play("ui_button_floatOut");
            dialogueText.GetComponent<Animation>().Play("ui_button_floatIn");
            dialogueText.text = "You're too hungry to use this move! You should eat...";
            yield return new WaitForSeconds(2.5f);
            dialogueText.GetComponent<Animation>().Play("ui_button_floatOut");
            moveSelect.GetComponent<Animation>().Play("ui_button_floatIn");
            EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Move3"));

        }
        else
        {
            moveSelect.GetComponent<Animation>().Play("ui_button_floatOut");
            dialogueText.GetComponent<Animation>().Play("ui_button_floatIn");
            StartCoroutine(PlayerAttack(gameManager.GetComponent<GameManager>().PlayerMoves[2]));
            playerUnit.hunger -= gameManager.GetComponent<GameManager>().PlayerMoves[2].HungerCost;
            playerHUD.SetHunger(playerUnit.hunger);
        }
    }

    public void OnReturnButton()
    {
        //this method will be reused for the snack select
        moveSelect.GetComponent<Animation>().Play("ui_button_floatOut");
        optionButtons.GetComponent<Animation>().Play("ui_button_floatIn");
        EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("AttackButton"));
    }
    public void OnSnackButton()
    {
        if (state != BattleState.PlayTurn)
        {
            return;
        }

        selectedInventorySlot = inventory.GetComponent<InventoryManager>().inventorySlots[0];

        isSnacksOpen = true;
        optionButtons.GetComponent<Animation>().Play("ui_button_floatOut");
        inventory.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
        backButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatIn");
        background.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");
    }
    public void OnFleeButton()
    {
        if (state != BattleState.PlayTurn)
        {
            return;
        }

        optionButtons.GetComponent<Animation>().Play("ui_button_floatOut");
        dialogueText.GetComponent<Animation>().Play("ui_button_floatIn");

        StartCoroutine(Leave());
    }

    private IEnumerator Leave()
    {
        if (Random.Range(-1, 2) >= 0)
        {
            dialogueText.text = "You got away!";
            yield return new WaitForSeconds(2.5f);

            Camera.main.GetComponent<CameraFollow>().enabled = true;
            Camera.main.GetComponent<AudioListener>().enabled = true;
            SceneManager.LoadScene("Ranch");
        }
        else
        {
            dialogueText.text = "You tried running away but failed...";
            yield return new WaitForSeconds(2.5f);

            state = BattleState.EnTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    public void CloseSnacks()
    {
        isSnacksOpen = false;

        optionButtons.GetComponent<Animation>().Play("ui_button_floatIn");
        inventory.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        backButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");
        background.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        EventSystem.current.SetSelectedGameObject(snackButton);
    }
}
