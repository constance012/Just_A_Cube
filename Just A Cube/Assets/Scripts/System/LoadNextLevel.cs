using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
	// Reference.
	public Animator transition;

	public float transitionTime = 1f;

	private void Awake()
	{
		transition = GetComponent<Animator>();
	}

	public void Load(string name)
	{
		StartCoroutine(LoadScene(name));
	}

	IEnumerator LoadScene(string name)
	{
		transition.SetTrigger("Start");

		yield return new WaitForSeconds(transitionTime);

		SceneManager.LoadScene(name);
	}
}
