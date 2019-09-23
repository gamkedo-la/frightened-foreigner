
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flicker : MonoBehaviour
{

    public GameObject thisLight;
    private Light lightComponent;
    private bool lightIsOn = false;
    

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = thisLight.GetComponent<Light>();
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


        Debug.Log(lightComponent.intensity);

        //testing light functionality
        if (Input.GetKeyDown("l"))//allows player to navigate out of the game space
        {
            if (!lightIsOn)
            {
                //flickerOn();
                //Debug.Log(lightComponent.intensity);
                lightIsOn = true;
            }
            else if (lightIsOn)
            {
                //flickerOff();
                //Debug.Log(lightComponent.intensity);

                lightIsOn = false;
            }
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
