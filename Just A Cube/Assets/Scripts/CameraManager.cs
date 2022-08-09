using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	// Camera references.
	[SerializeField] private CinemachineVirtualCamera cam1;
	[SerializeField] private CinemachineVirtualCamera cam2;
	[SerializeField] private CinemachineVirtualCamera cam3;

	private void Awake()
	{
		cam1 = GameObject.Find("CineCam 1").GetComponent<CinemachineVirtualCamera>();
		cam2 = GameObject.Find("CineCam 2").GetComponent<CinemachineVirtualCamera>();
		cam3 = GameObject.Find("CineCam 3").GetComponent<CinemachineVirtualCamera>();
	}

	private void OnEnable()
	{
		CameraSwitcher.Register(cam1);
		CameraSwitcher.Register(cam2);
		CameraSwitcher.Register(cam3);

		CameraSwitcher.SwitchCam();  // Default camera is cam1.
	}

	private void OnDisable()
	{
		CameraSwitcher.Unregister();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.V))
			CameraSwitcher.SwitchCam();
	}
}
