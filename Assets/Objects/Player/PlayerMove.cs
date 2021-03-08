using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 4f;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canMove)
        {
            float moveX = Input.GetAxis("Horizontal");
            transform.Translate(playerSpeed * moveX * Time.deltaTime, 0, 0);
        }
    }
}
