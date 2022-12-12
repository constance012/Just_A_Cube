using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;

	public static AudioManager instance;  // Ensure that there's only one AudioManager object is existing in the game.

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			
			s.source.clip = s.clip;
			s.source.name = s.name;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	private void Start()
	{
		Play("Theme");
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);  // Find a sound with the matched name.
		
		if (s == null)
		{
			Debug.LogWarning("Audio Clip: " + name + " not found!!");
			return;
		}

		s.source.Play();
	}
}
