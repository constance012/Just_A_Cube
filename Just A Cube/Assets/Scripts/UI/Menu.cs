using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
	// Static fields.
	public static Sound theme;
	//public static Resolution[] resolutionArr;

	public static bool isSet { get; private set; } = false;  // We only need to get the array once when the game started.

	public void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Graphics", 2));
		Time.timeScale = 1f;
		//Debug.Log(Time.timeScale);

		if (!isSet)
		{
			//resolutionArr = Screen.resolutions;
			Application.targetFrameRate = Screen.currentResolution.refreshRate;

			theme = Array.Find(AudioManager.instance.sounds, sound => sound.name == "Theme");

			if (theme == null)
			{
				Debug.LogWarning("Audio Clip: " + name + " not found!!");
				return;
			}

			theme.source.volume = PlayerPrefs.GetFloat("ThemeVolume", 1f);

			isSet = true;
		}
	}

	public void Credits()
	{
		FindObjectOfType<LoadNextLevel>().Load("Scenes/Credits");
	}

	public void Options()
	{
		FindObjectOfType<LoadNextLevel>().Load("Scenes/Options");
	}

	public void StartGame()
	{
		CameraSwitcher.index = 0;
		FindObjectOfType<LoadNextLevel>().Load("Scenes/Game");
		Time.timeScale = 1f;  // Set time scale back to normal if it was paused before.
	}

	public void QuitGame()
	{
		Debug.Log("Quiting...");
		WindowResizeChecker.isAlive = false;
		Application.Quit();
	}
}
