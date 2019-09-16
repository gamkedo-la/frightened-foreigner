using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPuzzleResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance PleasedCat;

    public FMOD.Studio.EventInstance DispleasedCat;

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
        PleasedCat = FMODUnity.RuntimeManager.CreateInstance("event:/CatPuzzle/PleasedCat");
        DispleasedCat = FMODUnity.RuntimeManager.CreateInstance("event:/CatPuzzle/DispleasedCat");

        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        lightsScript = lights.GetComponent<ProgressiveLights>();

        gateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        lockViewScript = playerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatPuzzleSolvedResponse()
    {
        PleasedCat.start();
        
        lightsScript.MakeAmbientCreepier();
        lockViewScript.makeGraphicsGrainier();

        //gateCloseScript.PlayLoopingTurulSquawk();
        turulSFXScript.TurulLoopsSquawk.start();
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
        DispleasedCat.start();
    }
}
