using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameManager gameManager;
    private GameObject enemy;
    private bool canUse = true;
    private bool canSwap = true;
    public Transform player;
    public WeaponClass[] Weapons = new WeaponClass[2];
    private int usingWeapon = 0;
    private void Start()
    {
        Weapons[0] = new WeaponClass("Taser", 3, .5f, 2f);
        Weapons[1] = new WeaponClass("Trap", 2, 2f, 5f);
        gameManager.changeAmmoText(Weapons[usingWeapon].ammo);
    }
    private void Update()
    {
        transform.position = player.position;
        if (Input.GetAxisRaw("switchWeapon") == 1f && canSwap)
            switchWeapon();
        if (Input.GetAxis("useWeapon") == 1f && canUse)
            useWeapon();

    }
    private void useWeapon()
    {
        if (canUse && Weapons[usingWeapon].hasAmmo())
            switch (Weapons[usingWeapon].name)
            {
                case "Taser":
                    {
                        if (enemy && enemy.tag == "Enemy")
                        {
                            canUse = false;
                            Weapons[usingWeapon].substractAmmo();
                            gameManager.changeAmmoText(Weapons[usingWeapon].ammo);
                            gameManager.AttackEnemy(enemy, Weapons[usingWeapon].stunTime);
                            enemy = null;
                            Invoke("setDelay", Weapons[usingWeapon].castTime);
                        }
                    }
                    break;
                case "Trap":
                    {
                        canUse = false;
                        Weapons[usingWeapon].substractAmmo();
                        gameManager.changeAmmoText(Weapons[usingWeapon].ammo);
                        gameManager.setTrap(Weapons[usingWeapon].castTime);
                        Invoke("setDelay", Weapons[usingWeapon].castTime);
                    }
                    break;
            }
    }
    public void switchWeapon()
    {
        canSwap = false;
        usingWeapon = (usingWeapon + 1) % Weapons.Length;
        Debug.Log(usingWeapon);
        gameManager.changeAmmoText(Weapons[usingWeapon].ammo);
        Invoke("setSwap", 0.5f);
    }
    public void AddAmmo(int amount = 1)
    {
        Weapons[usingWeapon].addAmmo(amount);
        gameManager.changeAmmoText(Weapons[usingWeapon].ammo);
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
        Debug.Log("Can Shoot");
        canUse = true;
    }
    private void setSwap()
    {
        canSwap = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == enemy)
        {
            enemy = null;
        }
    }
}
