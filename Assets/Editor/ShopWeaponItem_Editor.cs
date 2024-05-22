using UnityEditor;

[CustomEditor(typeof(ShopWeaponItem))]
public class ShopWeaponItem_Editor : Editor
{
    private SerializedProperty _slotData;
    private SerializedProperty _weaponObject;
    
    private void OnEnable()
    {
        _slotData = serializedObject.FindProperty("slotData");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_slotData);
        
        EditorGUILayout.Space(20);
        
        SerializedProperty weaponObjectProp = serializedObject.FindProperty("weaponObject");
        EditorGUILayout.PropertyField(weaponObjectProp.FindPropertyRelative("WeaponInfo"));
        
        serializedObject.ApplyModifiedProperties();
    }
}