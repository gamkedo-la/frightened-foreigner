using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementFootstepsTrigger : MonoBehaviour
{

    public GameObject generalRainSoundHolder;
    public GameObject thunderSoundHolder;

    void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayFootsteps.Footsteps.setParameterValue("SwitchSteps", 2.0f);
        if (!PuzzleManagement.PlayerIsDoingBathroomPuzzle && !PuzzleManagement.PlayerIsDoingMoneyPuzzle)
        {
            generalRainSoundHolder.GetComponent<GeneralRainSoundsScript>().generalRainSounds.setParameterValue("rainTypes", 1.0f);
            generalRainSoundHolder.GetComponent<GeneralRainSoundsScript>().generalRainSounds.setParameterValue("bathroomRainFilter", 1.0f);
            thunderSoundHolder.GetComponent<StormSoundControls>().StormSoundInstance.setParameterValue("StormIntensity", 2.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayFootsteps.Footsteps.setParameterValue("SwitchSteps", 0f);
        if (!PuzzleManagement.PlayerIsDoingBathroomPuzzle && !PuzzleManagement.PlayerIsDoingMoneyPuzzle)
        {
            generalRainSoundHolder.GetComponent<GeneralRainSoundsScript>().generalRainSounds.setParameterValue("rainTypes", 2.0f);
            generalRainSoundHolder.GetComponent<GeneralRainSoundsScript>().generalRainSounds.setParameterValue("bathroomRainFilter", 0.0f);
            thunderSoundHolder.GetComponent<StormSoundControls>().StormSoundInstance.setParameterValue("StormIntensity", 1.0f);


        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
