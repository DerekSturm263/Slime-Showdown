using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowSelectedSlime : MonoBehaviour
{
    EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        // Goes to the position of the slime that you are highlighting.
        transform.position = eventSystem.GetComponent<SelectSlime>().highlightedSlime.GetComponent<Transform>().position + new Vector3(0, 0, 0.1f);
    }
}
