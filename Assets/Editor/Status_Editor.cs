using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DamageStatusEffect))]
public class Status_Editor :  Editor
{
    public override void OnInspectorGUI()
    {
        
        var controller = (DamageStatusEffect) target;
        
        DrawDefaultInspector();
        
        GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
        labelStyle.fontSize = 20;
        labelStyle.fixedHeight = 80;
        labelStyle.alignment = TextAnchor.UpperLeft;

        EditorGUILayout.LabelField("Total Damage = " + controller.totalDamage, labelStyle);
    }
}