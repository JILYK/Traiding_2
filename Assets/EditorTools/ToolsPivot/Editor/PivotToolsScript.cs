using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PivotToolsScript : EditorWindow
{
    private readonly Stack<(RectTransform, Vector2)> undoStack = new();
    private List<GameObject> initiallyDisabledObjects = new();

    public void OnGUI()
    {
        GUILayout.Label("Pivot Maker", EditorStyles.boldLabel);

        if (GUILayout.Button("Adjust Anchors"))
        {
            EnableAllObjects();
            AdjustAnchors();
            RestoreInitialState();
        }
    }

    [MenuItem("Tools/Pivot Maker Tools")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PivotToolsScript));
    }

    private void EnableAllObjects()
    {
        initiallyDisabledObjects.Clear();
        var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (var rootObject in rootObjects)
        {
            EnableObjectAndChildren(rootObject);
        }
    }

    private void EnableObjectAndChildren(GameObject obj)
    {
        if (!obj.activeSelf)
        {
            initiallyDisabledObjects.Add(obj);
            obj.SetActive(true);
            Debug.Log($"Enabled: {obj.name}");
        }

        foreach (Transform child in obj.transform)
        {
            EnableObjectAndChildren(child.gameObject);
        }
    }

    private void RestoreInitialState()
    {
        foreach (var obj in initiallyDisabledObjects)
        {
            obj.SetActive(false);
            Debug.Log($"Restored to disabled: {obj.name}");
        }
    }

    private void AdjustAnchors()
    {
        var canvases = FindObjectsOfType<Canvas>();

        foreach (var canvas in canvases)
        {
            foreach (var o in canvas.GetComponentsInChildren<Transform>())
            {
                if (o == null)
                {
                    return;
                }

                var r = o.GetComponent<RectTransform>();

                if (r == null || r.parent == null)
                {
                    Debug.LogWarning($"Skipping object {o.name} as it does not have a RectTransform or parent.");
                    continue;
                }

                Undo.RecordObject(o, "SnapAnchors");
                AnchorRect(r);
            }
        }
    }

    static void AnchorRect(RectTransform r)
    {
        var p = r.parent.GetComponent<RectTransform>();

        if (p == null)
        {
            Debug.LogWarning($"Parent of {r.name} does not have a RectTransform.");
            return;
        }

        var offsetMin = r.offsetMin;
        var offsetMax = r.offsetMax;
        var _anchorMin = r.anchorMin;
        var _anchorMax = r.anchorMax;

        var parent_width = p.rect.width;
        var parent_height = p.rect.height;

        var anchorMin = new Vector2(_anchorMin.x + (offsetMin.x / parent_width),
            _anchorMin.y + (offsetMin.y / parent_height));
        var anchorMax = new Vector2(_anchorMax.x + (offsetMax.x / parent_width),
            _anchorMax.y + (offsetMax.y / parent_height));

        r.anchorMin = anchorMin;
        r.anchorMax = anchorMax;

        r.offsetMin = new Vector2(0, 0);
        r.offsetMax = new Vector2(0, 0);
        r.pivot = new Vector2(0.5f, 0.5f);
    }
}
