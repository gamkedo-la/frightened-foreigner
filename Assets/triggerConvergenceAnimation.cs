using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerConvergenceAnimation : MonoBehaviour
{
    Animator cloudConvergenceAnimator;
    int convergence2Hash = Animator.StringToHash("cloudConvergence2");

    // Start is called before the first frame update
    void Start()
    {
        cloudConvergenceAnimator = GetComponent<Animator>();
        //cloudConvergenceAnimator.SetBool("convergence2Bool", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerSecondCloudConvergence()
    {
        cloudConvergenceAnimator.Play("Storm Convergence 2");
    }
}
