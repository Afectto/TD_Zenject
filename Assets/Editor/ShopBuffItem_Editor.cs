using UnityEditor;

[CustomEditor(typeof(ShopBuffItem))]
public class ShopBuffItem_Editor : Editor
{
    private SerializedProperty _slotData;
    private SerializedProperty _buffObject;
    

    private void OnEnable()
    {
        _slotData = serializedObject.FindProperty("slotData");
    }

    public override void OnInspectorGUI()
    {
        var controller = (ShopBuffItem) target;
        
        serializedObject.Update();
        EditorGUILayout.PropertyField(_slotData);

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Buff Settings", EditorStyles.boldLabel);
        
        SerializedProperty buffObjectProp = serializedObject.FindProperty("buffObject");
        EditorGUILayout.PropertyField(buffObjectProp.FindPropertyRelative("buffInfo"));
        EditorGUILayout.PropertyField(buffObjectProp.FindPropertyRelative("buffType"));

        switch (controller.buffObject.buffType)
        {
            case BuffType.Tower:
                EditorGUILayout.PropertyField(buffObjectProp.FindPropertyRelative("towerBuffType"));
                break;            
            case BuffType.TowerWeapon:
                EditorGUILayout.PropertyField(buffObjectProp.FindPropertyRelative("towerWeaponBuffType"));
                EditorGUILayout.PropertyField(buffObjectProp.FindPropertyRelative("weaponDamageType"));
                break;            
            case BuffType.Enemy:
                EditorGUILayout.PropertyField(buffObjectProp.FindPropertyRelative("enemyBuffType"));
                break;
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}