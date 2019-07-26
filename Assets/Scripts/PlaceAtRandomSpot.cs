using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAtRandomSpot : MonoBehaviour
{
	[SerializeField] private Vector3 originPoint = Vector3.zero;
	[SerializeField] private Vector3 placeBoxSize = Vector3.one;

	void Start( )
	{
		Place( );
	}

	void OnDrawGizmosSelected( )
	{
		Gizmos.DrawWireCube( originPoint, placeBoxSize );
	}

	[ContextMenu("Place at random position")]
	private void Place( )
	{
		transform.position = GetRandomPosition( );
	}

	private Vector3 GetRandomPosition( )
	{
		Vector3 randomPos = new Vector3
		(
			Random.Range( -placeBoxSize.x / 2, placeBoxSize.x / 2 ),
			Random.Range( -placeBoxSize.y / 2, placeBoxSize.y / 2 ),
			Random.Range( -placeBoxSize.z / 2, placeBoxSize.z / 2 )
		);

		return randomPos + originPoint;
	}
}
