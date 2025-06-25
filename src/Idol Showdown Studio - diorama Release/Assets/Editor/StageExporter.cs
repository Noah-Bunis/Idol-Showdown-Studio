using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.AccessControl;
using System.IO;

public class StageExporter : EditorWindow
{
    public static GameObject objectName;

    public StageExporter()
    {
        minSize = new Vector2(450, 50);
    }

    [MenuItem("Idol Showdown/Export Stage", false, 0)]
    public static void ExportStageMenu()
    {
        GetWindow(typeof(StageExporter), true, "Stage Exporter");
    }
    private void OnGUI()
    {
        objectName = (GameObject)EditorGUILayout.ObjectField("Stage Object", objectName, typeof(GameObject), false);

        if (GUILayout.Button("Export"))
        {
            StageMetadataEditor.ShowWindow(AssetDatabase.GetAssetPath(objectName));
        }
    }

    public static void ExportStage(string stageObject)
    {
        string assetBundleDirectory = "Assets/Export";

        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        buildMap[0].assetBundleName = "stage";

        string[] stageo = new string[2];
        stageo[0] = stageObject;

        buildMap[0].assetNames = stageo;

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        foreach (string manifest in Directory.GetFiles(assetBundleDirectory, "*.manifest"))
            File.Delete(manifest);

        File.Delete(Path.Combine(assetBundleDirectory, "Export"));

        AssetDatabase.Refresh();
    }
}
