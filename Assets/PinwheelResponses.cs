using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinwheelResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysWindSound;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysWindSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/wind");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.PinwheelSolved = true;
    }

    public void IncorrectResponse()
    {
        TurulSaysWindSound.start();
    }
}
