using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudioCanvas : MonoBehaviour
{
    public GameObject AudioSettingsCanvasManagerFromHierarchy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DontDestroyMe()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
