using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFeneSFX : MonoBehaviour
{

    public FMOD.Studio.EventInstance FeneLaughs;
    public FMOD.Studio.EventInstance FeneScurries;
    public FMOD.Studio.EventInstance FenePukes;
    public FMOD.Studio.EventInstance FeneFliesAway;

    // Start is called before the first frame update
    void Start()
    {
        FeneLaughs = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FeneLaughs");
        FeneScurries = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FeneScurries");
        FenePukes = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FenePukes");
        //FeneFliesAway = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Lightning");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFeneLaughs()
    {
        FeneLaughs.start();
    }

    public void PlayFeneScurries()
    {
        FeneScurries.start();
    }

    public void PlayFenePukes()
    {
        FenePukes.start();
    }
}
