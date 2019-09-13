using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinwheelResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysWindSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;


    // Start is called before the first frame update
    void Start()
    {
        TurulSaysWindSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/wind");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.PinwheelSolved = true;
        Debug.Log("Pinwheel Solved!");
        TurulSaysIgenSound.start();
        GetComponent<Animator>().enabled = true;
    }

    public void IncorrectResponse()
    {
        TurulSaysWindSound.start();
    }
}
