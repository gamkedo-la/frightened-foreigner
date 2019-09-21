using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseGame : MonoBehaviour
{
    public Transform PauseMenuCanvas;
    public Transform AudioSettingsCanvas;
    public GameObject TutorialUIHolder;
    public bool GamePaused = false;


    private Scene CurrentScene;

    private void Awake()
    {
        //PauseMenuCanvas = GameObject.Find("PauseCanvas");
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))// && !TutorialUIHolder.GetComponent<TutorialUIScript>().isDialogActive
        {
            CurrentScene = SceneManager.GetActiveScene();
            if (CurrentScene.name == "Cemetery Level")
            {
                TogglePauseMenu();
            }
            
        }
    }

    public void TogglePauseMenu()
    {
        PlayUISounds.UISelectionGhostVoiceSound.start();
        Debug.Log("Should be toggling");
        if (PauseMenuCanvas.gameObject.activeInHierarchy == false)
        {
            GamePaused = true;
            PauseMenuCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            GamePaused = false;
            PauseMenuCanvas.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
