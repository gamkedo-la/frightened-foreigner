using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnGameWonText : MonoBehaviour
{

    public GameObject YouWinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOnYouWinText()
    {
        YouWinText.SetActive(true);
    }
}
