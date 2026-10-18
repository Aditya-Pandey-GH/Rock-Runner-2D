using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sounds
{
	public string name;
	public AudioClip clip;
	[HideInInspector] public AudioSource src;

	[Range(0f, 1f)] public float volume = 0.5f;
	// public float pitch = 1f;
	public bool loop;
}
