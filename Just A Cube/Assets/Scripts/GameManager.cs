using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	// References.
	[SerializeField] private CollisionCheck col;
	[SerializeField] private BlockSpawner spawner;

	[SerializeField] private Animator moveTutorAnim;
	[SerializeField] private Animator camTutorAnim;
	[SerializeField] private Animator scoreTextAnim;

	[SerializeField] private GameObject scoreText;
	[SerializeField] private GameObject moveTutor;
	[SerializeField] private GameObject cameraTutor;
	[SerializeField] private GameObject gameOverScreen;
	[SerializeField] private GameObject pauseScreen;

	TextMeshProUGUI scoreTMP;

	// Fields.
	public int scoreThreshold = 15;
	public float timeBetweenTutor = 3f;

	bool gameHasEnded = false;
	bool tutorial = false;
	public static bool isPaused = false;

	private void Awake()
	{
		col = GameObject.FindWithTag("Player").GetComponent<CollisionCheck>();
		spawner = GameObject.FindWithTag("Chooser").GetComponent<BlockSpawner>();
		
		scoreText = GameObject.FindWithTag("Score");
		scoreTextAnim = GameObject.FindWithTag("Score").GetComponent<Animator>();
		scoreTMP = scoreText.GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		
		scoreTMP.color = new Color(0.46f, 0.46f, 0.46f);
		
		gameOverScreen.SetActive(false);
		pauseScreen.SetActive(false);
		moveTutor.SetActive(false);
		cameraTutor.SetActive(false);
		scoreText.SetActive(false);

		if (PlayerPrefs.GetInt("HighScore", 0) == 0)
		{
			tutorial = true;
			spawner.enabled = false;
		}
	}

	// Update is called once per frame
	private void Update()
	{
		//Debug.Log(Time.fixedDeltaTime);
		if (tutorial)
		{
			StartCoroutine(PlayTutorial());
		}

		else
		{
			// Increasing difficulty based on score.
			IncreaseDifficulty();

			if (Time.timeSinceLevelLoad > 2f && !scoreText.activeInHierarchy)
			{
				scoreText.SetActive(true);
			}

			// Check for input to pause or resume the game.
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
				if (isPaused)
					ResumeGame();
				else
					PauseGame();
		}
	}

	public void GameOver()
	{
		if (!gameHasEnded)
		{
			Cursor.lockState = CursorLockMode.None;

			scoreTextAnim.SetTrigger("End");
			gameOverScreen.SetActive(true);
			gameHasEnded = true;
		}
	}

	public void RestartGame()
	{
		CameraSwitcher.index = 0;  // Reset the index to default: cam1.
		FindObjectOfType<LoadNextLevel>().Load(SceneManager.GetActiveScene().name);
	}

	public void ResumeGame()
	{
		Cursor.lockState = CursorLockMode.Locked;

		pauseScreen.SetActive(false);
		FindObjectOfType<TimeManager>().SlowdownTime();
		isPaused = false;
	}

	public void PauseGame()
	{
		Cursor.lockState = CursorLockMode.None;
		
		pauseScreen.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void ReturnToMenu()
	{
		// Unpause first so the animation can be played.
		Time.timeScale = 1f;
		isPaused = false;
		FindObjectOfType<LoadNextLevel>().Load("Scenes/Menu");
	}

	void IncreaseDifficulty()
	{
		if (col.score >= scoreThreshold && spawner.timeBetweenPatterns > 0.8f)
		{
			spawner.timeBetweenPatterns -= 0.2f;
			scoreThreshold *= 2;

			switch (col.score)
			{
				case 15:
					scoreTMP.color = new Color(0.2f, 0.32f, 0.75f);
					break;
				case 30:
					scoreTMP.color = new Color(0.25f, 0.7f, 0.18f);
					break;
				case 60:
					scoreTMP.color = new Color(0.76f, 0.52f, 0.16f);
					break;
				case 120:
					scoreTMP.color = new Color(0.76f, 0.16f, 0.2f);
					break;
				case 240:
					scoreTMP.color = new Color(0.76f, 0.16f, 0.72f);
					break;
				case 480:
					scoreTMP.color = new Color(0.49f, 0f, 1f);
					break;
			}
		}
	}

	IEnumerator PlayTutorial()
	{
		if (Time.timeSinceLevelLoad > 3f && !moveTutor.activeInHierarchy)
			moveTutor.SetActive(true);

		if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
			Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
			&& moveTutor.activeInHierarchy && !cameraTutor.activeInHierarchy)
		{
			yield return new WaitForSeconds(1f);

			moveTutorAnim.SetTrigger("End");

			yield return new WaitForSeconds(timeBetweenTutor);

			cameraTutor.SetActive(true);
		}

		if (Input.GetKeyDown(KeyCode.V) && cameraTutor.activeInHierarchy)
		{
			yield return new WaitForSeconds(1f);
			
			camTutorAnim.SetTrigger("End");

			yield return new WaitForSeconds(2f);

			scoreText.SetActive(true);
			cameraTutor.SetActive(false);
			moveTutor.SetActive(false);

			tutorial = false;
			spawner.enabled = true;
		}
	}
}
