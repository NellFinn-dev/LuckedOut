using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Required namespace

public class ControllerRumble : MonoBehaviour
{
    // Amplitude values range from 0.0f to 1.0f
    public float lowFrequencyMotorSpeed = 0.1f;
    public float highFrequencyMotorSpeed = 0.1f;

    public float lowFrequencyMotorSpeedHigher = 0.4f;
    public float highFrequencyMotorSpeedHigher = 1f;

    public float rumbleDuration = 0.05f; // Duration in seconds

    public void callRumble(bool high)
    {
        // Check if a gamepad is currently connected
        if (Gamepad.current != null)
        {
            if (high)
            {
                StartRumbleHigh();
            } else
            {
                StartRumbleLow();
            }
            // Stop the rumble after the specified duration
            Invoke("StopRumble", rumbleDuration);
            Debug.Log("Calling Rumble");
        }
        else
        {
            Debug.LogWarning("No Gamepad connected for rumble testing.");
        }
    }

    public void StartRumbleLow()
    {
        // Set the motor speeds
        // The left motor is typically low frequency, the right is high frequency
        Gamepad.current.SetMotorSpeeds(lowFrequencyMotorSpeed, highFrequencyMotorSpeed);
    }

    public void StartRumbleHigh()
    {
        // Set the motor speeds
        // The left motor is typically low frequency, the right is high frequency
        Gamepad.current.SetMotorSpeeds(lowFrequencyMotorSpeedHigher, highFrequencyMotorSpeedHigher);
    }

    public void StopRumble()
    {
        // Set speeds back to zero to stop the vibration
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}