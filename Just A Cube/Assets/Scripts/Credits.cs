using UnityEngine;

public class Credits : MonoBehaviour
{
	public void BackToMenu()
	{
		FindObjectOfType<LoadNextLevel>().Load("Scenes/Menu");
	}
}
