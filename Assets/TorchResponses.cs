using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysFireSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;
    public GameObject torchFlame;
    

    public GameObject playerCamera;
    private LockView lockViewScript;

    public GameObject textGraphic;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysFireSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/fire");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");
        lockViewScript = playerCamera.GetComponent<LockView>();
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
        
        lockViewScript.LockOnToTargetObject(gameObject.transform.position);

        MouseClicks.currentCorrectAnswer = "Tűz";

        textGraphic.transform.position = gameObject.transform.position;
        Vector3 correction = new Vector3(0, 0.65f, 0);
        textGraphic.transform.position += correction;
        textGraphic.SetActive(true);
    }

    public void IncorrectResponse()
    {
        TurulSaysFireSound.start();
    }
}
