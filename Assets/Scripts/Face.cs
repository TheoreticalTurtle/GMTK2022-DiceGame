using UnityEngine;

public class Face : MonoBehaviour
{
    public int faceNum;
	public bool faceUp = false;
	RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}
	public void Update()
	{
		faceUp = rectTransform.position.y > 1.5;
	}
}
