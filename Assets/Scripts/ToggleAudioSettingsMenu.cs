using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleAudioSettingsMenu : MonoBehaviour
{
    public Transform audioSettingsCanvas;
    public Transform pauseMenuCanvas;
    public Transform TitleScreenCanvas;

    

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleAudioSettingsMenu()
    {
        
        if (audioSettingsCanvas.gameObject.activeInHierarchy == false)//if the audio settings menu is inactive
        {
            audioSettingsCanvas.gameObject.SetActive(true);//make the audio settings menu active, then check.....

            if ("Main Menu" == SceneManager.GetActiveScene().name)//if the current scene is the main menu
            {
                TitleScreenCanvas.gameObject.SetActive(false);//hide the Title screen
            }
            else if ("Intro CutScene" == SceneManager.GetActiveScene().name)//if the current scene is the intro cutscene
            {
                //we don't need to do anything, just making it clear in comprehensive logic
            }
            else if ("Cemetery Level" == SceneManager.GetActiveScene().name) //if the current scene is the cemetery
            {
                if (pauseMenuCanvas.gameObject.activeInHierarchy == true)//and we're accessing the audio settings from the pause menu
                {
                    pauseMenuCanvas.gameObject.SetActive(false);//hide the pause menu
                }
            }
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        else if ((audioSettingsCanvas.gameObject.activeInHierarchy == true))//when leaving the audio settings menu
        {
            audioSettingsCanvas.gameObject.SetActive(false);//hide the audio settings menu

            if ("Main Menu" == SceneManager.GetActiveScene().name)//if the current scene is the main menu
            {
                TitleScreenCanvas.gameObject.SetActive(true);//show the title screen
            }
            else if ("Intro CutScene" == SceneManager.GetActiveScene().name)//if the current scene is the intro cutscene
            {
                //we don't need to do anything, just making it clear in comprehensive logic
            }
            else if ("Cemetery Level" == SceneManager.GetActiveScene().name) //if the current scene is the cemetery
            {
                if (pauseMenuCanvas.gameObject.activeInHierarchy == false)//and we're leaving the audio settings, which navigates back to the pause menu
                {
                    pauseMenuCanvas.gameObject.SetActive(false);//show the pause menu
                }
            }
        }
    }
}
