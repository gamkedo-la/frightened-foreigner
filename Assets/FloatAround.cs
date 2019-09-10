using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAround : MonoBehaviour
{

    

    // change this to light emissions
    public float flickersPerSecond = 4f;
    private int flickerCounter = 0;

    // float around
    private float originalX;
    private float originalY;
    private float originalZ;

    public float xStrength = 0.05f;
    public float yStrength = 0.05f;
    public float zStrength = 0.0125f;

    public float xSpeed = 0.001f;
    public float ySpeed = 0.001f;
    public float zSpeed = 0.001f;

    private GameObject tempGO;

    // Use this for initialization
    void Start()
    {
        // do nothing if we don't have meshes to use
        

        

        originalX = transform.position.x;
        originalY = transform.position.y;
        originalZ = transform.position.z;

        //change to light emissions
        //StartCoroutine("Flappy");
    }

    // change to light emissions
    IEnumerator LightFlickers()
    {
        for (; ; )
        {
            flickerCounter++;
            if (flickerCounter % 2 == 0)
            {
                
            }
            else
            {
                
            }
            yield return new WaitForSeconds(1f / flickersPerSecond);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            originalX + (Mathf.Sin(Time.time * xSpeed) * xStrength),
            originalY + (Mathf.Sin(Time.time * ySpeed) * yStrength),
            originalZ + (Mathf.Sin(Time.time * zSpeed) * zStrength)
        );        
    }

}

