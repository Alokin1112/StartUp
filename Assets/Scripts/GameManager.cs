using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int paralyzedEnemies = 0;
    public float nightLengthInMinutes = .1f;
    private float currentTime;
    [Header("Player Settings")]
    public int ammo = 2;
    public bool canMove = true;
    public float stunDuration = 2f;
    [Header("User Interface")]
    public Text timeText;
    public Text ammoText;
    private void Start()
    {
        currentTime = nightLengthInMinutes * 60;
        ammoText.text = ammo.ToString();
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        timeText.text = ((int)currentTime / 60 + ":" + ((int)currentTime % 60).ToString("D2"));
        if (currentTime <= 0)
        {
            Debug.Log("Koniec Nocy");
            Time.timeScale = 0;
        }
    }
    public void AttackEnemy(GameObject enemy)
    {
        if (ammo > 0)
        {
            ammo--;
            ammoText.text = ammo.ToString();
            enemy.GetComponent<EnemyWalk>().Stun(stunDuration);
            paralyzedEnemies++;
        }
        else
        {
            Debug.Log("Not Enough Ammo");
        }
    }
    public void AddAmmo(int amount, GameObject destroyObject)
    {
        ammo += amount;
        ammoText.text = ammo.ToString();
        Destroy(destroyObject, 0.1f);
        Debug.Log(ammo);
    }
    public void switchMove()
    {
        canMove = !canMove;
        Debug.Log(canMove);
    }
}
