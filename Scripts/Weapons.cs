using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase Weapon", menuName = "DataBase/Weapons")]
public class Weapons : ScriptableObject
{
    public List<Weapon> weapons = new List<Weapon>();
}
