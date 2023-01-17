using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = Vector3.zero;
    [SerializeField] float period = 2f;
    
    float movementFactor = 0;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return; // Asserts that period != 0
        float cycles = Time.time / period; // A cycle is specified by a period and continually grows overtime
        const float tau = Mathf.PI * 2; // tau is a constant that represents a full circle in radians (6.28f)
        float rawSinWave = Mathf.Sin(cycles * tau); // Returns a Sine wave that goes from -1f to 1f

        movementFactor = (rawSinWave + 1) / 2; // Recalculates the sine wave to goes from 0 to 1f

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
	}
}   
