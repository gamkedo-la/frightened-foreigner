using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGateClose : MonoBehaviour
{

    public GameObject door;
    private DoorScript doorScript;
    private SceneManagement sceneManagementScript;
    private GameObject levelChanger;

    // Start is called before the first frame update
    void Start()
    {
        doorScript = door.GetComponent<DoorScript>();
        levelChanger = GameObject.Find("LevelChanger");
        sceneManagementScript = levelChanger.GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerBathroomCutsceneBool()
    {
        doorScript.BathroomCutsceneHasPlayed = true;
        sceneManagementScript.ShouldFadeInPostBathroomMusic = true;
    }
}
