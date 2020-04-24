﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public GameObject move1;

    public bool done = false;

    public MoveClass NewMove;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    public void AffinityCheck(int type, int thresholdOne, int thresholdTwo, int thresholdThree)
    {
        switch (type)
        {
            case 1:

                if (gameManager.GetComponent<GameManager>().playSeafoodAff >= thresholdOne && gameManager.GetComponent<GameManager>().SeaCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SeaCount);
                    gameManager.GetComponent<GameManager>().SeaCount++;
                    Grow(0.025f);
                }
                else if (gameManager.GetComponent<GameManager>().playSeafoodAff >= thresholdTwo && gameManager.GetComponent<GameManager>().SeaCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SeaCount);
                    gameManager.GetComponent<GameManager>().SeaCount++;
                    Grow(0.05f);
                }
                else if (gameManager.GetComponent<GameManager>().playSeafoodAff >= thresholdThree && gameManager.GetComponent<GameManager>().SeaCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SeaCount);
                    gameManager.GetComponent<GameManager>().SeaCount++;
                    Grow(0.1f);
                }
                else
                {
                    done = true;
                }

                break;
            case 2:

                if (gameManager.GetComponent<GameManager>().playCandyAff >= thresholdOne && gameManager.GetComponent<GameManager>().CandyCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().CandyCount);
                    gameManager.GetComponent<GameManager>().CandyCount++;
                    Grow(0.025f);
                }
                else if (gameManager.GetComponent<GameManager>().playCandyAff >= thresholdTwo && gameManager.GetComponent<GameManager>().CandyCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().CandyCount);
                    gameManager.GetComponent<GameManager>().CandyCount++;
                    Grow(0.05f);
                }
                else if (gameManager.GetComponent<GameManager>().playCandyAff >= thresholdThree && gameManager.GetComponent<GameManager>().CandyCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().CandyCount);
                    gameManager.GetComponent<GameManager>().CandyCount++;
                    Grow(0.1f);
                }
                else
                {
                    done = true;
                }

                break;
            case 3:

                if (gameManager.GetComponent<GameManager>().playSpicyAff >= thresholdOne && gameManager.GetComponent<GameManager>().SpicyCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SpicyCount);
                    gameManager.GetComponent<GameManager>().SpicyCount++;
                    Grow(0.025f);
                }
                else if (gameManager.GetComponent<GameManager>().playSpicyAff >= thresholdTwo && gameManager.GetComponent<GameManager>().SpicyCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SpicyCount);
                    gameManager.GetComponent<GameManager>().SpicyCount++;
                    Grow(0.05f);
                }
                else if (gameManager.GetComponent<GameManager>().playSpicyAff >= thresholdThree && gameManager.GetComponent<GameManager>().SpicyCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SpicyCount);
                    gameManager.GetComponent<GameManager>().SpicyCount++;
                    Grow(0.1f);
                }
                else
                {
                    done = true;
                }

                break;
            case 4:

                if (gameManager.GetComponent<GameManager>().playVeggieAff >= thresholdOne && gameManager.GetComponent<GameManager>().VeggieCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().VeggieCount);
                    gameManager.GetComponent<GameManager>().VeggieCount++;
                    Grow(0.025f);
                }
                else if (gameManager.GetComponent<GameManager>().playVeggieAff >= thresholdTwo && gameManager.GetComponent<GameManager>().VeggieCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().VeggieCount);
                    gameManager.GetComponent<GameManager>().VeggieCount++;
                    Grow(0.05f);
                }
                else if (gameManager.GetComponent<GameManager>().playVeggieAff >= thresholdThree && gameManager.GetComponent<GameManager>().VeggieCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().VeggieCount);
                    gameManager.GetComponent<GameManager>().VeggieCount++;
                    Grow(0.1f);
                }
                else
                {
                    done = true;
                }

                break;
            case 5:

                if (gameManager.GetComponent<GameManager>().playSourAff >= thresholdOne && gameManager.GetComponent<GameManager>().SourCount < 1)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SourCount);
                    gameManager.GetComponent<GameManager>().SourCount++;
                    Grow(0.025f);
                }
                else if (gameManager.GetComponent<GameManager>().playSourAff >= thresholdTwo && gameManager.GetComponent<GameManager>().SourCount < 2)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SourCount);
                    gameManager.GetComponent<GameManager>().SourCount++;
                    Grow(0.05f);
                }
                else if (gameManager.GetComponent<GameManager>().playSourAff >= thresholdThree && gameManager.GetComponent<GameManager>().SourCount < 3)
                {
                    ReplaceMove(type, gameManager.GetComponent<GameManager>().SourCount);
                    gameManager.GetComponent<GameManager>().SourCount++;
                    Grow(0.1f);
                }
                else
                {
                    done = true;
                }

                break;

            default:
                done = true;
                break;
        }
    } // Aff check

    public void ReplaceMove(int type, int typeCount)
    {
        M0Name.text = gameManager.GetComponent<GameManager>().PlayerMoves[0].Name;
        M1Name.text = gameManager.GetComponent<GameManager>().PlayerMoves[1].Name;
        M2Name.text = gameManager.GetComponent<GameManager>().PlayerMoves[2].Name;

        topArea.GetComponent<Animation>().Play("ui_ranch_shopTop_floatIn");
        moveInfo.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
        moves.GetComponent<Animation>().Play("ui_button_floatIn");

        EventSystem.current.SetSelectedGameObject(move1);

        switch (type)
        {
            case 1:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveSplash.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveSplash.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MoveSplash;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveWaterBall.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveWaterBall.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveWaterBall;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveWaterHose.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveWaterHose.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveWaterHose;
                }
                break;

            case 2:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveBlow.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveBlow.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MoveBlow;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveAirCutter.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveAirCutter.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveAirCutter;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveWindBlade.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveWindBlade.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveWindBlade;
                }
                break;

            case 3:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MovePepperSpray.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MovePepperSpray.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MovePepperSpray;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveFlameShot.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveFlameShot.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveFlameShot;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveFireBall.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveFireBall.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveFireBall;
                }
                break;

            case 4:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MovePebbleSpit.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MovePebbleSpit.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MovePebbleSpit;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveRockToss.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveRockToss.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveRockToss;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveSeismicSmash.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveSeismicSmash.AffType;
                    NewMovePwr.text = "Power Level: 3";
                    NewMove = gameManager.GetComponent<GameManager>().MoveSeismicSmash;
                }
                break;

            case 5:
                if (typeCount < 1)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveZap.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveZap.AffType;
                    NewMovePwr.text = "Power Level: 1";
                    NewMove = gameManager.GetComponent<GameManager>().MoveZap;
                }
                else if (typeCount < 2)
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveShock.Name;
                    NewMoveType.text = "Type: " + gameManager.GetComponent<GameManager>().MoveShock.AffType;
                    NewMovePwr.text = "Power Level: 2";
                    NewMove = gameManager.GetComponent<GameManager>().MoveShock;
                }
                else
                {
                    NewMoveName.text = gameManager.GetComponent<GameManager>().MoveThunderShock.Name;
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
        done = true;

        topArea.GetComponent<Animation>().Play("ui_ranch_shopTop_floatOut");
        moveInfo.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        moves.GetComponent<Animation>().Play("ui_button_floatOut");
    }

    private void Grow(float size)
    {
        gameManager.GetComponent<GameManager>().playerSize += size;
        GameObject.FindGameObjectWithTag("RanchBattleSlime").transform.localScale = new Vector2(gameManager.GetComponent<GameManager>().playerSize, gameManager.GetComponent<GameManager>().playerSize);
    }
}
