using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPuzzleResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance WickedWitchCackle;

    public FMOD.Studio.EventInstance SavedChild;

    public GameObject turul;
    private PlayTurulSFX turulSFXScript;

    public GameObject lights;
    private ProgressiveLights lightsScript;

    public GameObject bathroomCutsceneTimeline;
    private TriggerGateClose gateCloseScript;

    public GameObject playerCamera;
    private LockView lockViewScript;

    public GameObject candyPuzzleLoopTriggerObject;
    private PlayCandyPuzzleLoop candyPuzzleLoopScript;

    private float tempCandyPuzzleSoundsValue;

    // Start is called before the first frame update
    void Start()
    {
        

        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        lightsScript = lights.GetComponent<ProgressiveLights>();

        gateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        lockViewScript = playerCamera.GetComponent<LockView>();

        candyPuzzleLoopScript = candyPuzzleLoopTriggerObject.GetComponent<PlayCandyPuzzleLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        candyPuzzleLoopScript.candyPuzzleLoopSound.getParameterValue("CandyPuzzleSounds", out tempCandyPuzzleSoundsValue, out tempCandyPuzzleSoundsValue);
        if (tempCandyPuzzleSoundsValue == 0.5f)
        {
            tempCandyPuzzleSoundsValue = 0.0f;
            candyPuzzleLoopScript.candyPuzzleLoopSound.setParameterValue("CandyPuzzleSounds", 0.0f);
        }
    }

    public void CandyPuzzleSolvedResponse()
    {
        candyPuzzleLoopScript.candyPuzzleLoopSound.setParameterValue("OnOff", 0f);
        candyPuzzleLoopScript.candyPuzzleLoopSound.setParameterValue("CandyPuzzleSounds", 1.0f);

        lightsScript.MakeAmbientCreepier();
        lockViewScript.makeGraphicsGrainier();

        //gateCloseScript.PlayLoopingTurulSquawk();
        PlayLoopingSquawk.TurulLoopsSquawk.start();
        PuzzleManagement.PlayerIsDoingBathroomPuzzle = false;
        PuzzleManagement.PlayerIsDoingCatPuzzle = false;
        PuzzleManagement.PlayerIsDoingElementsPuzzle = true;
        PuzzleManagement.PlayerIsDoingCandyPuzzle = false;
        turulSFXScript.playerHasInteractedWithTurulThisPuzzle = false;
        turulSFXScript.emersionLightningHasStruckThisPuzzle = false;
        gameObject.SetActive(false);
        lockViewScript.HoldItem(PlayerItem.None, null);
    }

    public void CandyPuzzleIncorrectResponse()
    {

        candyPuzzleLoopScript.candyPuzzleLoopSound.setParameterValue("CandyPuzzleSounds", 0.5f);


    }


}
