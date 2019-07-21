using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClicks: MonoBehaviour
{

    public Transform PauseCanvas;
    public Transform AudioSettingsCanvas;

    public List<string> ImageNameList;//to keep a list of possible word choices
    private int ImageListIndex = 0;

    private RandomWords RandomWordsScript;//where to grab list of possible words

    public string temporaryPictureName;//helps cycle through word choices

    private GameObject GhostSoul;//hidden soul to be set free when correct choice is chosen
    private bool GhostSoulVisible = false;

    private GameObject freedSoulsCounter;//keeping track of level progress
    private CountFreedSouls FreedSoulsScript;

    private FMOD.Studio.EventInstance BasicBackGroundMusic;//to set the parameter value of freed souls in FMOD, which should transition to creepier tracks as the level progresses
    private FMOD.Studio.EventInstance HarmonicMinoredBackgroundMusic;

    // Use this for initialization
    void Start()
    {  
        RandomWordsScript = transform.parent.GetComponent<RandomWords>();
        //Debug.Log(RandomWordsScript);
        temporaryPictureName = transform.parent.name;//grabbing the correct answer choice based on the parent object
        
        ImageNameList = new List<string>(RandomWordsScript.AnswerChoices);
        
        GhostSoul = GameObject.Find("BathroomGhostSoul");//grabbing the specific ghost soul

        freedSoulsCounter = GameObject.Find("SoulCounter");
        FreedSoulsScript = freedSoulsCounter.GetComponent<CountFreedSouls>();

        BasicBackGroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/BasicBackgroundMusic");
        BasicBackGroundMusic.start();

        //HarmonicMinoredBackgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/HarmonicMinoredBackgroundMusic");
       
    }

    void Update()
    {
        if (PauseCanvas.gameObject.activeInHierarchy == false && AudioSettingsCanvas.gameObject.activeInHierarchy == false) //if the game isn't paused
        {
            if (Input.GetMouseButtonDown(1))    // right mouse button click
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
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Words/Correct_Answer");//positive aural feedback for player

                    Sprite GhostSoulSprite = Resources.Load<Sprite>("Images/ghost_soul");//load soul sprite
                    Debug.Log(GhostSoulSprite);
                    GhostSoul.GetComponent<SpriteRenderer>().sprite = GhostSoulSprite;//soul sprite becomes visible
                    Debug.Log(GhostSoul.GetComponent<SpriteRenderer>().sprite);
                    GhostSoulVisible = true;//set bool to trigger float to heaven movement in the function/conditional at the bottom of this script

                    RenderSettings.ambientIntensity = 0.1f;//make the game slightly darker to help add progressive creepy ambience

                    FreedSoulsScript.IncreaseNumberOfFreedSouls();//keep track of progress in level

                    BasicBackGroundMusic.setParameterValue("FreedSouls", FreedSoulsScript.NumberOfFreedSouls);//changes the parameter value in FMOD, used to trigger progressively creepier music

                    Debug.Log(BasicBackGroundMusic.getParameterValue("FreedSouls", out float NewFreedSoulsValue, out float NewFreedSoulsFinalValue));//ONLY grabs the value from FMOD and outputs OK to Debug Log, see following function
                    Debug.Log(NewFreedSoulsValue);//displays actual value after being grabbed in the previous function

                    //BasicBackGroundMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    //HarmonicMinoredBackgroundMusic.start();
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Words/Incorrect_Answer");//negative aural feedback to player for an incorrect answer              
                }

            }//end of left click
            if (GhostSoulVisible)
            {
                GhostSoul.transform.position += Vector3.up * 0.1f;//when the soul is freed, make it fly to heaven
                Debug.Log("inside soul floating up");
            }
        }
            }//end of update
}//end of right click class
