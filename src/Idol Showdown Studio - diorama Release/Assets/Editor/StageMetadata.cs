using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class StageMetadata
{
    public string Title = "Untitled";
    public string Version = "1.0";
    public string Author = Environment.UserName;
    public string Type = "Stage";
    public string Description = "A Custom Stage";
    public string Color = "0;0;0";
    public string StageLocation = "Somewhere";
    public string AssetBundle = "stage";
    public int GamebananaID = -1;

    public static StageMetadata Load(string path)
        => JsonUtility.FromJson<StageMetadata>(File.ReadAllText(path));

    public static void Save(StageMetadata metadata, string path)
    {
        File.WriteAllText(path, JsonUtility.ToJson(metadata, true));
    }
}
