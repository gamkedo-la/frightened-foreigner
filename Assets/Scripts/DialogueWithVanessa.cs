using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithVanessa : MonoBehaviour
{

    private LockView LockViewScript;
    public GameObject PlayerCamera;

    public GameObject groundskeeper;
    //public bool groundskeeperInvisible = true;
    private DialogueWithGroundskeeper dialogueWithGroundskeeperScript;

    public GameObject door;
    private DoorScript doorScript;

    public FMOD.Studio.EventInstance TellPlayerToFindTheBathroom;
    //private bool PlayerHasBeenToldToFindBathroomThisInteraction = false;

    public FMOD.Studio.EventInstance ITriedToFindTheBathroom;
    //public bool ITriedToFindTheBathroomPlayed = false;

    public FMOD.Studio.EventInstance HeDoesntKnowEnglish;
    //public bool HeDoesntKnowEnglishHasPlayed = false;
    public bool learnedFurduszoba = false;

    public GameObject TutorialUIHolder;
    public TutorialUIScript tutorialUIScript;
    private string dialogText;

    private ForceBathroomPuzzleDialogue forceBathroomPuzzleDialogueScript;

    private CameraShake CameraShakeScript;

    public GameObject stormSystem;

    // Start is called before the first frame update
    void Start()
    {

        LockViewScript = PlayerCamera.GetComponent<LockView>();
        doorScript = door.GetComponent<DoorScript>();

        TellPlayerToFindTheBathroom = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Vanessa/TellPlayerToFindTheBathroom");
        ITriedToFindTheBathroom = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/ITriedToFindTheBathroom");
        HeDoesntKnowEnglish = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Vanessa/AskIfSheKnowsWordForBathroom");

        dialogueWithGroundskeeperScript = groundskeeper.GetComponent<DialogueWithGroundskeeper>();

        forceBathroomPuzzleDialogueScript = gameObject.GetComponent<ForceBathroomPuzzleDialogue>();

        tutorialUIScript = TutorialUIHolder.GetComponent<TutorialUIScript>();

        CameraShakeScript = PlayerCamera.GetComponent<CameraShake>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!LockView.AnItemIsBeingHeld)
        {


            if (LockViewScript.LockedWithVanessa && !DoorScript.playerHasExploredTheCemetery && !DialogManager.PlayerHasBeenToldToFindBathroomThisInteraction)
            {
                TellPlayerToFindTheBathroom.start();
                DialogManager.PlayerHasBeenToldToFindBathroomThisInteraction = true;
                forceBathroomPuzzleDialogueScript.timeLeft = 180.0f;
                dialogText = "WASD - Forward, Left, Back, Right movement" + Environment.NewLine +
                            "Mouse movement -First person eyesight movement" + Environment.NewLine +
                            "Left Click - Submit answer" + Environment.NewLine +
                            "Right Click - Scroll through word choices" + Environment.NewLine +
                            "r key - repeat the target word";
                tutorialUIScript.openDialog(dialogText);
            }

            if (!LockViewScript.LockedWithVanessa)
            {
                DialogManager.PlayerHasBeenToldToFindBathroomThisInteraction = false;
            }

            if (LockViewScript.LockedWithVanessa && DialogManager.groundskeeperInvisible && DoorScript.playerHasExploredTheCemetery && !DialogManager.ITriedToFindTheBathroomPlayed)
            {
                groundskeeper.SetActive(true);
                
                DialogManager.groundskeeperInvisible = false;
                ITriedToFindTheBathroom.start();
                DialogManager.ITriedToFindTheBathroomPlayed = true;
                StartCoroutine(CameraShakeScript.DelayedCameraShakeForGroundskeeperLightning());
            }


            if (LockViewScript.LockedWithVanessa && DialogManager.PlayerHasAskedWhereTheBathroomIs && !DialogManager.HeDoesntKnowEnglishHasPlayed)
            {
                HeDoesntKnowEnglish.start();
                DialogManager.HeDoesntKnowEnglishHasPlayed = true;
                learnedFurduszoba = true;
            }
        }
    }
}
