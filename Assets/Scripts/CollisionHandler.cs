using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] float delayToLoadScene = 0;

	[SerializeField] AudioClip crashSound = null;
	[SerializeField] AudioClip successSound = null;

	[SerializeField] ParticleSystem crashParticles = null;
	[SerializeField] ParticleSystem successParticles = null;

	AudioSource playerAudio;

	bool isTransitioning = false;
	bool isCollisionDisabled = false;

	void Start()
	{
		playerAudio = GetComponent<AudioSource>();
	}

	void Update()
	{
		HandleCheat();
	}

	void HandleCheat()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			isCollisionDisabled = !isCollisionDisabled;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (isTransitioning || isCollisionDisabled) return;

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				print("Collided with a Friendly object!");
				break;
			case "Finish":
				StartSuccessSequence();
				break;
			default:
				StartCrashSequence();
				break;
		}
	}

	void StartSuccessSequence()
	{
		isTransitioning = true;
		playerAudio.Stop();
		successParticles.Play();
		playerAudio.PlayOneShot(successSound, 1f);
		GetComponent<Movement>().enabled = false;
		Invoke("LoadNextLevel", delayToLoadScene);
	}

	void StartCrashSequence()
	{
		isTransitioning = true;
		playerAudio.Stop();
		crashParticles.Play();
		playerAudio.PlayOneShot(crashSound, 0.5f);
		GetComponent<Movement>().enabled = false;
		Invoke("ReloadLevel", delayToLoadScene);
	}

	void ReloadLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
	}

	void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneIndex + 1;
		if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextSceneIndex = 0;
		}

		SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
	}
}
