using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 4f;
    public GameManager gameManager;
    private Vector2 Rotation;
    private Vector2 Movement;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Rotation = new Vector2(transform.localScale.x, transform.localScale.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canMove)
        {
            float moveX = Input.GetAxis("Horizontal");
            RotatePlayer(moveX);
            Movement = new Vector2(moveX, 0);
        }
        else
        {
            Movement = new Vector2(0, 0);
        }
    }
    private void FixedUpdate()
    {
        moveCharacter(Movement);
    }
    private void moveCharacter(Vector2 direction)
    {
        rb.velocity = direction * playerSpeed;
    }
    private void RotatePlayer(float direction)
    {
        if (direction > 0f)
        {
            transform.localScale = Rotation;
        }
        else if (direction < 0f)
        {
            transform.localScale = new Vector2(-Rotation.x, Rotation.y);
        }
    }
}
