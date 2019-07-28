using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabSceneManager : MonoBehaviour
{

    private GameObject preservedLevelChanger;

    private Button SkipIntroButton;

    private SceneManagement SceneManagementScript;

    private void Awake()
    {
        SkipIntroButton = GetComponent<Button>();
        preservedLevelChanger = GameObject.Find("LevelChanger");
        SceneManagementScript = preservedLevelChanger.GetComponent<SceneManagement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SkipIntroButton.onClick.AddListener(SceneManagementScript.LoadCemeteryLevelFromIntroCutscene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
