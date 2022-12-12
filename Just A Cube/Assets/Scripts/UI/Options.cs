using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
	// References.
	[SerializeField] private Slider controlSensi;
	[SerializeField] private TextMeshProUGUI controlSensiText;

	[SerializeField] private Slider volume;
	[SerializeField] private TextMeshProUGUI volumeText;

	[SerializeField] private TMP_Dropdown graphics;
	[SerializeField] private TMP_Dropdown resolution;

	[SerializeField] private Toggle fullscreen;

	[SerializeField] private GameObject tooltipButton;
	[SerializeField] private GameObject confirmResetPanel;
	[SerializeField] private GameObject tooltip;

	// Fields.
	int fullScreenIndex;

	private void Awake()
	{
		controlSensi = GameObject.Find("Control Slider").GetComponent<Slider>();
		controlSensiText = GameObject.Find("Control Sensitivity").GetComponent<TextMeshProUGUI>();

		volume = GameObject.Find("Volume Slider").GetComponent<Slider>();
		volumeText = GameObject.Find("Volume").GetComponent<TextMeshProUGUI>();

		graphics = GameObject.Find("Graphics Dropdown").GetComponent<TMP_Dropdown>();
		resolution = GameObject.Find("Resolution Dropdown").GetComponent<TMP_Dropdown>();

		fullscreen = GameObject.Find("Fullscreen Toggle").GetComponent<Toggle>();

		tooltipButton = GameObject.Find("Tooltip Trigger");
		confirmResetPanel = GameObject.Find("Confirm Reset");
		tooltip = GameObject.Find("Tooltip");
	}

	private void Start()
	{
		confirmResetPanel.SetActive(false);
		tooltip.SetActive(false);

		SetUpResoDropdown();
		resolution.RefreshShownValue();

		controlSensi.value = PlayerPrefs.GetFloat("ControlSensitivity", 40f);
		volume.value = PlayerPrefs.GetFloat("ThemeVolume", 1f);
		
		graphics.value = PlayerPrefs.GetInt("Graphics", 2);  // Default graphics is high.

		resolution.value = PlayerPrefs.GetInt("Resolution", 7);  // Default resolution is 1280 x 720 windowed.
		
		fullscreen.isOn = PlayerPrefs.GetInt("Fullscreen", 0) == 1 ? true : false;  // This will invoke the bounded method too.
		
		resolution.interactable = !fullscreen.isOn;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
			fullscreen.isOn = !fullscreen.isOn;
	}

	public void BackToMenu()
	{
		FindObjectOfType<LoadNextLevel>().Load("Scenes/Menu");
	}

	public void ShowTooltip()
	{
		tooltip.SetActive(true);
	}

	public void HideTooltip()
	{
		tooltip.SetActive(false);
	}

	// The UI will call these methods whenever its value changed, manually or directly.
	public void SetThemeVolume(float value)
	{
		Menu.theme.source.volume = value;
		PlayerPrefs.SetFloat("ThemeVolume", value);
		volumeText.text = "VOLUME: " + (PlayerPrefs.GetFloat("ThemeVolume", 1f) * 100).ToString("0");
	}

	public void SetSidewaysForce(float value)
	{
		PlayerPrefs.SetFloat("ControlSensitivity", value);
		controlSensiText.text = "CONTROL SENSITIVITY: " + PlayerPrefs.GetFloat("ControlSensitivity", 40f).ToString("0");
	}

	public void SetQuality(int index)
	{
		QualitySettings.SetQualityLevel(index);
		PlayerPrefs.SetInt("Graphics", index);
	}

	public void SetFullscreen(bool isFullsreen)
	{
		if (isFullsreen)
			resolution.value = fullScreenIndex;
		else if (!resolution.interactable)
			resolution.value = 7;  // Return to default resolution only once time.

		resolution.interactable = !isFullsreen;  // The user can't change to other resolutions if the game is fullscreen.
		tooltipButton.SetActive(isFullsreen);

		Screen.fullScreen = isFullsreen;
		
		PlayerPrefs.SetInt("Fullscreen", isFullsreen ? 1 : 0);  // If true then return 1, else 0.
	}

	public void SetResolution(int index)
	{
		Resolution selectedResolution = Menu.resolutionArr[index];
		Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

		PlayerPrefs.SetInt("Resolution", index);
	}

	public void ResetSelected()
	{
		confirmResetPanel.SetActive(true);
	}

	public void CancelReset()
	{
		confirmResetPanel.SetActive(false);
	}

	public void ConfirmReset()
	{
		// Perform reset.
		PlayerPrefs.SetInt("HighScore", 0);

		PlayerPrefs.SetInt("Fullscreen", 0);
		PlayerPrefs.SetInt("Resolution", 7);
		
		PlayerPrefs.SetInt("Graphics", 2);
		PlayerPrefs.SetFloat("ControlSensitivity", 40f);
		PlayerPrefs.SetFloat("ThemeVolume", 1f);

		// Set new value to the UIs.
		controlSensi.value = PlayerPrefs.GetFloat("ControlSensitivity", 1f);

		volume.value = PlayerPrefs.GetFloat("ThemeVolume", 40f);

		graphics.value = PlayerPrefs.GetInt("Graphics", 2);
		
		resolution.value = PlayerPrefs.GetInt("Resolution", 7);
		
		fullscreen.isOn = PlayerPrefs.GetInt("Fullscreen", 0) == 1 ? true : false;

		// Refresh the dropdowns, just in case.
		graphics.RefreshShownValue();
		resolution.RefreshShownValue();

		// Deactivate the reset panel.
		confirmResetPanel.SetActive(false);
		tooltip.SetActive(false);
	}

	void SetUpResoDropdown()
	{
		resolution.ClearOptions();  // Clear all the placeholder options.
		
		List<string> options = new List<string>();

		for (int i = 0; i < Menu.resolutionArr.Length; i++)
		{
			string option = Menu.resolutionArr[i].width + " x " + Menu.resolutionArr[i].height;
			
			if (!options.Contains(option))
				options.Add(option);
		}

		// Update the dropdown each time the options scene is loaded.
		resolution.AddOptions(options);
		fullScreenIndex = options.Count - 1;  // Last index.
	}
}
