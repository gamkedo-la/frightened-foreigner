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

    // Start is called before the first frame update
    void Start()
    {
        SavedChild = FMODUnity.RuntimeManager.CreateInstance("event:/CandyPuzzle/ThankYou");
        WickedWitchCackle = FMODUnity.RuntimeManager.CreateInstance("event:/CandyPuzzle/Cackle");

        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        lightsScript = lights.GetComponent<ProgressiveLights>();

        gateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        lockViewScript = playerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CandyPuzzleSolvedResponse()
    {
        SavedChild.start();
        lightsScript.MakeAmbientCreepier();
        lockViewScript.makeGraphicsGrainier();

        gateCloseScript.PlayLoopingTurulSquawk();
        PuzzleManagement.PlayerIsDoingBathroomPuzzle = false;
        PuzzleManagement.PlayerIsDoingCatPuzzle = false;
        PuzzleManagement.PlayerIsDoingSicknessPuzzle = true;
        turulSFXScript.playerHasInteractedWithTurulThisPuzzle = false;
        turulSFXScript.emersionLightningHasStruckThisPuzzle = false;
        gameObject.SetActive(false);
        lockViewScript.HoldItem(PlayerItem.None, null);
    }

    public void CandyPuzzleIncorrectResponse()
    {
        WickedWitchCackle.start();
    }
}
