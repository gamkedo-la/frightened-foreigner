using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance IHaveWater;
    public FMOD.Studio.EventInstance TheSinkDoesntCare;


    // Start is called before the first frame update
    void Start()
    {
        IHaveWater = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/IHaveWater");
        TheSinkDoesntCare = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/SinkDoesntCare");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        IHaveWater.start();
    }

    public void IncorrectResponse()
    {
        TheSinkDoesntCare.start();
    }
}
