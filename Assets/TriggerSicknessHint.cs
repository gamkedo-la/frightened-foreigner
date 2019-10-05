using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSicknessHint : MonoBehaviour
{

    private bool sicknessHintHasBeenGiven = false;
    private bool lighterAndFanHintsHaveBeenGiven = false;
    private FMOD.Studio.EventInstance youDontLookWellHint;
    private FMOD.Studio.EventInstance lighterAndFanHints;


    void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;

        youDontLookWellHint = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/youLookSick");
        lighterAndFanHints = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/fanAndLighterHints");
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

        if (PuzzleManagement.PlayerIsDoingSicknessPuzzle && !sicknessHintHasBeenGiven)
        {
            
            youDontLookWellHint.start();
            sicknessHintHasBeenGiven = true;
        }
        if (PuzzleManagement.PlayerIsDoingElementsPuzzle && !lighterAndFanHintsHaveBeenGiven)
        {
            lighterAndFanHints.start();
            lighterAndFanHintsHaveBeenGiven = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
