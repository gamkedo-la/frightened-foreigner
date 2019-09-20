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

    // Start is called before the first frame update
    void Start()
    {
        SavedChild = FMODUnity.RuntimeManager.CreateInstance("event:/CandyPuzzle/ThankYou");
        WickedWitchCackle = FMODUnity.RuntimeManager.CreateInstance("event:/CandyPuzzle/Cackle");

        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        lightsScript = lights.GetComponent<ProgressiveLights>();

        gateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        lockViewScript = playerCamera.GetComponent<LockView>();

        candyPuzzleLoopScript = candyPuzzleLoopTriggerObject.GetComponent<PlayCandyPuzzleLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CandyPuzzleSolvedResponse()
    {
        candyPuzzleLoopScript.candyPuzzleLoopSound.setParameterValue("OnOff", 0f);
        SavedChild.start();
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
        WickedWitchCackle.start();
        Debug.Log("Should hear cat sound");
        candyPuzzleLoopScript.candyPuzzleLoopSound.setParameterValue("OnOff", 0f);
        StartCoroutine(delayCandyPuzzleLoopSound());
    }

    private IEnumerator delayCandyPuzzleLoopSound()
    {
        yield return new WaitForSeconds(3.25f);
        candyPuzzleLoopScript.candyPuzzleLoopSound.setParameterValue("OnOff", 1f);
    }
}
