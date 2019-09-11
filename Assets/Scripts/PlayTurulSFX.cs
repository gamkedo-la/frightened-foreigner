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
    public FMOD.Studio.EventInstance TurulSaysMedicineSound;
    public FMOD.Studio.EventInstance TurulSaysCandySound;

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
        TurulSaysMedicineSound = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Turul/medicine");
        TurulSaysCandySound = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Turul/candy");

        lightningScript = lightningSystem.GetComponent<EmersionLightningEmmissionToggle>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (LockViewScript.LockedWithTurul)
        {
            //Debug.Log("Locked With Turul" + LockViewScript.LockedWithTurul);
            //sceneManagementScript.PostBathroomMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //Debug.Log("player has interacted with turul: " + playerHasInteractedWithTurulThisPuzzle);
            //Debug.Log("lightning has struck: " + emersionLightningHasStruckThisPuzzle);
            //Debug.Log("player is doing cat puzzle: " + PuzzleManagement.PlayerIsDoingCatPuzzle);
            TriggerGateClose.loopingTurulSquawkSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (!playerHasInteractedWithTurulThisPuzzle && !emersionLightningHasStruckThisPuzzle && PuzzleManagement.PlayerIsDoingCatPuzzle)
            {
                Debug.Log("inside catPuzzle");
                
                
                lightningScript.emitLightningForCatPuzzle();
                playerHasInteractedWithTurulThisPuzzle = true;
                emersionLightningHasStruckThisPuzzle = true;
            }
            if (!playerHasInteractedWithTurulThisPuzzle && !emersionLightningHasStruckThisPuzzle && PuzzleManagement.PlayerIsDoingSicknessPuzzle)
            {
                LockViewScript.sicknessPuzzleCutsceneWithFene = true;
                lightningScript.emitLightningForSicknessPuzzle();
                playerHasInteractedWithTurulThisPuzzle = true;
                emersionLightningHasStruckThisPuzzle = true;
            }
            if (!playerHasInteractedWithTurulThisPuzzle && !emersionLightningHasStruckThisPuzzle && PuzzleManagement.PlayerIsDoingCandyPuzzle)
            {
                LockViewScript.candyPuzzleLightningCutscene = true;
                lightningScript.emitLightningForCandyPuzzle();
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
