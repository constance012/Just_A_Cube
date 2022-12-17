using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	// Camera references.
	[SerializeField] private CinemachineVirtualCamera cam1;
	[SerializeField] private CinemachineVirtualCamera cam2;
	[SerializeField] private CinemachineVirtualCamera cam3;
	private RectTransform centerPanel;

	[SerializeField] private float doubleTapWaitTime;

	private float doubleTapEndTime;
	private float tapCount = 0;

	private void Awake()
	{
		cam1 = GameObject.Find("CineCam 1").GetComponent<CinemachineVirtualCamera>();
		cam2 = GameObject.Find("CineCam 2").GetComponent<CinemachineVirtualCamera>();
		cam3 = GameObject.Find("CineCam 3").GetComponent<CinemachineVirtualCamera>();
		centerPanel = GameObject.Find("Center Panel").GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		CameraSwitcher.Register(cam1);
		CameraSwitcher.Register(cam2);
		CameraSwitcher.Register(cam3);

		CameraSwitcher.SwitchCam();  // Default camera is cam1.

		RenderSettings.fogMode = FogMode.Exponential;
	}

	private void OnDisable()
	{
		CameraSwitcher.Unregister();
	}

	// Update is called once per frame
	void Update()
	{
		RenderSettings.fogDensity = CameraSwitcher.activeCam == cam3 ? .01f : .014f;

		// Double tap the screen with 1 finger to change the camera.
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			Vector2 localTouchPos = centerPanel.transform.InverseTransformPoint(touch.position);

			if (centerPanel.rect.Contains(localTouchPos))
			{
				if (touch.phase == TouchPhase.Began)
					tapCount++;

				if (tapCount == 1)
					doubleTapEndTime = Time.time + doubleTapWaitTime;
				else if (tapCount == 2 && Time.time < doubleTapEndTime)
				{
					CameraSwitcher.SwitchCam();
					tapCount = 0;
				}
			}
		}

		if (Time.time > doubleTapEndTime || GameManager.isPaused)
			tapCount = 0;
	}
}
