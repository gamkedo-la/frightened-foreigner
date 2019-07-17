using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudioSettingsMenu : MonoBehaviour
{
    public Transform audioSettingsCanvas;
    public Transform pauseMenuCanvas;

    private void Awake()
    {
        //audioSettingsCanvas = GameObject.Find("AudioSettingsCanvas");
        //pauseMenuCanvas = GameObject.Find("PauseCanvas");
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
        if (audioSettingsCanvas.gameObject.activeInHierarchy == false)
        {
            audioSettingsCanvas.gameObject.SetActive(true);
            pauseMenuCanvas.gameObject.SetActive(false);

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            audioSettingsCanvas.gameObject.SetActive(false);
            pauseMenuCanvas.gameObject.SetActive(true);
            //Time.timeScale = 0;
            //Cursor.lockState = CursorLockMode.Confined;
            //Cursor.lockState = CursorLockMode.Locked;
            //Time.timeScale = 1;
        }
    }
}
