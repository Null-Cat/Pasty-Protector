using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveHorizontalConstrained : Physics2DObject
{
    [Header("Input keys")]
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;
    [Header("Movement")]
    [Tooltip("Speed of movement")]
    public float speed = 5f;
    [Header("Movement Constraint")]
    [Tooltip("How Far Movement is Constrained")]
    public float constrainedDistance = 5f;
    private Vector2 movement;
    private float moveHorizontal, initalPositionX, constraintOffsetXPosition;

    //Is called once when object is created
    void Start()
    {
        //Gets positional offset from current position
        initalPositionX = this.transform.position.x;
        constraintOffsetXPosition = initalPositionX + constrainedDistance;
    }
    // Update is called once per frame
    void Update()
    {
        //Gets input from either the arrow keys or WASD
        if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
        }
        //Defines Horizontal Movement
        movement = new Vector2(moveHorizontal, 0f);
    }

    //FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        //Sets the objects velocity to 0 if it hits the constrained bounds
        if ((this.transform.position.x >= constraintOffsetXPosition && moveHorizontal >= 0f) || (this.transform.position.x <= (constraintOffsetXPosition * -1) && moveHorizontal <= 0f))
        {
            rigidbody2D.velocity = new Vector2(0f, 0f);
        }
        else
        {
            //Apply the force to the Rigidbody2d
            rigidbody2D.AddForce(movement * speed * 5f);
        }
    }
}
