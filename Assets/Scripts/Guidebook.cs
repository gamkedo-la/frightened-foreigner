using UnityEngine;

public class Guidebook : MonoBehaviour
{
	[SerializeField] private GameObject[] pages = null;

	private int currentPage = 0;

    void Start()
    {

    }

    void Update()
    {

    }

	public void PlaySoundCue( int pageNumber )
	{
		// TODO: Stebs, here you can add all the FMOD sounds
		switch ( pageNumber )
		{
			case 1:
			break;

			case 2:
			break;

			case 3:
			break;

			case 4:
			break;

			default:
			break;
		}
	}

	public void NextPage( )
	{
		if (currentPage < pages.Length)
		{
			foreach ( var page in pages )
				page.SetActive( false );

			currentPage++;
			pages[currentPage].SetActive( true );
		}
	}

	public void PreviousPage( )
	{
		if ( currentPage > 0 )
		{
			foreach ( var page in pages )
				page.SetActive( false );

			currentPage--;
			pages[currentPage].SetActive( true );
		}
	}
}
