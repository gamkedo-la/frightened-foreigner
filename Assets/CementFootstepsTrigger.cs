using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementFootstepsTrigger : MonoBehaviour
{

    void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayFootsteps.Footsteps.setParameterValue("SwitchSteps", 2.0f);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayFootsteps.Footsteps.setParameterValue("SwitchSteps", 0f);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
