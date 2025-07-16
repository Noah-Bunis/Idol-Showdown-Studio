using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class FolderPaletteAn : EditorWindow
{
    public static string cPath = "";
    private static List<Color> colors = new List<Color>();
    private static List<string> files = new List<string>();

    [MenuItem("Idol Showdown/Dev/Palette Analysis", false, 1)]
    public static void PalleteWindow()
    {
        GetWindow(typeof(FolderPaletteAn), true, "Pallete Editor");
    }

    public FolderPaletteAn()
    {
        minSize = new Vector2(400, 100);
        maxSize = new Vector2(400, 100);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        style.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
        
        EditorGUILayout.LabelField(cPath, style);

        if (GUILayout.Button("Select Path"))
        {
            cPath = EditorUtility.OpenFolderPanel("Load Image Folder", "", "");
        }

        if (GUILayout.Button("Get Palette Data"))
        {
            Analyze();
        }
    }

    private void Analyze()
    {
        colors.Clear();
        files.Clear();
        DirectoryInfo d = new DirectoryInfo(cPath);

        bool hasColor = false;
        foreach (var file in d.GetFiles("*.png"))
        {
            Texture2D tex = GenerateTexture(file.FullName);

            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    hasColor = false;
                    foreach (Color color in colors)
                    {
                        if (color == tex.GetPixel(x, y))
                        {
                            hasColor = true;
                            break;
                        }
                    }
                    if (!hasColor)
                    {
                        colors.Add(tex.GetPixel(x, y));
                        files.Add(file.Name);
                    }
                }
            }
        }

        List<string> inColour = new List<string>();
        for (int i = 0; i < colors.Count; i++)
        {
            string text = colors[i].r + ";" + colors[i].g + ";" + colors[i].b + ";" + colors[i].a + ";" + files[i];
            inColour.Add(text);
        }

        StreamWriter sw = new StreamWriter(Path.Combine(Application.dataPath, "Export", "PaletteAnalysis.txt"));
        for (int i = 0; i < inColour.Count; i++)
        {
            sw.WriteLine(inColour[i]);
        }
        sw.Close();

        AssetDatabase.Refresh();
    }

    private Texture2D GenerateTexture(string path)
    {
        Texture2D tex = new Texture2D(2, 2);
        if (!path.EndsWith(".png")) return null;
        byte[] bytes = File.ReadAllBytes(path);
        ImageConversion.LoadImage(tex, bytes);
        tex.Apply();
        return tex;
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
