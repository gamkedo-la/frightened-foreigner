using UnityEngine;

public class ItemInteractionTester : MonoBehaviour
{
	[SerializeField] private string correctItemResponse = "Thanks!";
	[SerializeField] private string wrongItemResponse = "I don't want that.";

	public void ShowHint()
	{
		Debug.Log( wrongItemResponse );
	}

	public void ShowSuccess( )
	{
		Debug.Log(correctItemResponse);
	}
}
