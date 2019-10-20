using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPuzzleResponses : MonoBehaviour
{

    
    public GameObject turul;
    private PlayTurulSFX turulSFXScript;

    public GameObject lights;
    private ProgressiveLights lightsScript;

    public GameObject bathroomCutsceneTimeline;
    private TriggerGateClose gateCloseScript;

    public GameObject playerCamera;
    private LockView lockViewScript;

    public GameObject catPuzzleLoopTriggerObject;
    private PlayCatPuzzleLoop catPuzzleLoopScript;

    private float tempCatSoundsValue;

    // Start is called before the first frame update
    void Start()
    {
        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        lightsScript = lights.GetComponent<ProgressiveLights>();

        gateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        lockViewScript = playerCamera.GetComponent<LockView>();

        catPuzzleLoopScript = catPuzzleLoopTriggerObject.GetComponent<PlayCatPuzzleLoop>();
    }

    private void Update()
    {
        catPuzzleLoopScript.catPuzzleLoopSound.getParameterValue("catSounds", out tempCatSoundsValue, out tempCatSoundsValue);
        if (tempCatSoundsValue == 0.5f)
        {
            tempCatSoundsValue = 0.0f;
            catPuzzleLoopScript.catPuzzleLoopSound.setParameterValue("catSounds", 0.0f);
        }
    }

    public void CatPuzzleSolvedResponse()
    {
        catPuzzleLoopScript.catPuzzleLoopSound.setParameterValue("OnOff", 0f);
        catPuzzleLoopScript.catPuzzleLoopSound.setParameterValue("catSounds", 1.0f);
        lightsScript.targetLightIntensity -= lightsScript.targetDimAmount;
        ProgressiveLights.lightsShouldBeDimming = true;

        lightsScript.MakeAmbientCreepier();
        lockViewScript.makeGraphicsGrainier();

        //gateCloseScript.PlayLoopingTurulSquawk();
        PlayLoopingSquawk.TurulLoopsSquawk.start();
        PuzzleManagement.PlayerIsDoingBathroomPuzzle = false;
        PuzzleManagement.PlayerIsDoingCatPuzzle = false;
        PuzzleManagement.PlayerIsDoingSicknessPuzzle = true;
        turulSFXScript.playerHasInteractedWithTurulThisPuzzle = false;
        turulSFXScript.emersionLightningHasStruckThisPuzzle = false;
        gameObject.SetActive(false);
        lockViewScript.HoldItem(PlayerItem.None, null);
    }

    public void CatPuzzleIncorrectResponse()
    {
        catPuzzleLoopScript.catPuzzleLoopSound.setParameterValue("catSounds", 0.5f);
    }


}
