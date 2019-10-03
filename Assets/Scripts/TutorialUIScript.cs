using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIScript : MonoBehaviour
{
    public bool isDialogActive = false;
    public GameObject dialogCanvas;
    public GameObject dialogTextObject;
    private Text dialogText;

    

    // Update is called once per frame
    private void Start()
    {
        dialogText = dialogTextObject.GetComponent<Text>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeDialog();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isDialogActive)
            {
                closeDialog();
            }
            if (!isDialogActive)
            {
                openDialog(dialogText.text);
            }
        }
    }
    public void openDialog(string text)
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        dialogText.text = text;
        isDialogActive = true;
        dialogCanvas.SetActive(true);
        PauseGame.GamePaused = true;
    }
    public void closeDialog()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isDialogActive = false;
        dialogCanvas.SetActive(false);
        PauseGame.GamePaused = false;
    }
}
