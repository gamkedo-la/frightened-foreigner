using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPotResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysEarthSound;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysEarthSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/earth");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.FlowerPotSolved = true;
        Debug.Log("Flower Pot Solved");
    }

    public void IncorrectResponse()
    {
        TurulSaysEarthSound.start();
    }
}
