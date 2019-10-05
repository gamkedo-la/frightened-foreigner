using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPotResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysEarthSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;
    public GameObject dirt;
    public GameObject FlowerPotTextGraphic;

    public GameObject playerCamera;
    private LockView lockViewScript;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysEarthSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/earth");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");
        lockViewScript = playerCamera.GetComponent<LockView>();
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
        FlowerPotTextGraphic.SetActive(true);
        dirt.SetActive(true);
        lockViewScript.LockOnToTargetObject(gameObject.transform.position);
    }

    public void IncorrectResponse()
    {
        TurulSaysEarthSound.start();
    }
}
