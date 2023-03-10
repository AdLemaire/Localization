using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Com.AdrienLemaire.LocalizationUtil {
    public class ListToPopupAttribute : PropertyAttribute
    {
        public Type type;
        public string name;
        public List<string> list;

        public ListToPopupAttribute(Type type, string name)
        {
            this.type = type;
            this.name = name;
        }
    }

    [CustomPropertyDrawer(typeof(ListToPopupAttribute))]
    public class ListToPopupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ListToPopupAttribute _attribute = attribute as ListToPopupAttribute;
            List<string> stringList = null;

            if (_attribute.type.GetField(_attribute.name) != null)
                stringList = _attribute.type.GetField(_attribute.name).GetValue(_attribute.type) as List<string>;

            if (stringList != null && stringList.Count != 0)
            {
                int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
                selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
                property.stringValue = stringList[selectedIndex];
            }
            else EditorGUI.PropertyField(position, property, label);
        }
    }
}
