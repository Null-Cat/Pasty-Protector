using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateConstrained : Physics2DObject
{
    [Header("Input keys")]
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

    [Header("Rotation")]
    public float speed = 5f;

    [Header("Movement Constraint")]
    [Range(0, 1)]
    public float constraintRotationAmount = 0.3f;

    private float spin;


    // Update gets called every frame
    void Update()
    {
        // Register the spin from the player input
        // Moving with the arrow keys
        if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        {
            spin = Input.GetAxis("Horizontal");
        }
        else
        {
            spin = Input.GetAxis("Horizontal2");
        }
    }


    // FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        //Freezes the objects rotation if it hits the constrained bounds
        if ((this.transform.rotation.z >= constraintRotationAmount && spin <= 0f) || (this.transform.rotation.z <= -constraintRotationAmount && spin >= 0f))
        {
            rigidbody2D.freezeRotation = true;
        }
        else
        {
            rigidbody2D.freezeRotation = false;
            // Apply the torque to the Rigidbody2D
            rigidbody2D.AddTorque(-spin * speed);
        }
    }
}
