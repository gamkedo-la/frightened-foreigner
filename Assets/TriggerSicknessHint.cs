using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSicknessHint : MonoBehaviour
{

    private bool hintHasBeenGiven = false;
    private FMOD.Studio.EventInstance youDontLookWellHint;

    void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;

        youDontLookWellHint = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/youLookSick");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has entered sickness puzzle hint zone");
        if (PuzzleManagement.PlayerIsDoingSicknessPuzzle && !hintHasBeenGiven)
        {
            Debug.Log("Player should hear hint");
            youDontLookWellHint.start();
            hintHasBeenGiven = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player has left sickness puzzle hint zone");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("the game thinks you're still in the puzzle hint zone");
    }
}
