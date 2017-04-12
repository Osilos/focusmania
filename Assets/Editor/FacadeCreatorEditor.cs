using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StageGenerator))]
public class FacadeCreatorEditor : Editor {

    public List<Texture2D> listTexture = new List<Texture2D>();
    public override void OnInspectorGUI()
    {
        StageGenerator stageGenerator = (StageGenerator)target;
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Facede"))
        {
            stageGenerator.Generate();
        }

        EditorGUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}