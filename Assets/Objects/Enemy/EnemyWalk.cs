using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    private bool isNotStunned = true;
    private Transform player;
    public float enemyMovementSpeed = .1f;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    private float direction;
    private Vector2 Rotation;
    private Vector2 dir;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Rotation = new Vector2(transform.localScale.x, transform.localScale.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNotStunned && Vector2.Distance(transform.position, player.position) > minDistance && Vector2.Distance(transform.position, player.position) < maxDistance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, player.position, enemyMovementSpeed * Time.deltaTime);
            direction = player.transform.position.x - transform.position.x;
            RotateEnemy(direction);
            dir = new Vector2(direction % 1, 0);
            moveEnemy(dir);
        }
        else
        {
            dir = new Vector2(0, 0);
            moveEnemy(dir);
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
    private void moveEnemy(Vector2 dir)
    {
        rb.velocity = dir * enemyMovementSpeed;
    }
}
