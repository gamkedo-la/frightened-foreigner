using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmersionLightningEmmissionToggle : MonoBehaviour
{

    private ParticleSystem EmersionLightning;
    public ParticleSystem Sparks;
    

    // Start is called before the first frame update
    void Start()
    {
        EmersionLightning = gameObject.GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var EmersionLightningEmitter = EmersionLightning.emission;
        if (Input.GetKeyDown(KeyCode.L))
        {
            EmersionLightningEmitter.enabled = true;
            EmersionLightning.Emit(1);
            Sparks.Emit(1);
        }
    }
}
