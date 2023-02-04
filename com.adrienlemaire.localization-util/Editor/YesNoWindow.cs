using UnityEditor;
using UnityEngine;

namespace Com.AdrienLemaire.LocalizationUtil
{

    public delegate void LastCautionWindowEventHandler(YesNoWindow sender);
    public class YesNoWindow : EditorWindow
    {
        private static YesNoWindow window = default;

        private static string cautionText;
        private static Rect windowRect;

        public static event LastCautionWindowEventHandler OnAnswerYes;
        public static event LastCautionWindowEventHandler OnAnswerNo;

        public bool choice;

        public static void DrawWindow(string _actionVerb)
        {
            cautionText = _actionVerb;

            if (!window) window = (YesNoWindow)GetWindow(typeof(YesNoWindow));
            window.minSize = new Vector2(200, 200);
            window.titleContent = new GUIContent("Language Editor");
            window.Show();

            windowRect = new Rect(0, 0, window.position.width, window.position.height);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(windowRect);
            {
                GUILayout.BeginVertical();
                {
                    GUIContent contentText = new GUIContent();
                    contentText.text = cautionText;

                    GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
                    labelStyle.alignment = TextAnchor.UpperCenter;

                    GUILayout.Label(
                        cautionText,
                        labelStyle,
                        GUILayout.Height(100),
                        GUILayout.Width(windowRect.width));

                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Yes"))
                        {
                            OnAnswerYes?.Invoke(this);
                            Close();
                        }
                        else if (GUILayout.Button("No"))
                        {
                            OnAnswerNo?.Invoke(this);
                            Close();
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }
}
