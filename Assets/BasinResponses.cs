﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysWaterSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;
    public FMOD.Studio.EventInstance FillingBasinSound;

    public GameObject FilledBasin;
    

    public GameObject playerCamera;
    private LockView lockViewScript;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysWaterSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/water");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");
        FillingBasinSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/fillingWaterBasin");
        lockViewScript = playerCamera.GetComponent<LockView>();
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
        FillingBasinSound.start();
        
        
        lockViewScript.LockOnToTargetObject(gameObject.transform.position);
        FilledBasin.SetActive(true);
        gameObject.SetActive(false);
    }

    public void IncorrectResponse()
    {
        TurulSaysWaterSound.start();
    }
}
