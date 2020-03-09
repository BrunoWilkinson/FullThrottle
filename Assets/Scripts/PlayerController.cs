using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horsePower = 0f;
    [SerializeField] private const float turnSpeed = 45.0f;
    [SerializeField] float rpm;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float speed;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    void FixedUpdate()
    {
        GetInputs();

        if(IsOnGround())
        {
            MovePlayer();
            RotatePlayer();
            SetSpeed();
            SetRpm();
        }
    }

    void GetInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {
        playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

    void SetSpeed()
    {
        speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f);
        speedometerText.SetText("Speed: " + speed + "kph");
    }

    void SetRpm()
    {
        rpm = (speed % 30) * 40;
        rpmText.SetText("RPM: " + rpm);
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if(wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        return false;
    }
}
    