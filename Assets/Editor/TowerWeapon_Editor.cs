using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerWeapon), true)]
public class TowerWeapon_Editor :  Editor
{
    public override void OnInspectorGUI()
    {
        
        var controller = (TowerWeapon) target;
        
        DrawDefaultInspector();
        
        if (controller.totalDPS != "")
        {
            GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
            labelStyle.fontSize = 20;
            labelStyle.fixedHeight = 80;
            labelStyle.alignment = TextAnchor.UpperLeft;
            
            EditorGUILayout.LabelField("Total Weapon DPS = " + controller.totalDPS, labelStyle);
        }
    }
}