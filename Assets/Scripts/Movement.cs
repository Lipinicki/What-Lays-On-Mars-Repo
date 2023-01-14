using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] float thrustForce = 0;
    [SerializeField] float rotationForce = 0;
    [SerializeField] AudioClip thrustEngineSound = null;

	Rigidbody playerRb;
    AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
			playerRb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime, ForceMode.Force);

            if (!playerAudio.isPlaying)
            {
                playerAudio.PlayOneShot(thrustEngineSound, 1f);
            }
		}
        else
        {
            playerAudio.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotationForce);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotationForce);
        }
    }

    void Rotate(float rotationValue)
    {
        playerRb.freezeRotation = true; // Freezes the physics rotation so the player can rotate freely
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        playerRb.freezeRotation = false; // Unfreezes rotation
    }
}
