using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPotResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysEarthSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;


    // Start is called before the first frame update
    void Start()
    {
        TurulSaysEarthSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/earth");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.FlowerPotSolved = true;
        Debug.Log("Flower Pot Solved");
        TurulSaysIgenSound.start();

    }

    public void IncorrectResponse()
    {
        TurulSaysEarthSound.start();
    }
}
