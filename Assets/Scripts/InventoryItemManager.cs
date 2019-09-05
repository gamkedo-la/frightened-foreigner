using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerItem
{
	None,
	Phone,
	Water,
}

public class InventoryItemManager : MonoBehaviour
{

    //the player has this stuff at the beginning of the game
    public static bool playerHasWaterBottle = true;
    public static bool playerHasGuidebook = true;


    //the player does not have this stuff at the beginning of the game
    public static bool playerHasForint = false;
    public static bool playerHasMilk = false;

    public GameObject ForintInLevel;
    public GameObject ForintTextGraphic;

    public GameObject MilkInLevel;
    public GameObject MilkTextGraphic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHasForint)
        {
            ForintInLevel.SetActive(false);//make the forint no longer visible in the cemetery when obtained
            ForintTextGraphic.SetActive(false); // text graphic no longer visible when the forint is obtained
        }
        if (playerHasMilk)
        {
            MilkInLevel.SetActive(false);
            MilkTextGraphic.SetActive(false);
        }
    }
}
