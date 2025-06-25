using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.AccessControl;
using System.IO;

public class PalleteCreator : EditorWindow
{
    public static List<Color> originalColors = new List<Color>();
    public static List<Color> newColors = new List<Color>();

    public static int curChar = 0;
    public static int outfit = 0;

    public static Texture2D displayChar = null;
    public static Texture2D displayCharRef = null;
    public static string displayCharName = string.Empty;

    public static bool waitForClear = false;

    public PalleteCreator()
    {
        minSize = new Vector2(450, 815);
    }

    //[MenuItem("Idol Showdown/Pallete Editor New", false, 9)]
    public static void PalleteWindow()
    {
        originalColors.Clear();
        newColors.Clear();

        curChar = 0;
        outfit = 0;

        displayChar = null;
        displayCharName = "";

        GetWindow(typeof(PalleteCreator), true, "Pallete Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("box");
        string[] options = new string[]
        {
            "Aki",
            "Ayame",
            "Coco",
            "Fubuki",
            "Korone",
            "Sora",
            "Suisei",
            "Botan",
            "Ina",
            "Koyori"
        };

        curChar = EditorGUILayout.Popup(curChar, options);

        string[] nums = new string[]
        {
            "Outfit 1",
            "Outfit 2",
            "Outfit 3",
            "Outfit 4",
            "Outfit 5",
            "Outfit 6",
            "Outfit 7",
            "Outfit 8",
            "Outfit 9",
            "Outfit 10",
            "Outfit 11",
            "Outfit 12",
            "Outfit 13",
            "Outfit 14",
            "Outfit 15",
            "Outfit 16",
            "Outfit 17",
            "Outfit 18",
            "Outfit 19"
        };

        outfit = EditorGUILayout.Popup(outfit, nums);

        EditorGUILayout.EndHorizontal();

        if (!waitForClear)
        {
            EditorGUILayout.BeginHorizontal("box");

            switch (curChar)
            {
                case 0:
                    if (displayCharName != "Aki")
                    {
                        displayCharName = "Aki";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Aki.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/AkiRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }


                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;

                case 1:
                    if (displayCharName != "Ayame")
                    {
                        displayCharName = "Ayame";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Ayame.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/AyameRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }


                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;

                case 2:
                    if (displayCharName != "Coco")
                    {
                        displayCharName = "Coco";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Coco.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/CocoRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }


                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;

                case 3:
                    if (displayCharName != "Fubuki")
                    {
                        displayCharName = "Fubuki";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Fubuki.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/FubukiRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }
                    

                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;

                case 4:
                    if (displayCharName != "Korone")
                    {
                        displayCharName = "Korone";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Korone.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/KoroneRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors = originalColors;
                                }
                            }
                        }
                    }
                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;

                case 5:
                    if (displayCharName != "Sora")
                    {
                        displayCharName = "Sora";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Sora.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/SoraRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }
                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;

                case 6:
                    if (displayCharName != "Suisei")
                    {
                        displayCharName = "Suisei";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Suisei.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/SuiseiRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }
                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;

                case 7:
                    if (displayCharName != "Botan")
                    {
                        displayCharName = "Botan";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Botan.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/BotanRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }

                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;
                case 8:
                    if (displayCharName != "Ina")
                    {
                        displayCharName = "Ina";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Ina.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/InaRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }
                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;
                    case 9:
                    if (displayCharName != "Koyori")
                    {
                        displayCharName = "Koyori";

                        displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/Ina.png", typeof(Texture2D));
                        displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/InaRef.png", typeof(Texture2D));

                        if (originalColors.Count > 0)
                        {
                            originalColors.Clear();
                            newColors.Clear();
                        }

                        originalColors.Clear();
                        newColors.Clear();

                        if (originalColors.Count == 0)
                        {
                            Color[] fColors = displayCharRef.GetPixels(0, 0, displayCharRef.width, displayCharRef.height);

                            for (int i = 0; i < fColors.Length; i++)
                            {
                                bool hasCol = false;
                                for (int j = 0; j < originalColors.Count; j++)
                                {
                                    if (fColors[i] == originalColors[j])
                                    {
                                        hasCol = true;
                                        break;
                                    }
                                }

                                if (!hasCol)
                                {
                                    originalColors.Add(fColors[i]);
                                    newColors.Add(fColors[i]);
                                }
                            }
                        }
                    }

                    GUILayout.Box(displayChar, GUILayout.MaxWidth(150), GUILayout.MaxHeight(324));
                    break;
            }


            if (displayChar == null) return;

            EditorGUILayout.BeginVertical("box");

            int cCol = 0;

            for (int i = 0; i < newColors.Count; i++)
            {
                Color refCol = newColors[i];
                newColors[i] = EditorGUILayout.ColorField("Color " + cCol, newColors[i]);

                if (refCol != newColors[i])
                {
                    for (int x = 0; x < displayChar.width; x++)
                    {
                        for (int y = 0; y < displayChar.height; y++)
                        {
                            if (displayCharRef.GetPixel(x, y) == originalColors[i])
                            {
                                displayChar.SetPixel(x, y, newColors[i]);
                            }
                        }
                    }

                    displayChar.Apply();
                }

                cCol++;
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        waitForClear = false;

        if (GUILayout.Button("Reset Colors"))
        {
            originalColors.Clear();
            newColors.Clear();

            displayChar = null;
            displayCharName = string.Empty;

            outfit = 0;

            waitForClear = true;

            string directory = @"Assets\Editor\PaletteImages";

            foreach (string meta in Directory.GetFiles(directory, "*.meta"))
                File.Delete(meta);

            ResetAllMeta();

            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("Export"))
        {
            ExportPallete();
        }
    }

    private static void ExportPallete()
    {
        List<string> inColour = new List<string>();
        List<string> outColour = new List<string>();

        for (int i = 0; i < originalColors.Count; i++)
        {
            string text = originalColors[i].r + ";" + originalColors[i].g + ";" + originalColors[i].b + ";" + originalColors[i].a + ";";
            inColour.Add(text);
        }

        for (int i = 0; i < newColors.Count; i++)
        {
            string text = newColors[i].r + ";" + newColors[i].g + ";" + newColors[i].b + ";" + newColors[i].a;
            outColour.Add(text);
        }

        StreamWriter sw = new StreamWriter(Path.Combine(Application.dataPath, "Export", displayCharName + outfit + "in.txt"));

        for (int i = 0; i < 48; i++)
        {
            if (i >= inColour.Count)
                sw.WriteLine("0;0;0;0");
            else
                sw.WriteLine(inColour[i]);
        }

        sw.Close();

        sw = new StreamWriter(Path.Combine(Application.dataPath, "Export", displayCharName + outfit + "out.txt"));

        for (int i = 0; i < 48; i++)
        {
            if (i >= outColour.Count)
                sw.WriteLine("0;0;0;0");
            else
                sw.WriteLine(outColour[i]);
        }

        sw.Close();

        AssetDatabase.Refresh();
    }

    private static void ResetAllMeta()
    {
        for (int i = 0; i < 8; i++)
        {
            string charName = "";
            switch (i)
            {
                case 0:
                    charName = "Aki";
                    break;
                case 1:
                    charName = "Ayame";
                    break;
                case 2:
                    charName = "Coco";
                    break;
                case 3:
                    charName = "Fubuki";
                    break;
                case 4:
                    charName = "Korone";
                    break;
                case 5:
                    charName = "Sora";
                    break;
                case 6:
                    charName = "Suisei";
                    break;
                case 7:
                    charName = "Botan";
                    break;
                case 8:
                    charName = "Ina";
                    break;
                case 9:
                    charName = "Koyori";
                    break;
            }

            var tImporter = AssetImporter.GetAtPath("Assets/Editor/PaletteImages/" + charName + ".png") as TextureImporter;

            if (tImporter != null)
            {
                tImporter.textureType = TextureImporterType.Default;
                tImporter.isReadable = true;
                tImporter.textureCompression = 0;

                AssetDatabase.ImportAsset("Assets/Editor/PaletteImages/" + charName + ".png");
                AssetDatabase.Refresh();
            }
            tImporter = AssetImporter.GetAtPath("Assets/Editor/PaletteImages/" + charName + "Ref.png") as TextureImporter;

            if (tImporter != null)
            {
                tImporter.textureType = TextureImporterType.Default;
                tImporter.isReadable = true;
                tImporter.textureCompression = 0;

                AssetDatabase.ImportAsset("Assets/Editor/PaletteImages/" + charName + "Ref.png");
                AssetDatabase.Refresh();
            }
        }
    }
}
