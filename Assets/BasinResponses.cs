using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysWaterSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;
    public GameObject water;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysWaterSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/water");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.WaterBasinSolved = true;
        Debug.Log("Water Basin Solved");
        TurulSaysIgenSound.start();
        water.SetActive(true);
    }

    public void IncorrectResponse()
    {
        TurulSaysWaterSound.start();
    }
}
