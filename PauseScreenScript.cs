// PauseScreenScript
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour
{
  [SerializeField]
	private GameObject pauseScreen;
  
  [SerializeField]
	private GameObject pauseButton;

	[SerializeField]
  private Transform canvas;

	[SerializeField]
  private AudioListener audioListener;

	[SerializeField]
  private List<AudioSource> audioSources;

	[SerializeField]
  private GameManager gameManager;

	[SerializeField]
  private bool opened;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Start()
	{
		ClosePauseScreen();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !opened)
		{
			OpenPauseScreen();
			opened = true;
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && opened)
		{
			ClosePauseScreen();
			opened = false;
		}
	}

	public void OpenPauseScreen()
	{
		pauseScreen.SetActive(value: true);
		pauseButton.SetActive(value: false);
		Time.timeScale = 0f;
	}

	public void ClosePauseScreen()
	{
		pauseScreen.SetActive(value: false);
		pauseButton.SetActive(value: true);
		Time.timeScale = 1f;
	}

	public void ResetGame()
	{
		SceneManager.LoadScene("SampleScene");
		pauseButton.SetActive(value: true);
		pauseScreen.SetActive(value: false);
		Time.timeScale = 1f;
		gameManager.ResetGame();
	}

	public void MuteSounds()
	{
		foreach (AudioSource audioSource in audioSources)
		{
			audioSource.Stop();
		}
	}
}
