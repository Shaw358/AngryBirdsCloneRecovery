// GameManager
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Transform spawnLocation;

	private CameraMovement cameraMovement;

	private SlingShotLine slingShotLine;

	[SerializeField]
	private GameObject birdPrefab;

	private bool spawn;

	private float timer;

	private int birdsFired;

	public int score;

	[SerializeField]
	private TextMeshProUGUI textPro;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		slingShotLine = GameObject.Find("SlingshotFront").GetComponent<SlingShotLine>();
		spawnLocation = GameObject.Find("CentrePoint").GetComponent<Transform>();
	}

	private void Start()
	{
		score = 0;
		spawn = false;
		GameObject currentBird = Object.Instantiate(birdPrefab, spawnLocation.position, spawnLocation.rotation);
		slingShotLine.setCurrentBird(currentBird);
		slingShotLine.setLineRendererActive(active: true);
		birdsFired = 1;
	}

	private void Update()
	{
		textPro.text = score.ToString();
		timer += Time.deltaTime;
		if (spawn && (double)timer >= 1.5 && birdsFired < 5)
		{
			spawn = false;
			GameObject currentBird = Object.Instantiate(birdPrefab, spawnLocation.position, spawnLocation.rotation);
			slingShotLine.setCurrentBird(currentBird);
			birdsFired++;
			StartCoroutine(WaitAndEnableLineRenderer());
		}
	}

	public void setBool(bool set)
	{
		spawn = set;
		timer = 0f;
	}

	private IEnumerator WaitAndEnableLineRenderer()
	{
		yield return new WaitForSeconds(0.1f);
		slingShotLine.setLineRendererActive(true);
	}

	public void ResetGame()
	{
		score = 0;
		birdsFired = 0;
	}
}
