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
    [Header("Time")]
    public bool isRewinding = false;
    public float maxRewindTime = 5f;
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
        if (Input.GetAxisRaw("Time") == 1)
        {
            StartRewinding();
        }
        else
        {
            StopRewinding();
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
        //Debug.Log(canMove);
    }
    public void StartRewinding()
    {
        isRewinding = true;
    }
    public void StopRewinding()
    {
        isRewinding = false;
    }
    public void Rewind(List<Vector3> pos, GameObject obj)
    {
        if (pos.Count > 1)
        {
            obj.transform.position = pos[0];
            pos.RemoveAt(0);
            pos.RemoveAt(0);
        }
    }
}