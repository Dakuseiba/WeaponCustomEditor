using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName ="Weapon/new Weapon")]
public class Weapon : ScriptableObject
{
    public string nameWeapon;
    public string description;

    public int id;
    public int minDmg;
    public int maxDmg;
    public int amunitionCapacity;

    public float range;
    public bool isPenetration;
    public Sprite icon;
    
    public Category category;
    public Type typeWeapon;
    public enum Category
    {
        Melee,
        Range,
        Magic
    }
    public enum Type
    {
        One_Handed,
        Two_Handed
    }

    public Weapon(int _id, string _name)
    {
        id = _id;
        nameWeapon = _name;
    }
}
