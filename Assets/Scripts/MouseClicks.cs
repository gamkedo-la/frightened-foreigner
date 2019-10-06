using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MouseClicks: MonoBehaviour
{

    private PauseGame PauseGameScript;
    private GameObject AudioActiveStatusManager;

    private List<string> ImageNameList;//to keep a list of possible word choices
    private int ImageListIndex = 0;

    public RandomWords RandomWordsScript;//where to put list of possible words in memory

    public string temporaryPictureName;//helps cycle through word choices

    private GameObject GhostSoul;//hidden soul to be set free when correct choice is chosen
    private bool GhostSoulVisible = false;

    private GameObject freedSoulsCounter;//keeping track of level progress
    private CountFreedSouls FreedSoulsScript;

    private FMOD.Studio.EventInstance PostFirstPuzzleMusic;
    private FMOD.Studio.EventInstance TitleScreenMusic;
    private GameObject LevelChanger;
    private SceneManagement SceneManagementScript;

    private GameObject Lights;
    private ProgressiveLights LightScript;

    public GameObject PostProccessingValue;
    private PostProcessVolume PPVScript;
    private Grain GrainLayer;
    private Vignette VignetteLayer;
    private float PPVMultiplier = 1.5f;
    private float maxGrainIntensity = 0.5f;
    private float maxVignetteIntensity = 0.45f;

    private LockView LockViewScript;
    public GameObject PlayerCamera;
    public FMOD.Studio.EventInstance GroundskeeperRespondsToIncorrectAnswer;
    public FMOD.Studio.EventInstance GroundskeeperRespondsToCorrectAnswer;

    public GameObject bathroomDoor;
    public GameObject bathroomAttendant;
    public GameObject candyTable;
    public GameObject candyBasket;

    public GameObject bathroomStuff;
    public GameObject temporaryCemeteryWall;

    public FMOD.Studio.EventInstance LightningSound;

    public GameObject Groundskeeper;
    public GameObject thisTextGraphic;
    public GameObject shovel;
    public GameObject Forint;

    public GameObject milk;
    private holdMyInventorySprite holdMySpriteScript;

    public GameObject charlie;
    public GameObject turul;

    private PlayTurulSFX turulSFXScript;

    public FMOD.Studio.EventInstance makeItRainInTheBathroom;

    public GameObject bathroomCutsceneHolder;
    private TriggerGateClose gateCloseScript;

    public GameObject character;
    private Inventory charactersInventoryScript;

    public FMOD.Studio.EventInstance IHaveMilk;
    public FMOD.Studio.EventInstance IHaveCandy;

    public GameObject stormSystem;
    private Animator stormSystemAnimator;

    public GameObject stormSystemSoundHolder;
    private StormSoundControls stormSoundControlsScript;

    public FMOD.Studio.EventInstance generalIncorrectAnswerOrInteractionComment;

    public GameObject generalRainSoundHolder;
    private GeneralRainSoundsScript generalRainSoundScript;

    private void Awake()
    {
        stormSystemAnimator = stormSystem.GetComponent<Animator>();
        stormSoundControlsScript = stormSystemSoundHolder.GetComponent<StormSoundControls>();
        generalIncorrectAnswerOrInteractionComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/ThatDidntWork");
        generalRainSoundScript = generalRainSoundHolder.GetComponent<GeneralRainSoundsScript>();

    }

    // Use this for initialization
    void Start()
    {


        PauseGameScript = GameObject.Find("GameController")?.GetComponent<PauseGame>();

        AudioActiveStatusManager = GameObject.Find("ActiveStatusManager");
        
        RandomWordsScript = gameObject.GetComponent<RandomWords>();

        temporaryPictureName = transform.name;//grabbing the correct answer choice based on the parent object
        
        ImageNameList = new List<string>(RandomWordsScript.MasterListOfChoices);

        Lights = GameObject.Find("Lights");
        LightScript = Lights.GetComponent<ProgressiveLights>();

        GhostSoul = GameObject.Find("BathroomGhostSoul");//grabbing the specific ghost soul

        freedSoulsCounter = GameObject.Find("SoulCounter");
        FreedSoulsScript = freedSoulsCounter.GetComponent<CountFreedSouls>();

        LevelChanger = GameObject.Find("LevelChanger");
        SceneManagementScript = LevelChanger.GetComponent<SceneManagement>();
        TitleScreenMusic = SceneManagement.TitleScreenMusic;
        PostFirstPuzzleMusic = SceneManagement.PostFirstPuzzleMusic;

        

        LockViewScript = PlayerCamera.GetComponent<LockView>();
        GroundskeeperRespondsToIncorrectAnswer = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Groundskeeper/IncorrectBathroomAnswerResponse");
        GroundskeeperRespondsToCorrectAnswer = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Groundskeeper/CorrectBathroomAnswerResponse");
        LightningSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Lightning");

        makeItRainInTheBathroom = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/makeItRainInTheBathroom");

        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        gateCloseScript = bathroomCutsceneHolder.GetComponent<TriggerGateClose>();

        charactersInventoryScript = character.GetComponent<Inventory>();

        IHaveMilk = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/IHaveMilk");
        IHaveCandy = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/IHaveCandy");

    }

    void Update()
    {
        if (!PauseGame.GamePaused)// if the game isn't paused
        {
           
            if (LockViewScript.locked && Input.GetMouseButtonDown(1))    // right mouse button click 
            {
                
                ImageListIndex++;//move through the ImageList and cycle back to 0 at the end
                if (ImageListIndex > ImageNameList.Count - 1)
                {
                    ImageListIndex = 0;
                }

                temporaryPictureName = ImageNameList[ImageListIndex];//string for picture to load

                Sprite SpriteToLoad = Resources.Load<Sprite>("Images/TextSprites/" + temporaryPictureName);//create a space in memory for the sprite to load

                if (SpriteToLoad)//error checking
                {
                    GetComponent<SpriteRenderer>().sprite = SpriteToLoad;//if no error, load the sprite 

                }
                else
                {
                    Debug.LogError("no sprite found ImageName = " + ImageNameList[ImageListIndex]);//if there is an error, notify the developers
                }

                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/RightClickScrollThroughWords");//aural feedback to confirm we're cycling through choices
            }//end of right click

            if (Input.GetMouseButtonDown(0))//left click submits an answer choice
            {
               


                if (temporaryPictureName == gameObject.transform.parent.name)//if the answer is correct
                {
                    if (LockViewScript.LockedWithGroundskeeper) //if provided the correct answer for bathroom while speaking with the groundskeeper
                    {
                        stormSystemAnimator.enabled = true;
                        ProgressiveLights.turnOnFog();
                        stormSoundControlsScript.StormSoundInstance.start();
                        GroundskeeperRespondsToCorrectAnswer.start(); //groundskeeper says 'there' in Hungarian
                        generalRainSoundScript.generalRainSounds.setParameterValue("rainTypes", 1);
                        LockViewScript.randomWord = null;
                        LockViewScript.randomWordBool = false;
                        LockViewScript.LockedWithGroundskeeper = false;
                        LockViewScript.bathroomLightningCutSceneCameraPan = true; //camera pans to the bathroom
                        LockViewScript.checkHit = false;
                        
                        LightningSound.start();
                        StartCoroutine(delayAppearanceOfBathroomStuff());
                        SceneManagementScript.ShouldFadeInPostFirstLevelTrack = true;
                        LightScript.MakeAmbientCreepier();//make the game slightly darker to help add progressive creepy ambience
                        increaseGraininessOfGraphics();
                    }

                    if (LockViewScript.LockedWithForint)
                    {
                        makeItRainInTheBathroom.start();
                        InventoryItemManager.playerHasForint = true;
                        
                        thisTextGraphic.SetActive(false);
                        Forint.SetActive(false);
                     }
                    if (LockView.LockedWithMilk)
                    {
                        InventoryItemManager.playerHasMilk = true;
                       
                        milk.SetActive(false);
                        IHaveMilk.start();
                        //charactersInventoryScript.addObtainedItemPictureToNextAvailableSlot(holdMySpriteScript.myInventorySprite);
                    }
                    if (LockViewScript.LockedWithCharlie)
                    {
                        DialogueWithCharlie.CorrectWordForMedicineResponse.start();
                        thisTextGraphic.SetActive(false);
                        PuzzleManagement.PlayerIsDoingSicknessPuzzle = false;
                        //StartCoroutine(delayTransitionOutOfSicknessPuzzle());
                        PuzzleManagement.PlayerIsDoingCandyPuzzle = true;
                        InventoryItemManager.playerHasMedicine = true;
                        
                        turulSFXScript.playerHasInteractedWithTurulThisPuzzle = false;
                        turulSFXScript.emersionLightningHasStruckThisPuzzle = false;
                        //gateCloseScript.PlayLoopingTurulSquawk();
                        PlayLoopingSquawk.TurulLoopsSquawk.start();
                    }
                    if (LockViewScript.LockedWithCandyBowl)
                    {
                        thisTextGraphic.SetActive(false);
                        InventoryItemManager.playerHasCandy = true;
                        IHaveCandy.start();
                    }

                    

                    FreedSoulsScript.IncreaseNumberOfFreedSouls();//keep track of progress in level

                    
                    
                    
                    
                    

                }
                else //incorrect answer choice
                {
                    if (LockViewScript.LockedWithGroundskeeper)
                    {

                        GroundskeeperRespondsToIncorrectAnswer.start();
                        
                    }
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Words/Incorrect_Answer");//negative aural feedback to player for an incorrect answer    
                    else if (LockViewScript.LockedWithCharlie)
                    {
                        DialogueWithCharlie.IncorrectWordForMedicineResponse.start();
                    }

                    else
                    {
                        generalIncorrectAnswerOrInteractionComment.start();
                    }
                }

            }//end of left click
            if (GhostSoulVisible)
            {
                GhostSoul.transform.position += Vector3.up * 0.1f;//when the soul is freed, make it fly to heaven
                
            }
        }
            }//end of update

        public IEnumerator delayAppearanceOfBathroomStuff()
    {
        yield return new WaitForSeconds(2.0f);
        bathroomStuff.SetActive(true);//bathroom magically appears after lightning
        temporaryCemeteryWall.SetActive(false);
        Groundskeeper.SetActive(false);//groundskeeper disappears
        shovel.SetActive(true);//groundskeeper drops the shovel
        
    }

    public void increaseGraininessOfGraphics()
    {
        PPVScript = PostProccessingValue.GetComponent<PostProcessVolume>();
        PPVScript.profile.TryGetSettings<Grain>(out GrainLayer);
        PPVScript.profile.TryGetSettings<Vignette>(out VignetteLayer);
        
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

    
}//end of right click class
