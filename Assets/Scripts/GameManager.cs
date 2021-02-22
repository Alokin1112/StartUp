using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int ammo = 2;
    private int score = 0;

    public void AttackEnemy(GameObject enemy)
    {
        if (ammo > 0)
        {
            ammo--;
            Destroy(enemy);
            score++;
        }
        else
        {
            Debug.Log("Not Enough Ammo");
        }
    }
    public void AddAmmo(int amount, GameObject destroyObject)
    {
        ammo += amount;
        Destroy(destroyObject, 1f);
        Debug.Log(ammo);
    }
}
