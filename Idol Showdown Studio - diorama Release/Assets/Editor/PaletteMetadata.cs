using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class PaletteMetadata
{
    public string Title = "Untitled";
    public string Version = "1.0";
    public string Author = Environment.UserName;
    public string Type = "Character";
    public string Character = "Aki";
    public int Outfit = 0;
    public int GamebananaID = -1;

    public static PaletteMetadata Load (string path)
        => JsonUtility.FromJson<PaletteMetadata>(File.ReadAllText(path));

    public static void Save(PaletteMetadata metadata, string path)
    {
        File.WriteAllText(path, JsonUtility.ToJson(metadata, true));
    }
}
