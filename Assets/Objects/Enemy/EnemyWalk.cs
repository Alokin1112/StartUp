using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    private bool isNotStunned = true;
    private bool isNotChasing = true;
    private float walkingTime = 2.5f;
    private Transform player;
    public float enemyMovementSpeed = .1f;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    public float maxYDistance = 0.5f;
    private float direction;
    private Vector2 Rotation;
    private Vector2 dir;
    private Vector2 normalWalkingSpeed = new Vector2(2, 0);
    private Rigidbody2D rb;
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Rotation = new Vector2(transform.localScale.x, transform.localScale.y);
        rb = GetComponent<Rigidbody2D>();
        StartWalking();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dist = new Vector2(Mathf.Abs(transform.position.x - player.position.x), Mathf.Abs(transform.position.y - player.position.y));
        if (isNotStunned && dist.x > minDistance && dist.x < maxDistance && dist.y < maxYDistance)
        {
            isNotChasing = false;
            //transform.position = Vector2.MoveTowards(transform.position, player.position, enemyMovementSpeed * Time.deltaTime);
            direction = player.transform.position.x - transform.position.x;
            RotateEnemy(direction);
            dir = new Vector2(direction % 1, 0);
            moveEnemy(dir);
        }
        else if (!isNotStunned)
        {
            dir = new Vector2(0, 0);
            moveEnemy(dir);
        }
        else
        {
            isNotChasing = true;

        }
    }
    public void Stun(float amountOfTime)
    {
        changeStun();
        Debug.Log("Enemy Stunned");
        enemyMovementSpeed = enemyMovementSpeed * 0.75f;
        Invoke("changeStun", amountOfTime);
    }
    private void changeStun()
    {
        isNotStunned = !isNotStunned;
        col.isTrigger = !col.isTrigger;
    }
    private void RotateEnemy(float direction)
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
    private void StopWalking()
    {
        Vector2 velocity = rb.velocity;
        if (isNotChasing)
            rb.velocity = new Vector2(0, 0);
        Invoke("StartWalking", walkingTime);
    }
    private void StartWalking()
    {
        normalWalkingSpeed *= (-1f);
        if (isNotChasing)
        {
            rb.velocity = normalWalkingSpeed;
            RotateEnemy(normalWalkingSpeed.x);
        }
        Invoke("StopWalking", walkingTime);
    }
    private void moveEnemy(Vector2 dir)
    {
        rb.velocity = dir * enemyMovementSpeed;
    }
}
