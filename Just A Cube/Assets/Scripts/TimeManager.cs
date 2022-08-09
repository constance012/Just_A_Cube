using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public float slowdownFactor = 0.05f;  // How slow we want the effect will be.
	public float length = 2f;  // Slow for 2 secs.
	
	// Update is called once per frame
	void Update()
	{
		if (!GameManager.isPaused)
		{
			Time.timeScale += (1f / length) * Time.unscaledDeltaTime;  // Gradually increasing the time scale back to 1.
			Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);  // Limit the time scale between 0 and 1, otherwise the game will speed up.
			Time.fixedDeltaTime = Time.timeScale * 0.01f;  // Update the fixed delta time too.
		}
	}

	public void SlowdownTime()
	{
		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.01f;  // Update the fixed delta time of the physics engine.
	}
}
