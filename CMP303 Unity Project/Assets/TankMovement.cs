using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement made with reference to Brackeys
public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rigidBody;

    Vector2 move;

    // Update is called once per frame
    void Update()
    {
        //Input
       move.x = Input.GetAxisRaw("Horizontal");
       move.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        //Movement
        rigidBody.MovePosition(rigidBody.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}