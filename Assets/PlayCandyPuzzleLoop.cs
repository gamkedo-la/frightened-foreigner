using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCandyPuzzleLoop : MonoBehaviour
{

    public FMOD.Studio.EventInstance candyPuzzleLoopSound;
    public GameObject PlayerCamera;
    private LockView LockViewScript;


    private void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        candyPuzzleLoopSound = FMODUnity.RuntimeManager.CreateInstance("event:/CandyPuzzle/lightCackleLoop");
        LockViewScript = PlayerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        candyPuzzleLoopSound.start();
        
        candyPuzzleLoopSound.setParameterValue("OnOff", 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        //catPuzzleLoopSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        
        candyPuzzleLoopSound.setParameterValue("OnOff", 0f);
    }
}
