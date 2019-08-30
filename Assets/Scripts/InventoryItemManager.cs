using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemManager : MonoBehaviour
{

    //the player has this stuff at the beginning of the game
    public static bool playerHasWaterBottle = true;
    public static bool playerHasGuidebook = true;
    

    //the player does not have this stuff at the beginning of the game
    public static bool playerHasForint = false;

    public GameObject Forint;
    public GameObject ForintTextGraphic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHasForint)
        {
            Forint.SetActive(false);//make the forint no longer visible in the cemetery when obtained
            ForintTextGraphic.SetActive(false); // text graphic no longer visible when the forint is obtained
        }
    }
}
