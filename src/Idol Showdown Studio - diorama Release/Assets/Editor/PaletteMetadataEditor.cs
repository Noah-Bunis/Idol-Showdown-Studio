using JetBrains.Annotations;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;

public class PaletteMetadataEditor : EditorWindow
{
    public static PaletteMetadata Metadata = new PaletteMetadata();

    public PaletteMetadataEditor()
    {
        minSize = new Vector2(450, 230);

        string metadata = "Assets/Editor/DefaultPaletteMeta.json";
        {
            if (File.Exists(metadata))
                Metadata = PaletteMetadata.Load(metadata);
        }
    }

    public static void ShowWindow(string character, int outfit)
    {
        GetWindow(typeof(PaletteMetadataEditor), true, "Metadata Editor: " + character + " - Outfit: " + (outfit + 1));

        Metadata.Character = character;
        Metadata.Outfit = outfit;
    }

    private void OnGUI()
    {
        Metadata.Title = EditorGUILayout.TextField("Title", Metadata.Title);
        EditorGUILayout.HelpBox("The name of this palette.", MessageType.None);

        GUILayout.Space(10);

        Metadata.Version = EditorGUILayout.TextField("Version", Metadata.Version);
        EditorGUILayout.HelpBox("The version of this palette.", MessageType.None);

        GUILayout.Space(10);

        Metadata.Author = EditorGUILayout.TextField("Author", Metadata.Author);
        EditorGUILayout.HelpBox("The author of this palette.", MessageType.None);

        GUILayout.Space(10);

        Metadata.GamebananaID = EditorGUILayout.IntField("Gamebanana ID", Metadata.GamebananaID);
        EditorGUILayout.HelpBox("The GameBanana ID for this palette." /*Used for in-game updates. You can change this once the GameBanana page is created."*/, MessageType.None);

        GUILayout.Space(10);

        if (GUILayout.Button("Export"))
        {
            string metadata = "Assets/Export/mod.json";

            if (!string.IsNullOrEmpty(metadata))
            {
                PaletteMetadata.Save(Metadata, metadata);
            }

            AssetDatabase.Refresh();
        }
    }
}
