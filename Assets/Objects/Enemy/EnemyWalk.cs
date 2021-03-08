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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNotStunned && Vector2.Distance(transform.position, player.position) > minDistance && Vector2.Distance(transform.position, player.position) < maxDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyMovementSpeed * Time.deltaTime);
        }
    }
    public void Stun(float amountOfTime)
    {
        changeStun();
        Debug.Log("Enemy Stunned");
        Invoke("changeStun", amountOfTime);
    }
    private void changeStun()
    {
        isNotStunned = !isNotStunned;
    }
}
