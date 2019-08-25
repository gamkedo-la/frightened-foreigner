using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithVanessa : MonoBehaviour
{

    private LockView LockViewScript;
    public GameObject PlayerCamera;

    public GameObject groundskeeper;
    public bool groundskeeperInvisible = true;
    private DialogueWithGroundskeeper dialogueWithGroundskeeperScript;

    public GameObject door;
    private DoorScript doorScript;

    public FMOD.Studio.EventInstance TellPlayerToFindTheBathroom;
    private bool PlayerHasBeenToldToFindBathroomThisInteraction = false;

    public FMOD.Studio.EventInstance ITriedToFindTheBathroom;
    public bool ITriedToFindTheBathroomPlayed = false;

    public FMOD.Studio.EventInstance HeDoesntKnowEnglish;
    public bool HeDoesntKnowEnglishHasPlayed = false;
    public bool learnedFurduszoba = false;

    public GameObject TutorialUIHolder;
    public TutorialUIScript tutorialUIScript;
    private string dialogText;

    private ForceBathroomPuzzleDialogue forceBathroomPuzzleDialogueScript;

    
    private CameraShake CameraShakeScript;

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
        
        if (LockViewScript.LockedWithVanessa && !doorScript.playerHasExploredTheCemetery && !PlayerHasBeenToldToFindBathroomThisInteraction)
        {
            TellPlayerToFindTheBathroom.start();
            PlayerHasBeenToldToFindBathroomThisInteraction = true;
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
            PlayerHasBeenToldToFindBathroomThisInteraction = false;
        }

        if (LockViewScript.LockedWithVanessa && groundskeeperInvisible && doorScript.playerHasExploredTheCemetery)
        {
            groundskeeper.SetActive(true);
            groundskeeperInvisible = false;
            ITriedToFindTheBathroom.start();
            ITriedToFindTheBathroomPlayed = true;
            CameraShakeScript.ResetShakeSettings();
        }


        if (LockViewScript.LockedWithVanessa && dialogueWithGroundskeeperScript.PlayerHasAskedWhereTheBathroomIs && !HeDoesntKnowEnglishHasPlayed)
        {
            HeDoesntKnowEnglish.start();
            HeDoesntKnowEnglishHasPlayed = true;
            learnedFurduszoba = true;
        }
    }
}
