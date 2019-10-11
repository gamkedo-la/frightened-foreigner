using UnityEngine;

public class Guidebook : MonoBehaviour
{
	[SerializeField] private GameObject[] pages = null;

	private int currentPage = 0;
    private FMOD.Studio.EventInstance candyPuzzleGuidebookSound;
    private FMOD.Studio.EventInstance catPuzzleGuidebookSound;
    private FMOD.Studio.EventInstance elementsPuzzleGuidebookSound;
    private FMOD.Studio.EventInstance sicknessPuzzleGuidebookSound;

    private FMOD.Studio.EventInstance GhostUISound;

    void Start()
    {
        candyPuzzleGuidebookSound = FMODUnity.RuntimeManager.CreateInstance("event:/GuideBook/candyPuzzle");
        catPuzzleGuidebookSound = FMODUnity.RuntimeManager.CreateInstance("event:/GuideBook/catPuzzle");
        elementsPuzzleGuidebookSound = FMODUnity.RuntimeManager.CreateInstance("event:/GuideBook/elementsPuzzle");
        sicknessPuzzleGuidebookSound = FMODUnity.RuntimeManager.CreateInstance("event:/GuideBook/sicknessPuzzle");
        GhostUISound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UIGhostVoice");
    }

    void Update()
    {

    }

	public void PlaySoundCue( int pageNumber )
	{
		// TODO: Stebs, here you can add all the FMOD sounds
		switch ( pageNumber )
		{
			case 1: candyPuzzleGuidebookSound.start();
			break;

			case 2: catPuzzleGuidebookSound.start();
			break;

			case 3:
                elementsPuzzleGuidebookSound.start();

            break;

			case 4:
                sicknessPuzzleGuidebookSound.start();

            break;

			default:
			break;
		}
	}

	public void NextPage( )
	{
        GhostUISound.start();
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
        GhostUISound.start();
		if ( currentPage > 0 )
		{
			foreach ( var page in pages )
				page.SetActive( false );

			currentPage--;
			pages[currentPage].SetActive( true );
		}
	}

    private void OnEnable()
    {
        GhostUISound.start();
    }
}
