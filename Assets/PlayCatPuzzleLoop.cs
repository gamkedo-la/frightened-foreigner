using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCatPuzzleLoop : MonoBehaviour
{

    public FMOD.Studio.EventInstance catPuzzleLoopSound;
    public GameObject PlayerCamera;
    private LockView LockViewScript;
    // Start is called before the first frame update

    void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;

    }

    void Start()
    {
        catPuzzleLoopSound = FMODUnity.RuntimeManager.CreateInstance("event:/CatPuzzle/CatPuzzleLoop");
        LockViewScript = PlayerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        catPuzzleLoopSound.start();
        Debug.Log("Should hear cat sound");
        catPuzzleLoopSound.setParameterValue("OnOff", 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        //catPuzzleLoopSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Debug.Log("Should not hear cat sound");
        catPuzzleLoopSound.setParameterValue("OnOff", 0f);
    }


    
}
