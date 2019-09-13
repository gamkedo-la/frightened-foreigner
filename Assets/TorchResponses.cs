using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysFireSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;
    public GameObject torchFlame;


    // Start is called before the first frame update
    void Start()
    {
        TurulSaysFireSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/fire");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.TorchSolved = true;
        Debug.Log("Torch Solved!");
        TurulSaysIgenSound.start();
        torchFlame.SetActive(true);
    }

    public void IncorrectResponse()
    {
        TurulSaysFireSound.start();
    }
}
