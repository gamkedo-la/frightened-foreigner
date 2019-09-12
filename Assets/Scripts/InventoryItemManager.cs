using UnityEngine;

public enum PlayerItem
{
	None,
	Phone,
	WaterBottleEmpty,
	WaterBottleFull,
	Forint,
	Milk,
	Candy,
	Shovel,
    Dirt,
    Lighter,
    Fan,
}

public class InventoryItemManager : MonoBehaviour
{
	public GameObject[] buttons;

   //the player has this stuff at the beginning of the game
    public static bool playerHasWaterBottle = true;
    public static bool playerHasFullWaterBottle = false;
    public static bool playerHasGuidebook = true;

    //the player does not have this stuff at the beginning of the game
    public static bool playerHasForint = false;
    public static bool playerHasMilk = false;
    public static bool playerHasCandy = false;
    public static bool playerHasShovel = false;
    public static bool playerHasMedicine = false;
    public static bool playerHasDirt = false;
    public static bool playerHasFan = false;
    public static bool playerHasLighter = false;

    public GameObject ForintInLevel;
    public GameObject ForintTextGraphic;

    public GameObject MilkInLevel;
    public GameObject MilkTextGraphic;

    void FixedUpdate()
    {
		buttons[0].SetActive( playerHasGuidebook );
		buttons[1].SetActive( playerHasWaterBottle );
		buttons[2].SetActive( playerHasFullWaterBottle );
		buttons[3].SetActive( playerHasForint );
		buttons[4].SetActive( playerHasMilk );
		buttons[5].SetActive( playerHasCandy );
		buttons[6].SetActive( playerHasShovel );
		
        buttons[7].SetActive(playerHasDirt);
        buttons[8].SetActive(playerHasFan);
        buttons[9].SetActive(playerHasLighter);
		buttons[10].SetActive( true );
}

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

	public void GiveItem( PlayerItem recivedItem )
	{
		SetItem( recivedItem, true );
	}

	public void RemoveItem( PlayerItem itemToRemove )
	{
		SetItem( itemToRemove, false );
	}

	private void SetItem( PlayerItem item, bool hasItem )
	{
		switch ( item )
		{
			case PlayerItem.None:
			break;

			case PlayerItem.Phone:
			playerHasGuidebook = hasItem;
			break;

			case PlayerItem.WaterBottleEmpty:
			playerHasWaterBottle = hasItem;
			break;

			case PlayerItem.WaterBottleFull:
			playerHasFullWaterBottle = hasItem;
			break;

			case PlayerItem.Forint:
			playerHasForint = hasItem;
			break;

			case PlayerItem.Milk:
			playerHasMilk = hasItem;
			break;

			case PlayerItem.Candy:
			playerHasCandy = hasItem;
			break;

			case PlayerItem.Shovel:
			playerHasShovel = hasItem;
			break;

            case PlayerItem.Dirt:
                playerHasDirt = hasItem;
                break;

            case PlayerItem.Fan:
                playerHasFan = hasItem;
                break;

            case PlayerItem.Lighter:
                playerHasLighter = hasItem;
                break;

            default:
			Debug.LogError( "I don't know this item!" );
			break;
		}
	}
}
