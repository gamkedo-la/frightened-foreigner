using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFMODThenChangeScene : MonoBehaviour
{
    private void Start()
    {
        FMODUnity.RuntimeManager.LoadBank("Master Bank");
    }
    void Update()
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master Bank"))
        {
            SceneManager.LoadScene("Main Menu",LoadSceneMode.Single);
        }
    }
}
