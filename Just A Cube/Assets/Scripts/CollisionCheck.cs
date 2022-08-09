using UnityEngine;
using TMPro;

public class CollisionCheck : MonoBehaviour
{
	// References.
	[SerializeField] private PlayerMovement moveScript;
	[SerializeField] private BlockSpawner spawnScript;

	[SerializeField] private Transform player;

	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI finishedScoreText;
	[SerializeField] private TextMeshProUGUI highScoreText;

	[SerializeField] private GameManager gameManager;

	// Fields.
	public int score = 0;
	public float restartDelay = 2f;
	bool collided = false;

	private void Awake()
	{
		//player = GetComponent<Rigidbody>();
		moveScript = GetComponent<PlayerMovement>();
		spawnScript = GameObject.FindWithTag("Chooser").GetComponent<BlockSpawner>();

		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

		player = GetComponent<Transform>();
		scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		highScoreText.color = Color.black;
		highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0).ToString();  // Default high score is 0, if previous high score hasn't been set yet.
	}

	private void Update()
	{
		if (moveScript.enabled)
			player.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

		if (player.position.x < -15f || player.position.x > 15f)
		{
			moveScript.enabled = false;
			spawnScript.enabled = false;

			gameManager.Invoke("GameOver", restartDelay);  // Call method after a provided delay time.
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Obstacle")
		{
			moveScript.enabled = false;
			spawnScript.enabled = false;

			if (!collided)  // Only slow down time 1 time.
			{
				FindObjectOfType<TimeManager>().SlowdownTime();
				Debug.Log("Slowed!!");
			}
			
			gameManager.Invoke("GameOver", restartDelay);
			collided = true;
		}
	}

	// When the player collides with the score check trigger.
	private void OnTriggerEnter(Collider other)
	{
		score++;
		scoreText.text = score.ToString();
		finishedScoreText.text = "Score: " + scoreText.text;

		if (score > PlayerPrefs.GetInt("HighScore", 0))
		{
			PlayerPrefs.SetInt("HighScore", score);
			highScoreText.color = Color.red;
			highScoreText.text = "New Best: " + score;
		}
	}
}
