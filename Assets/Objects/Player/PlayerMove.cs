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
    private List<Vector3> positions;
    // Start is called before the first frame update
    void Start()
    {
        Rotation = new Vector2(transform.localScale.x, transform.localScale.y);
        rb = GetComponent<Rigidbody2D>();
        positions = new List<Vector3>();
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
        Debug.Log(gameManager.isRewinding);
    }
    private void FixedUpdate()
    {
        moveCharacter(Movement);
        if (gameManager.isRewinding)
        {
            gameManager.Rewind(positions, this.gameObject);
        }
        else
        {
            Record();
        }
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
    private void Record()
    {
        if (positions.Count > (gameManager.leftRewindTime / Time.fixedDeltaTime))
            positions.RemoveAt(positions.Count - 1);
        positions.Insert(0, transform.position);
    }
}
