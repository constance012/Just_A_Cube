using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
	static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera> ();

	public static CinemachineVirtualCamera activeCam = null;

	public static int index = 0;

	public static bool isActive(CinemachineVirtualCamera cam)
	{
		return cam == activeCam;
	}

	public static void SwitchCam()
	{
		if (index > 2)
			index = 0;

		cameras[index].Priority = 20;
		activeCam = cameras[index];

		// Iterate through the list of cameras and set the priority of those which are not the active cam to 0.
		foreach (CinemachineVirtualCamera c in cameras)
			if (c.Priority != 0 && c != activeCam)
				c.Priority = 0;

		index++;
	}

	public static void Register(CinemachineVirtualCamera cam)
	{
		cameras.Add(cam);
		Debug.Log("Registered camera: " + cam);
	}

	public static void Unregister()
	{
		Debug.Log("Unregister all the cams.");
		cameras.Clear();
	}
}
