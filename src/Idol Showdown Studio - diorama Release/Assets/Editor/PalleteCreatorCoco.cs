using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.AccessControl;
using System.IO;

public class PalleteCreatorCoco : EditorWindow
{
    public static List<Color> originalColors = new List<Color>();
    public static List<Color> newColors = new List<Color>();

    public static int outfit = 0;

    public static Texture2D displayChar = null;
    public static Texture2D displayCharRef = null;
    public static string displayCharName = string.Empty;

    public static bool waitForClear = false;

    private static Vector2 scrollPos = Vector2.zero;

    public PalleteCreatorCoco()
    {
        minSize = new Vector2(450, 450);
        maxSize = new Vector2(450, 450);
    }

    [MenuItem("Idol Showdown/Palette Editor (Old)/Coco", false, 3)]
    public static void PalleteWindow()
    {
        originalColors.Clear();
        newColors.Clear();

        outfit = 0;

        displayChar = null;
        displayCharName = "";

        GetWindow(typeof(PalleteCreatorCoco), true, "Pallete Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("box");

        string[] nums = new string[]
        {
            "Outfit 1",
            "Outfit 2",
            "Outfit 3",
            "Outfit 4",
            "Outfit 5",
            "Outfit 6",
            "Outfit 7"
        };

        outfit = EditorGUILayout.Popup(outfit, nums);

        EditorGUILayout.EndHorizontal();

        if (!waitForClear)
        {
            EditorGUILayout.BeginHorizontal("box");

            EditorGUILayout.BeginVertical("box");
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

            if (GUILayout.Button("Load Current Outfit Colors"))
            {
                string directory = @"Assets\Export\" + displayCharName + outfit + "out.txt";

                List<string> outColours = new List<string>();

                if (File.Exists(directory))
                {
                    StreamReader r = new StreamReader(directory);

                    string line;
                    using (r)
                    {
                        do
                        {
                            line = r.ReadLine();
                            if (line != null)
                            {
                                outColours.Add(line);
                            }
                        } while (line != null);

                        r.Close();
                    }


                    for (int i = 0; i < newColors.Count; i++)
                    {
                        string[] lineData = outColours[i].Split(';');

                        newColors[i] = new Color(float.Parse(lineData[0]), float.Parse(lineData[1]), float.Parse(lineData[2]), float.Parse(lineData[3]));

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
                    }

                    displayChar.Apply();

                    waitForClear = true;

                    return;
                }
            }

            if (GUILayout.Button("Reset Colors"))
            {
                originalColors.Clear();
                newColors.Clear();

                displayChar = null;
                displayCharName = string.Empty;

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

                PaletteMetadataEditor.ShowWindow(displayCharName, outfit);
            }

            EditorGUILayout.EndVertical();


            if (displayChar == null) return;

            EditorGUILayout.BeginVertical("box");

            int cCol = 0;

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
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
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        waitForClear = false;
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
                    charName = "Coco";
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
                    charName = "Pekora";
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
