using UnityEngine;
using UnityEngine.Assertions;

public class SelectItem : MonoBehaviour
{
	[SerializeField] private LockView lockView = null;
	[SerializeField] private PlayerItem item = PlayerItem.None;
	[SerializeField] private Sprite sprite = null;

    void Start()
    {
		Assert.IsNotNull( lockView );
    }

    public void Select()
    {
		lockView.HoldItem( item, sprite );
    }
}
