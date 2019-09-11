using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysFireSound;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysFireSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/fire");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.TorchSolved = true;
    }

    public void IncorrectResponse()
    {
        TurulSaysFireSound.start();
    }
}
