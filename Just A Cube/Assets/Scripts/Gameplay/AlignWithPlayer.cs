using UnityEngine;

public class AlignWithPlayer : MonoBehaviour
{
	// References.
	[SerializeField] private Transform player;

	public int distance;

	private void Awake()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	private void Update()
	{
		float z = player.position.z + distance;
		transform.position = new Vector3(0f, 0f, z);
	}
}
