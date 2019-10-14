using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance TurulSaysWaterSound;
    public FMOD.Studio.EventInstance TurulSaysIgenSound;
    public FMOD.Studio.EventInstance FillingBasinSound;

    public GameObject FilledBasin;
    

    public GameObject playerCamera;
    private LockView lockViewScript;

    public GameObject textGraphic;

    // Start is called before the first frame update
    void Start()
    {
        TurulSaysWaterSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/water");
        TurulSaysIgenSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/igen");
        FillingBasinSound = FMODUnity.RuntimeManager.CreateInstance("event:/ElementsPuzzle/fillingWaterBasin");
        lockViewScript = playerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        PuzzleManagement.WaterBasinSolved = true;
        Debug.Log("Water Basin Solved");
        TurulSaysIgenSound.start();
        FillingBasinSound.start();
        
        
        lockViewScript.LockOnToTargetObject(gameObject.transform.position);
        FilledBasin.SetActive(true);
        gameObject.SetActive(false);

        MouseClicks.currentCorrectAnswer = "víz";
        textGraphic.transform.position = gameObject.transform.position;
        Vector3 correction = new Vector3(0, 0.65f, 0);
        textGraphic.transform.position += correction;
        textGraphic.SetActive(true);
    }

    public void IncorrectResponse()
    {
        TurulSaysWaterSound.start();
    }
}
