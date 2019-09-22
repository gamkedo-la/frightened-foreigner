using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomCutsceneFootstepsControl : MonoBehaviour
{

    public static FMOD.Studio.EventInstance bathroomCutsceneBathroomFootsteps;
    public static FMOD.Studio.EventInstance bathroomCutsceneGroundFootsteps;

    // Start is called before the first frame update
    void Start()
    {
        bathroomCutsceneBathroomFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Bathroom Cutscene/BathroomFootsteps");
        var feet3DPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        bathroomCutsceneBathroomFootsteps.set3DAttributes(feet3DPosition);
        bathroomCutsceneGroundFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Bathroom Cutscene/GroundFootsteps");
        bathroomCutsceneGroundFootsteps.set3DAttributes(feet3DPosition);

    }

    // Update is called once per frame
    void Update()
    {
        var feet3DPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        bathroomCutsceneBathroomFootsteps.set3DAttributes(feet3DPosition);
        bathroomCutsceneGroundFootsteps.set3DAttributes(feet3DPosition);
    }

    public void turnOffBathroomFootsteps()
    {
        bathroomCutsceneBathroomFootsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void turnOnBathroomFootsteps()
    {
        bathroomCutsceneBathroomFootsteps.start();
    }

    public void turnOffGroundFootsteps()
    {
        bathroomCutsceneGroundFootsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void turnOnGroundFootsteps()
    {
        bathroomCutsceneGroundFootsteps.start();
    }

    
}
