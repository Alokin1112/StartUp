
using UnityEngine;

public class WeaponClass
{
    public string name;
    public int ammo;
    public float castTime;
    public float stunTime;
    public WeaponClass(string _name, int _ammo, float _castTime, float _stunTime)
    {
        name = _name;
        ammo = _ammo;
        castTime = _castTime;
        stunTime = _stunTime;
    }
    public bool hasAmmo()
    {
        if (ammo > 0)
            return true;
        return false;
    }
    public void substractAmmo(int amount = 1)
    {
        ammo -= amount;
    }
    public void addAmmo(int amount = 1)
    {
        ammo += amount;
    }
}
