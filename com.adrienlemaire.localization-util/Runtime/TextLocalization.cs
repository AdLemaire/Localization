using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using static Com.AdrienLemaire.LocalizationUtil.Localization;

namespace Com.AdrienLemaire.LocalizationUtil
{
    [Serializable]
    [RequireComponent(typeof(TextMeshProUGUI))]
    [DisallowMultipleComponent]
    public class TextLocalization : MonoBehaviour
    {
        public const string LOCALIZATION_PATH = "Packages/com.adrienlemaire.localization-util/Runtime/LocalizationSetting.asset";

        public static Localization localization = default;

        public static List<string> myList;

        [Header("Reference")]
        [ListToPopup(typeof(TextLocalization), "myList")]
        public string textReference = "exemple";

        private TextMeshProUGUI text = default;

        public LocalizationRefType textType = default;

        public static Localization GetLocalization
        {
            get => (Localization)AssetDatabase.LoadAssetAtPath(LOCALIZATION_PATH, typeof(Localization));
        }

        private void OnValidate()
        {
            if (!localization) localization = GetLocalization;

            myList = localization.allKeys[(int)textType].list;

            SetText(localization.currentLanguage);

            EditorUtility.SetDirty(localization);
        }

        public void SetText(int textIndex)
        {
            int referenceIndex = localization.allKeys[(int)textType].list.IndexOf(textReference);

            if (referenceIndex == -1) return;

            if (!text)
                text = GetComponent<TextMeshProUGUI>();

            text.text = localization.contentList[(int)textType].list[referenceIndex].list[textIndex];
        }
    }
}
