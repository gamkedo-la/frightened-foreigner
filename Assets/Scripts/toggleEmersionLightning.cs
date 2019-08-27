using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleEmersionLightning : MonoBehaviour
{
    public GameObject emersionLightningParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.L) && !emersionLightningParticleSystem.activeInHierarchy)
        {
            emersionLightningParticleSystem.SetActive(true);
            Debug.Log("Hello L Key");
        }
        else if (Input.GetKeyDown(KeyCode.L) && emersionLightningParticleSystem.activeInHierarchy)
        {
            emersionLightningParticleSystem.SetActive(false);
        }
    }
}
