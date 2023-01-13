using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float thrustForce;
    public float rotationForce;
    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
