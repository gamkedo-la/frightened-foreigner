using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinwheelResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysWindSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;
    

    public GameObject playerCamera;
    private LockView lockViewScript;

    public GameObject pinwheelSoundsHolder;
    

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysWindSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/wind");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");
        lockViewScript = playerCamera.GetComponent<LockView>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.PinwheelSolved = true;
        Debug.Log("Pinwheel Solved!");
        TurulSaysIgenSound.start();
        pinwheelSoundsHolder.SetActive(true);
        
        GetComponent<Animator>().enabled = true;
        lockViewScript.LockOnToTargetObject(gameObject.transform.position);
    }

    public void IncorrectResponse()
    {
        TurulSaysWindSound.start();
    }
}
