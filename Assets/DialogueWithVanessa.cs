using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithVanessa : MonoBehaviour
{

    private LockView LockViewScript;
    public GameObject PlayerCamera;

    public GameObject groundskeeper;
    public bool groundskeeperInvisible = true;

    public GameObject door;
    private DoorScript doorScript;

    // Start is called before the first frame update
    void Start()
    {
        
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        doorScript = door.GetComponent<DoorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(LockViewScript);
        
        if (LockViewScript.LockedWithVanessa && groundskeeperInvisible && doorScript.playerHasExploredTheCemetery)
        {
            groundskeeper.SetActive(true);
            Debug.Log(groundskeeperInvisible);
        }

        
    }
}
