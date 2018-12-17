using UnityEditor;
using UnityEngine;

public class ShowMyGuiPenis : EditorWindow
{
    public string[] test123 = new[]
    {
        "1",
        "3",
        "5",
        "SingleTarget",
        "MultiTarget"
    };

    int index = 0;

    [MenuItem("Examples/Editor GUILayout Popup usage")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(ShowMyGuiPenis));
        window.Show();
    }

    void OnGUI()
    {
        index = EditorGUILayout.Popup(index, test123);
        if (GUILayout.Button("Update Data"))
        {
            BossManager_Level2.singleton.OnUpdateData();
        }

        if(GUILayout.Button("Add Skill to "))
        {
            BossPhaseManager.singleton.AddSkill(test123[index]);
        }
    }
}