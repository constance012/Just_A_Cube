using System.Collections;
using System;
using UnityEngine;

public class WindowResizeChecker : MonoBehaviour
{
	public static WindowResizeChecker instance;  // Ensure that there's only one WindowResizeChecker object is active at time.

	public static event Action<Vector2> OnResolutionChange;
	public static float CheckDelay = 0.5f;  // How long to wait until we check again.

	public static Vector2 res;  // Current Resolution.

	public static bool isAlive = true;  // Keep this script running?

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		OnResolutionChange += WhenWindowResized;  // Register the method to be called along with the resize event.
		Debug.Log("Registered");
	}

	private void Start()
	{
		StartCoroutine(CheckForChange());
	}
	
	IEnumerator CheckForChange()
	{
		res = new Vector2(Screen.width, Screen.height);

		while (isAlive)
		{
			yield return new WaitForEndOfFrame();

			// Check for a Resolution Change
			if ((res.x != Screen.width || res.y != Screen.height) && PlayerPrefs.GetInt("UseCustomSize", 0) == 1)
			{
				res = new Vector2(Screen.width, Screen.height);

				if (OnResolutionChange != null)
					OnResolutionChange(res);
			}

			yield return new WaitForSeconds(CheckDelay);

			PlayerPrefs.SetInt("ResizedWidth", Screen.width);
			PlayerPrefs.SetInt("ResizedHeight", Screen.height);
		}
	}

	void WhenWindowResized(Vector2 res)
	{
		Debug.Log("Resize event called.");
		Screen.SetResolution((int)res.x, (int)res.y, Screen.fullScreen);
	}
}
