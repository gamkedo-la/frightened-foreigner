using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveIfWebBuild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("Non-native build, removing exit button");
            Destroy(gameObject);
        }
    }
}
