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
    public FMOD.Studio.EventInstance TurulSaysFireWaterEarthWindSound;
    


    public GameObject lightningSystem;
    private EmersionLightningEmmissionToggle lightningScript;
    public bool playerHasInteractedWithTurulThisPuzzle = false;
    public bool emersionLightningHasStruckThisPuzzle = false;

    // Start is called before the first frame update
    void Start()
    {
        
        triggerGateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        LockViewScript = mainCamera.GetComponent<LockView>();
        

        LevelChanger = GameObject.Find("LevelChanger");
        sceneManagementScript = LevelChanger.GetComponent<SceneManagement>();

        TurulSaysMilkTejSound = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Turul/milkTej");
        TurulSaysMedicineSound = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Turul/medicine");
        TurulSaysCandySound = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Turul/candy");
        TurulSaysFireWaterEarthWindSound = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Turul/fireWaterEarthWind");
        TurulSquawkSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TurulSquawk");



        lightningScript = lightningSystem.GetComponent<EmersionLightningEmmissionToggle>();
        TurulSquawkSound.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (LockViewScript.LockedWithTurul)
        {
            
            PlayLoopingSquawk.TurulLoopsSquawk.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            
            if (!playerHasInteractedWithTurulThisPuzzle && !emersionLightningHasStruckThisPuzzle && PuzzleManagement.PlayerIsDoingCatPuzzle)
            {                    
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
            if (!playerHasInteractedWithTurulThisPuzzle && !emersionLightningHasStruckThisPuzzle && PuzzleManagement.PlayerIsDoingElementsPuzzle)
            {
                LockViewScript.elementsPuzzleLightningCutscene = true;
                lightningScript.emitLightningForElementsPuzzle();
                playerHasInteractedWithTurulThisPuzzle = true;
                emersionLightningHasStruckThisPuzzle = true;
            }
        }
    }

    /*public void PlayTurulSquawkSound()
    {
        
        TurulSquawkSound.start();
    }*/
}
