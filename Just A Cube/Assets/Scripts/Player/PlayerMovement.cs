using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// References.
	[SerializeField] private Rigidbody player;

	// Fields.
	public float forwardForce = 3000f;
	public float sidewaysForce = 40f;

	private void Awake()
	{
		player = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		sidewaysForce = PlayerPrefs.GetFloat("ControlSensitivity", 40f);
		Debug.Log(sidewaysForce);
	}

	// Use FixedUpdate to change stuff related to physics, cuz it's always update every 0.02s.
	private void FixedUpdate()
	{
		player.AddForce(0, 0, forwardForce * Time.deltaTime);
		//Debug.Log(sidewaysForce);

		//if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		//	player.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

		//if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		//	player.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
	}

	public void OnHoldLeft()
	{
		player.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
	}

	public void OnHoldRight()
	{
		player.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
	}
}
