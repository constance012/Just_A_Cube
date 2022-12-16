using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMobile : MonoBehaviour
{
	// References.
	[SerializeField] private Slider controlSensi;
	[SerializeField] private TextMeshProUGUI controlSensiText;

	[SerializeField] private Slider volume;
	[SerializeField] private TextMeshProUGUI volumeText;

	[SerializeField] private TMP_Dropdown graphics;
	[SerializeField] private GameObject confirmResetPanel;

	private void Awake()
	{
		controlSensi = GameObject.Find("Control Slider").GetComponent<Slider>();
		controlSensiText = GameObject.Find("Control Sensitivity").GetComponent<TextMeshProUGUI>();

		volume = GameObject.Find("Volume Slider").GetComponent<Slider>();
		volumeText = GameObject.Find("Volume").GetComponent<TextMeshProUGUI>();

		graphics = GameObject.Find("Graphics Dropdown").GetComponent<TMP_Dropdown>();

		confirmResetPanel = GameObject.Find("Confirm Reset");
	}

	private void Start()
	{
		confirmResetPanel.SetActive(false);

		controlSensi.value = PlayerPrefs.GetFloat("ControlSensitivity", 40f);
		volume.value = PlayerPrefs.GetFloat("ThemeVolume", 1f);
		
		graphics.value = PlayerPrefs.GetInt("Graphics", 2);  // Default graphics is high.
	}

	public void BackToMenu()
	{
		FindObjectOfType<LoadNextLevel>().Load("Scenes/Menu");
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

		PlayerPrefs.SetInt("Graphics", 2);
		PlayerPrefs.SetFloat("ControlSensitivity", 40f);
		PlayerPrefs.SetFloat("ThemeVolume", 1f);

		// Set new value to the UIs.
		controlSensi.value = PlayerPrefs.GetFloat("ControlSensitivity", 1f);

		volume.value = PlayerPrefs.GetFloat("ThemeVolume", 40f);

		graphics.value = PlayerPrefs.GetInt("Graphics", 2);

		// Refresh the dropdowns, just in case.
		graphics.RefreshShownValue();

		// Deactivate the reset panel.
		confirmResetPanel.SetActive(false);
	}
}
