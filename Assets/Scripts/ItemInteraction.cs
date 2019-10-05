using UnityEngine;
using UnityEngine.Events;

public class ItemInteraction : MonoBehaviour
{
	[SerializeField] private PlayerItem requiredItem = PlayerItem.None;
	[SerializeField] private bool returnDifferentItem = false;
	[SerializeField] private PlayerItem returnItem = PlayerItem.None;
	[SerializeField] private UnityEvent doIntereaction = null;
	[SerializeField] private UnityEvent giveHint = null;
    

    public PlayerItem TryInteracting( PlayerItem itemInHand )
    {
      
		if ( requiredItem == itemInHand )
		{
			doIntereaction.Invoke( );

			if ( returnDifferentItem )
				return returnItem;
			else
				return itemInHand;
		}
		else
		{
			giveHint.Invoke( );
			return itemInHand;
		}
	}
}
