
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flicker : MonoBehaviour
{

    
    private Light lightComponent;
    private bool lightIsOn = false;
    

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = gameObject.GetComponent<Light>();
        lightComponent.intensity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (lightIsOn && lightComponent.intensity < 2.0f)
        {
            lightComponent.intensity += 0.05f;
        }
        if (!lightIsOn && lightComponent.intensity > 0.0f)
        {
            lightComponent.intensity -= 0.05f;
        }




        
    }

    public void flickerOn()
    {
        lightIsOn = true;
        
    }

    public void flickerOff()
    {
        lightIsOn = false;
        
    }
}
