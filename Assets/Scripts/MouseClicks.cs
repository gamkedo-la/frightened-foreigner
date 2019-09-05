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

    public FMOD.Studio.EventInstance makeItRainInTheBathroom;

    private void Awake()
    {
        
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

        //PostProccessingValue = GameObject.Find("PostProccessingVolume");
        //PPVScript = PostProccessingValue.GetComponent<PostProcessVolume>();
        //Debug.Log(PostProccessingValue);

        LockViewScript = PlayerCamera.GetComponent<LockView>();
        GroundskeeperRespondsToIncorrectAnswer = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Groundskeeper/IncorrectBathroomAnswerResponse");
        GroundskeeperRespondsToCorrectAnswer = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Groundskeeper/CorrectBathroomAnswerResponse");
        LightningSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Lightning");

        makeItRainInTheBathroom = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/makeItRainInTheBathroom");
    }

    void Update()
    {
        if (!PauseGameScript.GamePaused)// if the game isn't paused
        {
           // Debug.Log(PostProccessingValue.GetComponent<PostProcessVolume>());
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
                        GroundskeeperRespondsToCorrectAnswer.start(); //groundskeeper says 'there' in Hungarian
                        LockViewScript.randomWord = null;
                        LockViewScript.bathroomCutSceneCameraPan = true; //camera pans to the bathroom
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
                        LockViewScript.UnLockView();
                        thisTextGraphic.SetActive(false);
                        Forint.SetActive(false);
                     }
                    if (LockView.LockedWithMilk)
                    {
                        InventoryItemManager.playerHasMilk = true;
                        Debug.Log(InventoryItemManager.playerHasMilk);
                        milk.SetActive(false);
                    }

                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Words/Correct_Answer");//positive aural feedback for player

                    //Sprite GhostSoulSprite = Resources.Load<Sprite>("Images/ghost_soul");//load soul sprite
                    
                    //GhostSoul.GetComponent<SpriteRenderer>().sprite = GhostSoulSprite;//soul sprite becomes visible
                    
                    //GhostSoulVisible = true;//set bool to trigger float to heaven movement in the function/conditional at the bottom of this script

                    //RenderSettings.ambientIntensity = 0.1f;//make the game slightly darker to help add progressive creepy ambience

                    

                    // postprocessing effect to make ambient creepier
                    //PPVScript.profile.TryGetSettings(out grainLayer);
                    //PPVScript.profile.TryGetSettings(out ambientOcclusionLayer);
                    //grainLayer = FindObjectOfType<Grain>();
                    //grainLayer = PPVScript.GetComponent<Grain>();
                    //ambientOcclusionLayer = FindObjectOfType<AmbientOcclusion>();
                    

                    FreedSoulsScript.IncreaseNumberOfFreedSouls();//keep track of progress in level

                    
                    
                    //PostFirstPuzzleMusic.start();
                    
                    //Debug.Log(BasicBackGroundMusic.getParameterValue("FreedSouls", out float NewFreedSoulsValue, out float NewFreedSoulsFinalValue));//ONLY grabs the value from FMOD and outputs OK to Debug Log, see following function
                    //Debug.Log(NewFreedSoulsValue);//displays actual value after being grabbed in the previous function

                }
                else //incorrect answer choice
                {
                    if (LockViewScript.LockedWithGroundskeeper)
                    {

                        GroundskeeperRespondsToIncorrectAnswer.start();
                        
                    }
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Words/Incorrect_Answer");//negative aural feedback to player for an incorrect answer              
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
}//end of right click class
