using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TriggerGateClose : MonoBehaviour
{

    public GameObject door;
    private DoorScript doorScript;

    private SceneManagement sceneManagementScript;
    private GameObject levelChanger;

    private GameObject Lights;
    private ProgressiveLights LightsScript;

    public GameObject PostProccessingValue;
    private PostProcessVolume PPVScript;
    private Grain GrainLayer;
    private Vignette VignetteLayer;
    private float PPVMultiplier = 1.5f;
    private float maxGrainIntensity = 0.5f;
    private float maxVignetteIntensity = 0.45f;

    // Start is called before the first frame update
    void Start()
    {
        doorScript = door.GetComponent<DoorScript>();
        levelChanger = GameObject.Find("LevelChanger");
        sceneManagementScript = levelChanger.GetComponent<SceneManagement>();

        Lights = GameObject.Find("Lights");
        LightsScript = Lights.GetComponent<ProgressiveLights>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerBathroomCutsceneBool()
    {
        doorScript.BathroomCutsceneHasPlayed = true;
        sceneManagementScript.ShouldFadeInPostBathroomMusic = true;
        LightsScript.MakeAmbientCreepier();//make the game slightly darker to help add progressive creepy ambience

        PPVScript = PostProccessingValue.GetComponent<PostProcessVolume>();
        PPVScript.profile.TryGetSettings<Grain>(out GrainLayer);
        PPVScript.profile.TryGetSettings<Vignette>(out VignetteLayer);
        //Debug.Log(ambientOcclusionLayer);
        GrainLayer.intensity.Override(GrainLayer.intensity * PPVMultiplier);
        VignetteLayer.intensity.Override(VignetteLayer.intensity * PPVMultiplier);
        if (GrainLayer.intensity > maxGrainIntensity)
        {
            GrainLayer.intensity.Override(maxGrainIntensity);
        }
        if (VignetteLayer.intensity > maxVignetteIntensity)
        {
            VignetteLayer.intensity.Override(maxVignetteIntensity);
        }
    }
}
