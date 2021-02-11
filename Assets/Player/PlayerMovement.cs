using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 1f;
    public Rigidbody2D rigidbody;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * playerSpeed * Time.fixedDeltaTime);

    }
}
