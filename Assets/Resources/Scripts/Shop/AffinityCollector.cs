using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AffinityCollector : MonoBehaviour
{
    private GameObject gameManager;

    public GameObject topArea;
    public GameObject moveInfo;
    public GameObject moves;

    public Text NewMoveName;
    public Text NewMoveType;
    public Text NewMovePwr;

    public Text M0Name;
    public Text M1Name;
    public Text M2Name;

    public bool done = false;

    public MoveClass NewMove;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    public void AffinityCheck(int type, int thresholdOne, int thresholdTwo, int thresholdThree)
    {
        done = false;

        switch (type)
        {
            case 1:

                if (gameManager.GetComponent<GameManager>().playSeafoodAff >= thresholdOne && gameManager.GetComponent<GameManager>().SeaCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SeaCount);
                    gameManager.GetComponent<GameManager>().SeaCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playSeafoodAff >= thresholdTwo && gameManager.GetComponent<GameManager>().SeaCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SeaCount);
                    gameManager.GetComponent<GameManager>().SeaCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playSeafoodAff >= thresholdThree && gameManager.GetComponent<GameManager>().SeaCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SeaCount);
                    gameManager.GetComponent<GameManager>().SeaCount++;
                }

                break;
            case 2:

                if (gameManager.GetComponent<GameManager>().playCandyAff >= thresholdOne && gameManager.GetComponent<GameManager>().CandyCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().CandyCount);
                    gameManager.GetComponent<GameManager>().CandyCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playCandyAff >= thresholdTwo && gameManager.GetComponent<GameManager>().CandyCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().CandyCount);
                    gameManager.GetComponent<GameManager>().CandyCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playCandyAff >= thresholdThree && gameManager.GetComponent<GameManager>().CandyCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().CandyCount);
                    gameManager.GetComponent<GameManager>().CandyCount++;
                }

                break;
            case 3:

                if (gameManager.GetComponent<GameManager>().playSpicyAff >= thresholdOne && gameManager.GetComponent<GameManager>().SpicyCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SpicyCount);
                    gameManager.GetComponent<GameManager>().SpicyCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playSpicyAff >= thresholdTwo && gameManager.GetComponent<GameManager>().SpicyCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SpicyCount);
                    gameManager.GetComponent<GameManager>().SpicyCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playSpicyAff >= thresholdThree && gameManager.GetComponent<GameManager>().SpicyCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SpicyCount);
                    gameManager.GetComponent<GameManager>().SpicyCount++;
                }

                break;
            case 4:

                if (gameManager.GetComponent<GameManager>().playVeggieAff >= thresholdOne && gameManager.GetComponent<GameManager>().VeggieCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().VeggieCount);
                    gameManager.GetComponent<GameManager>().VeggieCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playVeggieAff >= thresholdTwo && gameManager.GetComponent<GameManager>().VeggieCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().VeggieCount);
                    gameManager.GetComponent<GameManager>().VeggieCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playVeggieAff >= thresholdThree && gameManager.GetComponent<GameManager>().VeggieCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().VeggieCount);
                    gameManager.GetComponent<GameManager>().VeggieCount++;
                }

                break;
            case 5:

                if (gameManager.GetComponent<GameManager>().playSourAff >= thresholdOne && gameManager.GetComponent<GameManager>().SourCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SourCount);
                    gameManager.GetComponent<GameManager>().SourCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playSourAff >= thresholdTwo && gameManager.GetComponent<GameManager>().SourCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SourCount);
                    gameManager.GetComponent<GameManager>().SourCount++;
                }
                else if (gameManager.GetComponent<GameManager>().playSourAff >= thresholdThree && gameManager.GetComponent<GameManager>().SourCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SourCount);
                    gameManager.GetComponent<GameManager>().SourCount++;
                }

                break;

            default:

                done = true;

                break;
        }


    } // Aff check

    public void ReplaceMove(int type, int typeCount)
    {
        M0Name.text = gameManager.GetComponent<GameManager>().PlayerMoves[0].name;
        M1Name.text = gameManager.GetComponent<GameManager>().PlayerMoves[1].name;
        M2Name.text = gameManager.GetComponent<GameManager>().PlayerMoves[2].name;

        topArea.GetComponent<Animation>().Play("ui_ranch_shopTop_floatIn");
        moveInfo.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
        moves.GetComponent<Animation>().Play("ui_button_floatIn");

        switch (type)
        {
            case 1:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveSplash.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveSplash.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MoveSplash;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveWaterBall.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveWaterBall.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveWaterBall;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveWaterHose.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveWaterHose.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveWaterHose;
                }
                break;

            case 2:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveBlow.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveBlow.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MoveBlow;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveAirCutter.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveAirCutter.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveAirCutter;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveWindBlade.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveWindBlade.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveWindBlade;
                }
                break;

            case 3:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MovePepperSpray.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MovePepperSpray.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MovePepperSpray;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveFlameShot.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveFlameShot.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveFlameShot;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveFireBall.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveFireBall.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveFireBall;
                }
                break;

            case 4:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MovePebbleSpit.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MovePebbleSpit.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MovePebbleSpit;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveRockToss.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveRockToss.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveRockToss;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveSeismicSmash.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveSeismicSmash.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveSeismicSmash;
                }
                break;

            case 5:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveZap.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveZap.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MoveZap;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveShock.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveShock.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveShock;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveThunderShock.name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveThunderShock.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveThunderShock;
                }
                break;
        }

    } // Moveset up end below are the buttons that physically replace the move in the list

    public void OnM0Replace()
    {
        gameManager.GetComponent<GameManager>().PlayerMoves[0] = NewMove;

        GoAway();
    }

    public void OnM1Replace()
    {
        gameManager.GetComponent<GameManager>().PlayerMoves[1] = NewMove;

        GoAway();
    }

    public void OnM2Replace()
    {
        gameManager.GetComponent<GameManager>().PlayerMoves[2] = NewMove;

        GoAway();
    }

    public void Nevermind()
    {
        GoAway();
    }

    private void GoAway()
    {
        topArea.GetComponent<Animation>().Play("ui_ranch_shopTop_floatOut");
        moveInfo.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        moves.GetComponent<Animation>().Play("ui_button_floatOut");

        done = true;
    }
}
