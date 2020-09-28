// SlingShotLine
using UnityEngine;

public class SlingShotLine : MonoBehaviour
{
	private LineRenderer lineRendererFront;

	private LineRenderer lineRendererBack;

	[SerializeField]
	private GameObject currentBird;

	private void Awake()
	{
		lineRendererFront = GameObject.Find("LineRendererFront").GetComponent<LineRenderer>();
		lineRendererBack = GameObject.Find("LineRendererBack").GetComponent<LineRenderer>();
	}

	private void Update()
	{
		lineRendererFront.SetPosition(0, new Vector3(lineRendererFront.transform.position.x, lineRendererFront.transform.position.y, 0f));
		lineRendererBack.SetPosition(0, new Vector3(lineRendererBack.transform.position.x, lineRendererBack.transform.position.y, 0f));
		lineRendererFront.SetPosition(1, new Vector3(currentBird.transform.position.x, currentBird.transform.position.y, 0f));
		lineRendererBack.SetPosition(1, new Vector3(currentBird.transform.position.x, currentBird.transform.position.y, 0f));
	}

	public void setLineRendererActive(bool active)
	{
		if (!active)
		{
			lineRendererBack.enabled = false;
			lineRendererFront.enabled = false;
		}
		else if (active)
		{
			lineRendererBack.enabled = true;
			lineRendererFront.enabled = true;
		}
	}

	public void setCurrentBird(GameObject bird)
	{
		currentBird = bird;
	}
}
