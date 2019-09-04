using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTurulSFX : MonoBehaviour
{

    private FMOD.Studio.EventInstance TurulSquawkSound;

    public GameObject bathroomCutsceneTimeline;
    private TriggerGateClose triggerGateCloseScript;

    public GameObject mainCamera;
    private LockView LockViewScript;

    private GameObject LevelChanger;
    private SceneManagement sceneManagementScript;

    public FMOD.Studio.EventInstance TurulSaysMilkTejSound;

    public GameObject lightningSystem;
    private EmersionLightningEmmissionToggle lightningScript;
    public bool playerHasInteractedWithTurulThisPuzzle = false;
    public bool emersionLightningHasStruckThisPuzzle = false;

    // Start is called before the first frame update
    void Start()
    {
        TurulSquawkSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TurulSquawk");
        triggerGateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        LockViewScript = mainCamera.GetComponent<LockView>();
        //TurulSquawkSound.start();

        LevelChanger = GameObject.Find("LevelChanger");
        sceneManagementScript = LevelChanger.GetComponent<SceneManagement>();

        TurulSaysMilkTejSound = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Turul/milkTej");

        lightningScript = lightningSystem.GetComponent<EmersionLightningEmmissionToggle>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (LockViewScript.LockedWithTurul)
        {
            Debug.Log("inside locked with Turul");
            Debug.Log(playerHasInteractedWithTurulThisPuzzle);
            Debug.Log(emersionLightningHasStruckThisPuzzle);
            Debug.Log(PuzzleManagement.PlayerIsDoingCatPuzzle);
            //sceneManagementScript.PostBathroomMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            TriggerGateClose.loopingTurulSquawkSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (!playerHasInteractedWithTurulThisPuzzle && !emersionLightningHasStruckThisPuzzle && PuzzleManagement.PlayerIsDoingCatPuzzle)
            {
                Debug.Log("inside catPuzzle");
                
                
                lightningScript.emitLightningForCatPuzzle();
                playerHasInteractedWithTurulThisPuzzle = true;
                emersionLightningHasStruckThisPuzzle = true;
            }
        }
    }

    public void PlayTurulSquawkSound()
    {
        
        TurulSquawkSound.start();
    }
}
