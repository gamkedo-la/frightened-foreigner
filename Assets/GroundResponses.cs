using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance groundDoesntCare;
    public FMOD.Studio.EventInstance IHaveDirt;

    // Start is called before the first frame update
    void Start()
    {
        groundDoesntCare = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/GroundDoesntCare");
        IHaveDirt = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/IHaveDirt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
        IHaveDirt.start();
    }

    public void IncorrectResponse()
    {
        groundDoesntCare.start();
    }
}
