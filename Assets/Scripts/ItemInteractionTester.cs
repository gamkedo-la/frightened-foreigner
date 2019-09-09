using UnityEngine;

public class ItemInteractionTester : MonoBehaviour
{
	[SerializeField] private string success = "Thanks!";
	[SerializeField] private string hint = "I'm thirsty...";

	public void ShowHint()
	{
		Debug.Log( hint );
	}

	public void ShowSuccess( )
	{
		Debug.Log( success );
	}
}
