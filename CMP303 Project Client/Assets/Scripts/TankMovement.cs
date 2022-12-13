using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement made with reference to Brackeys
public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rigidBody;
    public Camera cam;

    Vector2 move;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        //Input
       move.x = Input.GetAxisRaw("Horizontal");
       move.y = Input.GetAxisRaw("Vertical");

       mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        //Movement
        rigidBody.MovePosition(rigidBody.position + move * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookAt = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = angle;
    }
}