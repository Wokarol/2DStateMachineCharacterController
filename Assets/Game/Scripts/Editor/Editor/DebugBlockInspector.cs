using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;

public class DebugBlockInspector : EditorWindow
{
    MonoBehaviour target;
    [SerializeField] int targetID;
    [SerializeField] List<DebugBlock> blocksWithNames = new List<DebugBlock>();

    [MenuItem("Tools/Debug Block Inspector")]
    static void Init() {
        DebugBlockInspector inspector = GetWindow<DebugBlockInspector>();
        inspector.name = "Debug Block Inspector";
        inspector.Show();
    }

    private void OnEnable() {
        FindTarget();
        EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
    }

    private void OnDisable() {
        EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
        StoreTarget();
    }

    private void EditorApplication_playModeStateChanged(PlayModeStateChange obj) {
        switch (obj) {
            case PlayModeStateChange.ExitingPlayMode:
            case PlayModeStateChange.ExitingEditMode:
                StoreTarget();
                break;
            case PlayModeStateChange.EnteredEditMode:
            case PlayModeStateChange.EnteredPlayMode:
                FindTarget();
                break;
            default:
                break;
        }
    }

    void StoreTarget() {
        if (target != null)
            targetID = target.GetInstanceID();
    }

    void FindTarget() {
        target = EditorUtility.InstanceIDToObject(targetID) as MonoBehaviour;
        FindBlocks(target);
    }

    private void Update() {
        Repaint();
    }

    private void OnGUI() {
        EditorGUI.BeginChangeCheck();
        target = (MonoBehaviour)EditorGUILayout.ObjectField("Target", target, typeof(MonoBehaviour), true);
        if (EditorGUI.EndChangeCheck()) {
            FindBlocks(target);
        }

        foreach (var blockWithName in blocksWithNames) {
            if (blockWithName != null)
                DrawDebugBlock(blockWithName);
        }
    }

    private void FindBlocks(MonoBehaviour target) {
        blocksWithNames.Clear();
        if (target != null) {
            Type type = target.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties) {
                if (property.PropertyType == typeof(DebugBlock)) {
                    DebugBlock block = (DebugBlock)property.GetValue(target as object);
                    if (block == null) EditorGUILayout.HelpBox($"{property.Name} is null!!!", MessageType.Error);
                    blocksWithNames.Add(block);
                }
            }
        }
    }

    private void DrawDebugBlock(DebugBlock block) {
        var text = "";
        text += block.Name;
        text += "\n============";
        var datas = block.Data.Values;
        foreach (var data in datas) {
            text += $"\n{(data.Name == "" ? $"----- ":$"{data.Name} :")}\t{data.Value}";
        }
        var style = GUI.skin.GetStyle("HelpBox");
        style.richText = true;
        EditorGUILayout.HelpBox(text, MessageType.None);
        style.richText = false;
    }
}
