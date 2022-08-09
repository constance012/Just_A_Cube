using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	// Patterns array.
	[SerializeField] private Pattern[] patterns;

	// References.
	public GameObject obstaclePrefab;
	public GameObject scoreCheckPrefab;

	// Fields.
	private float timeToSpawn = 2.5f;  // Time to spawn the first wave.
	public float timeBetweenPatterns = 2f;  // Time to spawn the next wave after another.

	// Spawn a random pattern after 3 sec.
	private void Update()
	{
		if (Time.timeSinceLevelLoad >= timeToSpawn)
		{
			SpawnObstalces();
			timeToSpawn = Time.timeSinceLevelLoad + timeBetweenPatterns;
		}
	}

	private void SpawnObstalces()
	{
		// Choose a random pattern from the list.
		int randomIndex = Random.Range(0, patterns.Length);
		Pattern selectedPattern = patterns[randomIndex];

		// Spawn obstacles of that pattern.
		for (int i = 0; i < selectedPattern.spawnPoints.Length; i++)
		{
			if (i == selectedPattern.spawnPoints.Length - 1)
			{
				GameObject clone = Instantiate(scoreCheckPrefab, selectedPattern.spawnPoints[i].position, Quaternion.identity);
				clone.SetActive(true);
			}
			else
				Instantiate(obstaclePrefab, selectedPattern.spawnPoints[i].position, Quaternion.identity);
		}
	}
}
