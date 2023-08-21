

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(GridStartLayout))]
public class ArrayPropertyDrawer : PropertyDrawer
{
    private readonly float cellSize = 50;
    private readonly float soHandlePixelSize = 15;
 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        SerializedProperty data = property.FindPropertyRelative("BlockDatas").FindPropertyRelative("Blocks");

        float cellWidth = cellSize * 1.3f;
        float x = position.x;
        float y = position.y + cellSize;

        int width = property.FindPropertyRelative("Width").intValue;
        int height = property.FindPropertyRelative("Height").intValue;

        for (int i = 0; i < width * height; i++)
        {
            Rect newPosition = new Rect(x, y, cellWidth, cellSize);
            EditorGUI.PropertyField(newPosition, data.GetArrayElementAtIndex(i), GUIContent.none);

            BaseBlockEntityTypeDefinition typeDef = (data.GetArrayElementAtIndex(i).objectReferenceValue as BaseBlockEntityTypeDefinition);
            newPosition.width -= soHandlePixelSize;
            if (typeDef != null) EditorGUI.DrawTextureTransparent(newPosition, typeDef.DefaultEntitySprite.texture);
            newPosition.width += soHandlePixelSize;

            x += cellWidth;

            if ((i + 1) % width == 0)
            {
                x = position.x;
                y += cellSize;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int height = property.FindPropertyRelative("Height").intValue;
        return cellSize * (height + 2);
    }
}