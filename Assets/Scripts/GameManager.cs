using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public EventSystem eventSystem;

    // The amount of gold that the player has acquired.
    public uint goldCount;

    // The color slime that the player has.
    public string playerSlimeType;

    // The name that the player chose for their slime.
    public string playerSlimeName;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }
}
