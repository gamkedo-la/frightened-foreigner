using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLayerChange : MonoBehaviour
{

    private SceneManagement SceneManagementScript;
    private FMOD.Studio.EventInstance TitleScreenMusic;
    //public float BackgroundMusicLayersFadeValue;
    public bool ShouldTransitionToBassoonPart = false;

    private void Awake()
    {
        SceneManagementScript = GameObject.Find("LevelChanger").GetComponent<SceneManagement>();
        TitleScreenMusic = SceneManagementScript.TitleScreenMusic;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ShouldTransitionToBassoonPart && SceneManagementScript.BackgroundMusicLayersFadeValue < 4)
        {
            SceneManagementScript.BackgroundMusicLayersFadeValue += 0.005f;
            
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", SceneManagementScript.BackgroundMusicLayersFadeValue);
        }
    }

    public void ChangeLayersSecondPartIntroText()
    {
        if (!ShouldTransitionToBassoonPart)
        {
            ShouldTransitionToBassoonPart = true;
        }
        
    }
}
