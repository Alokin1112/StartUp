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
    public bool canMove = true;
    public Transform player;
    public PlayerAttack pAttackScript;
    [Header("User Interface")]
    public Text timeText;
    public Text ammoText;
    public Text rewindTimeText;
    public Scrollbar usingProgressBar;
    public Text weaponName;
    private float maxProgressStatus;
    private float loadingProgress = 0f;
    [Header("Time")]
    public bool isRewinding = false;
    public float maxRewindTime = 10f;
    public float usedRewindTime = 0f;
    public float leftRewindTime;
    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject trapPrefab;
    [Header("EnemySpawn")]
    public int maxEnemySpawnAmount = 3;
    public float minSpawnTime = 20f;
    public float maxSpawnTime = 45f;
    public Transform[] spawners;
    private void Start()
    {
        currentTime = nightLengthInMinutes * 60;
        rewindTimeText.text = maxRewindTime.ToString();
        leftRewindTime = maxRewindTime;
        SpawnEnemy();
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
        if (Input.GetAxisRaw("Time") == 1 && canMove)
        {
            StartRewinding();
        }
        else
        {
            StopRewinding();
        }
        if (usingProgressBar.IsActive())
        {
            loadingProgress += Time.deltaTime;
            usingProgressBar.size = loadingProgress / maxProgressStatus;
            if (loadingProgress >= maxProgressStatus)
            {
                usingProgressBar.gameObject.SetActive(false);
            }
        }
    }

    public void switchMove()
    {
        canMove = !canMove;
        //Debug.Log(canMove);
    }
    ///Rewind Mechanic
    public void StartRewinding()
    {
        isRewinding = true;
    }
    public void StopRewinding()
    {
        isRewinding = false;

    }
    public void showProgressBar(float maxTime)
    {
        if (maxTime > 0)
        {
            maxProgressStatus = maxTime;
            loadingProgress = 0f;
            usingProgressBar.gameObject.SetActive(true);
        }
    }
    public void Rewind(List<Vector3> pos, GameObject obj)
    {
        rewindTimeText.text = leftRewindTime.ToString("F2");
        if (pos.Count > 1 && leftRewindTime > 0)
        {
            obj.transform.position = pos[0];
            pos.RemoveAt(0);
            usedRewindTime += Time.deltaTime;
            leftRewindTime = maxRewindTime - usedRewindTime;
        }
    }
    ///Weapons
    public void AddAmmo(int amount, GameObject destroyObject)
    {
        pAttackScript.AddAmmo(amount);
        Destroy(destroyObject, 0.1f);
    }
    public void AttackEnemy(GameObject enemy, float stunDuration)
    {
        {
            enemy.GetComponent<EnemyWalk>().Stun(stunDuration);
            paralyzedEnemies++;
        }
    }
    public void setTrap(float castTime)
    {
        canMove = false;
        Vector3 pos = new Vector3(player.position.x, player.position.y - 0.4f, player.position.z);
        Invoke("switchMove", castTime);
        Instantiate(trapPrefab, pos, Quaternion.identity);
        showProgressBar(castTime);
    }
    public void changeAmmoText(int amount, string _weaponName)
    {
        ammoText.text = amount.ToString();
        weaponName.text = _weaponName;
    }
    private void SpawnEnemy()
    {
        Debug.Log("SpawnWorks");
        if (maxEnemySpawnAmount > 0)
        {
            maxEnemySpawnAmount--;
            int spawnIndex = Random.Range(0, spawners.Length);
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            Instantiate(enemyPrefab, spawners[spawnIndex].position, Quaternion.identity);
            Invoke("SpawnEnemy", spawnTime);
        }
    }
}