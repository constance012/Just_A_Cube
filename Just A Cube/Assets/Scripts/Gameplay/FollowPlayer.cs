using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	// References.
	[SerializeField] private Transform player;

	// Fields.
	public Vector3 offset = new Vector3(0f, 2f, -6f);

	private void Awake()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	private void Update()
	{
		transform.position = player.position + offset;
	}
}
