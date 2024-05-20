using System;
using UnityEditor;

[CustomEditor(typeof(BuffObject))]
public class BuffObject_Editor : Editor
{
    private SerializedProperty _name;
    private SerializedProperty _buffInfo;
    
    private SerializedProperty _buffType;
    
    private SerializedProperty _enemyBuffType;
    private SerializedProperty _towerBuffType;
    private SerializedProperty _towerWeaponBuffType;
    private SerializedProperty _towerWeaponDamageType;

    private void OnEnable()
    {
        _name = serializedObject.FindProperty("Name");
        _buffInfo = serializedObject.FindProperty("BuffInfo");
        
        _buffType = serializedObject.FindProperty("BuffType");
        
        _enemyBuffType = serializedObject.FindProperty("EnemyBuffType");
        _towerBuffType = serializedObject.FindProperty("TowerBuffType");
        _towerWeaponBuffType = serializedObject.FindProperty("TowerWeaponBuffType");
        _towerWeaponDamageType = serializedObject.FindProperty("WeaponDamageType");
    }

    public override void OnInspectorGUI()
    {
        var controller = (BuffObject) target;
        
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(_name);
        
        EditorGUILayout.PropertyField(_buffType);
        
        switch (controller.BuffType)
        {
            case BuffType.Tower:
                EditorGUILayout.PropertyField(_towerBuffType);
                break;            
            case BuffType.TowerWeapon:
                EditorGUILayout.PropertyField(_towerWeaponBuffType);
                EditorGUILayout.PropertyField(_towerWeaponDamageType);
                break;            
            case BuffType.Enemy:
                EditorGUILayout.PropertyField(_enemyBuffType);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        EditorGUILayout.PropertyField(_buffInfo);
        
        serializedObject.ApplyModifiedProperties();
    }
}