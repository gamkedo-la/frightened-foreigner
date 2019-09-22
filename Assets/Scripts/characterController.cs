using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    public float speed = 10.0f;
   


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//keeps mouse locked into the game space
       
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxisRaw("Vertical") * speed;
        float strafe = Input.GetAxisRaw("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))//allows player to navigate out of the game space
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown("r"))//repeats target audio
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Words/Bathroom_Furdoszoba");
        }

        
            
    }
}
