using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameManager gameManager;
    private GameObject enemy;
    private bool canUse = true;
    // Start is called before the first frame update
    private void Update()
    {
        if (enemy)
            if (Input.GetAxis("Fire") == 1f && enemy.tag == "Enemy" && canUse)
            {
                canUse = false;
                gameManager.AttackEnemy(enemy);
                enemy = null;
                Invoke("setDelay", 1f);
            }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject;
        }
    }
    private void setDelay()
    {
        Debug.Log("zzz");
        canUse = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == enemy)
        {
            enemy = null;
        }
    }
}
