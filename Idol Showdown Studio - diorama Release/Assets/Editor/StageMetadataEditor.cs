using JetBrains.Annotations;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;

public class StageMetadataEditor : EditorWindow
{
    public static StageMetadata Metadata = new StageMetadata();
    public static Color color = Color.white;
    public static string rStageObject;

    public StageMetadataEditor()
    {
        minSize = new Vector2(450, 230);

        string metadata = "Assets/Editor/DefaultStageMeta.json";
        {
            if (File.Exists(metadata))
                Metadata = StageMetadata.Load(metadata);
        }
    }

    public static void ShowWindow(string stageObject)
    {
        GetWindow(typeof(StageMetadataEditor), true, "Stage Metadata Editor");
        rStageObject = stageObject;
    }

    private void OnGUI()
    {
        Metadata.Title = EditorGUILayout.TextField("Title", Metadata.Title);
        EditorGUILayout.HelpBox("The name of this stage.", MessageType.None);

        GUILayout.Space(10);

        Metadata.Version = EditorGUILayout.TextField("Version", Metadata.Version);
        EditorGUILayout.HelpBox("The version of this stage.", MessageType.None);

        GUILayout.Space(10);

        Metadata.Author = EditorGUILayout.TextField("Author", Metadata.Author);
        EditorGUILayout.HelpBox("The author of this stage.", MessageType.None);

        GUILayout.Space(10);

        Metadata.Description = EditorGUILayout.TextField("Description", Metadata.Description);
        EditorGUILayout.HelpBox("A description of this stage.", MessageType.None);

        GUILayout.Space(10);

        color = EditorGUILayout.ColorField("Song Color", color); //EditorGUILayout.TextField("Author", Metadata.Description);
        EditorGUILayout.HelpBox("The outside color of songs for this stage.", MessageType.None);

        GUILayout.Space(10);

        Metadata.StageLocation = EditorGUILayout.TextField("Location", Metadata.StageLocation);
        EditorGUILayout.HelpBox("Where this stage is located.", MessageType.None);

        GUILayout.Space(10);

        Metadata.AssetBundle = EditorGUILayout.TextField("File Name", Metadata.AssetBundle);
        EditorGUILayout.HelpBox("Name of the exported file.", MessageType.None);

        GUILayout.Space(10);

        Metadata.GamebananaID = EditorGUILayout.IntField("Gamebanana ID", Metadata.GamebananaID);
        EditorGUILayout.HelpBox("The GameBanana ID for this stage." /*Used for in-game updates. You can change this once the GameBanana page is created."*/, MessageType.None);

        GUILayout.Space(10);

        if (GUILayout.Button("Export"))
        {
            string metadata = "Assets/Export/mod.json";

            Metadata.Color = color.r.ToString() + ";" + color.g.ToString() + ";" + color.b.ToString();

            if (!string.IsNullOrEmpty(metadata))
            {
                StageMetadata.Save(Metadata, metadata);
            }

            ExportStage(rStageObject, Metadata.AssetBundle);
        }
    }

    public static void ExportStage(string stageObject, string abName)
    {
        string assetBundleDirectory = "Assets/Export";

        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        buildMap[0].assetBundleName = abName;

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
