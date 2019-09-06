using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disactivateMe : MonoBehaviour
{

    public GameObject playerCamera;
    private LockView lockViewScript;

    // Start is called before the first frame update
    void Start()
    {
        lockViewScript = playerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisactivateMe()
    {
        gameObject.SetActive(false);
        lockViewScript.sicknessPuzzleCutsceneWithFene = false;
    }
}
