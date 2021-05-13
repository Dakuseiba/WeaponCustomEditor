using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponsEditorWindow : EditorWindow
{
    Weapons my;
    Weapon selected;
    string newWeaponName;
    public static void Open(Weapons weapon)
    {
        WeaponsEditorWindow window = GetWindow<WeaponsEditorWindow>("Weapons Editor");
        window.my = weapon;
    }
    [MenuItem("Database/Weapons")]
    public static void Open()
    {
        WeaponsEditorWindow window = GetWindow<WeaponsEditorWindow>("Weapons Editor");
        window.my = (Weapons)AssetDatabase.LoadAssetAtPath("Assets/Database/DataBase Weapon.asset", typeof(Weapons));
    }

    private void OnGUI()
    {
        if(my!=null)
        {
            Save();
            newWeaponName = EditorGUILayout.TextField(newWeaponName);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                AddFunction();
            }
            if (GUILayout.Button("Remove") && my.weapons.Count > 0)
            {
                RemoveFunction(my.weapons.Count - 1);
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("Count: " + my.weapons.Count);
            ListWeapon();
        }
    }

    void AddFunction()
    {
        Weapon weapon = new Weapon(my.weapons.Count,newWeaponName);
        string path = "Assets/Database/Weapons/";
        var isExist = AssetDatabase.LoadAssetAtPath(path + newWeaponName + ".asset", typeof(Weapon));
        if (isExist != null)
        {
            CreateByNewName(1, path);
        }
        else
        {
            AssetDatabase.CreateAsset(weapon, path + newWeaponName + ".asset");
            AssetDatabase.SaveAssets();
            my.weapons.Add(weapon);
        }
    }

    void CreateByNewName(int count, string path)
    {
        Weapon weapon = new Weapon(my.weapons.Count, newWeaponName);
        string counter = " (" + count + ")";
        var isExist = AssetDatabase.LoadAssetAtPath(path + newWeaponName + counter + ".asset", typeof(Weapon));
        if(isExist!=null)
        {
            CreateByNewName(count + 1, path);
        }
        else
        {
            AssetDatabase.CreateAsset(weapon, path + newWeaponName + counter + ".asset");
            AssetDatabase.SaveAssets();
            my.weapons.Add(weapon);
        }
    }

    void RemoveFunction(Weapon weapon)
    {
        string path = "Assets/Database/Weapons/";
        var isExist = AssetDatabase.LoadAssetAtPath(path + weapon.name + ".asset", typeof(Weapon));
        my.weapons.Remove(weapon);
        if(isExist!=null)
        {
            AssetDatabase.DeleteAsset(path + weapon.name + ".asset");
        }
    }
    void RemoveFunction(int id)
    {
        if (my.weapons[id] == null) my.weapons.RemoveAt(id);
        else RemoveFunction(my.weapons[id]);
    }

    void ListWeapon()
    {
        for(int i=0;i<my.weapons.Count;i++)
        {
            if(my.weapons[i]!=null)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(i + "." + my.weapons[i].nameWeapon))
                {
                    WeaponEditorWindow.Open(my.weapons[i], i);
                    selected = my.weapons[i];
                }
                if(selected == my.weapons[i])
                {
                    if(GUILayout.Button("X",GUILayout.MaxWidth(20f),GUILayout.MaxHeight(20f)))
                    {
                        RemoveFunction(selected);
                        selected = null;
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
    void Save()
    {
        if (GUILayout.Button("Save", GUILayout.Width(100)))
        {
            EditorUtility.SetDirty(my);
            AssetDatabase.SaveAssets();
        }
    }
}
