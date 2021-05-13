using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponEditorWindow : EditorWindow
{
    Weapon my;
    public static void Open(Weapon weapon)
    {
        WeaponEditorWindow window = GetWindow<WeaponEditorWindow>("Weapon Editor");
        window.my = weapon;
        window.my.id = -1;
    }
    public static void Open(Weapon weapon, int id)
    {
        WeaponEditorWindow window = GetWindow<WeaponEditorWindow>("Weapon Editor");
        window.my = weapon;
        window.my.id = id;
    }
    private void OnGUI()
    {
        if(my != null)
        {
            Save();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("box");
            GuiBase();
            GUILayout.EndVertical();
            GUILayout.Space(20);
            GUILayout.BeginVertical("box");
            GuiDmg();
            GuiAdvance();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
    void GuiBase()
    {
        GUILayout.Label("ID: " + my.id);
        GUILayout.BeginHorizontal();
        my.nameWeapon = EditorGUILayout.TextField("Name", my.nameWeapon);
        my.icon = (Sprite)EditorGUILayout.ObjectField("Icon", my.icon, typeof(Sprite), true);
        GUILayout.EndHorizontal();
        GUILayout.Label("Description");
        my.description = EditorGUILayout.TextArea(my.description, GUILayout.MinHeight(50f));
    }
    void GuiDmg()
    {
        GUILayout.Label("Damage");
        GUILayout.BeginHorizontal();
        my.minDmg = EditorGUILayout.IntField(my.minDmg);
        GUILayout.Label("-");
        my.maxDmg = EditorGUILayout.IntField(my.maxDmg);
        GUILayout.EndHorizontal();
        my.isPenetration = EditorGUILayout.Toggle("Penetration", my.isPenetration);
        if (my.minDmg < 0) my.minDmg = 0;
        if (my.maxDmg < my.minDmg) my.maxDmg = my.minDmg;
    }
    void GuiAdvance()
    {
        my.category = (Weapon.Category)EditorGUILayout.EnumPopup("Category", my.category);
        switch(my.category)
        {
            case Weapon.Category.Melee:
                my.amunitionCapacity = 0;
                SetRange(1.5f);
                break;
            case Weapon.Category.Magic:
                my.amunitionCapacity = 0;
                SetRange(30);
                break;
            case Weapon.Category.Range:
                my.amunitionCapacity = EditorGUILayout.IntField("Amunition Capacity", my.amunitionCapacity);
                if (my.amunitionCapacity < 1) my.amunitionCapacity = 1;
                SetRange(20);
                break;
        }
        my.typeWeapon = (Weapon.Type)EditorGUILayout.EnumPopup("Type", my.typeWeapon);
        GUILayout.Label("Range: " + my.range + "m");
    }
    void SetRange(float range)
    {
        switch(my.typeWeapon)
        {
            case Weapon.Type.One_Handed:
                my.range = range;
                break;
            case Weapon.Type.Two_Handed:
                my.range = range * 2;
                break;
        }
    }

    void Save()
    {
        if(GUILayout.Button("Save",GUILayout.Width(100)))
        {
            EditorUtility.SetDirty(my);
            AssetDatabase.SaveAssets();
        }
    }
}
