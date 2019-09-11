using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysWaterSound;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysWaterSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/water");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.WaterBasinSolved = true;
        Debug.Log("Water Basin Solved");
    }

    public void IncorrectResponse()
    {
        TurulSaysWaterSound.start();
    }
}
