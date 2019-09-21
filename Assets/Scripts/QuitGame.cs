using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        // This won't work in WebGL
        // Debug.Log("Attempting to quit");
        PlayUISounds.UISelectionGhostVoiceSound.start();
        Application.Quit();
    }
}
