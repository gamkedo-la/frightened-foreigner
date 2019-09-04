using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PuzzleManagement : MonoBehaviour
{
    public static bool PlayerIsDoingBathroomPuzzle = false;
    public static bool PlayerIsDoingCatPuzzle = false;
    public static bool PlayerIsDoingSicknessPuzzle = false;
    public static bool PlayerIsDoingCandyPuzzle = false;
    public static bool PlayerIsDoingElementsPuzzle = false;

    public GameObject puzzleJumper;

    public GameObject bathroomStuff;

    public GameObject groundskeeper;

    public GameObject lights;
    private ProgressiveLights lightsScript;

    public GameObject postProcessingValue;
    private PostProcessVolume PPVScript;
    private Grain GrainLayer;
    private Vignette VignetteLayer;
    private float PPVMultiplier = 1.5f;
    private float maxGrainIntensity = 0.5f;
    private float maxVignetteIntensity = 0.45f;

    public GameObject wallInFrontOfBathroom;
    public GameObject bathroomDoor;

    private void Start()
    {
        lightsScript = lights.GetComponent<ProgressiveLights>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!puzzleJumper.active)
            {
                puzzleJumper.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                puzzleJumper.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    }

    public void JumpToCatPuzzle()
    {
        PlayerIsDoingCatPuzzle = true;

        //scene changes
        DoorScript.playerHasExploredTheCemetery = true;
        bathroomStuff.SetActive(true);
        bathroomDoor.SetActive(false);
        wallInFrontOfBathroom.SetActive(false);
        groundskeeper.SetActive(false);
        DoorScript.BathroomCutsceneHasPlayed = true;//triggers gate closing

        //dialogue progression
        DialogManager.PlayerHasAskedWhereTheBathroomIs = true;
        DialogManager.PlayerHasLearnedWordForBathroom = true;
        DialogManager.GroundskeeperSaidThere = true;
        DialogManager.ITriedToFindTheBathroomPlayed = true;
        DialogManager.HeDoesntKnowEnglishHasPlayed = true;
        DialogManager.learnedFurduszoba = true;
        DialogManager.BathroomAttendantSaidToGetForint = true;
        
        //lets get darker twice because player has solved the word for bathroom and forint
        lightsScript.MakeAmbientCreepier();
        lightsScript.MakeAmbientCreepier();

        //scene gets grainier twice because player has solved the word for bathroom and forint
        makeGraphicsGrainier();
        makeGraphicsGrainier();

        //change the music
        SceneManagement.TitleScreenMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManagement.PostFirstPuzzleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManagement.PostBathroomMusic.start();

        //make turul squawk
        TriggerGateClose.loopingTurulSquawkSound.start();
    }

    private void makeGraphicsGrainier()
    {
        PPVScript = postProcessingValue.GetComponent<PostProcessVolume>();
        PPVScript.profile.TryGetSettings<Grain>(out GrainLayer);
        PPVScript.profile.TryGetSettings<Vignette>(out VignetteLayer);
        //Debug.Log(ambientOcclusionLayer);
        GrainLayer.intensity.Override(GrainLayer.intensity * PPVMultiplier);
        VignetteLayer.intensity.Override(VignetteLayer.intensity * PPVMultiplier);
        if (GrainLayer.intensity > maxGrainIntensity)
        {
            GrainLayer.intensity.Override(maxGrainIntensity);
        }
        if (VignetteLayer.intensity > maxVignetteIntensity)
        {
            VignetteLayer.intensity.Override(maxVignetteIntensity);
        }
    }
}
