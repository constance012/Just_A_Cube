using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    // Reference.
    [SerializeField] private Transform player;

	private void Awake()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	private void Update()
    {
		if ((player.position.z - transform.position.z) > 7f)
			Destroy(gameObject);
    }
}
