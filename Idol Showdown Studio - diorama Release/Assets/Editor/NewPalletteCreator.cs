using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NewPalletteCreator : EditorWindow
{
    public static List<Color> originalColors = new List<Color>();
    public static List<Color> newColors = new List<Color>();

    public static int curChar = 0;
    public static int outfit = 0;

    public static Texture2D displayChar = null;
    public static Texture2D displayCharRef = null;
    public static string displayCharName = string.Empty;

    public static bool updateColors = true;
    public static bool waitForClear = false;

    public static List<HeaderData> headers = new List<HeaderData>();

    private static Vector2 scrollPos = Vector2.zero;

    public NewPalletteCreator() 
    {
        minSize = new Vector2(900, 900);
        maxSize = new Vector2(900, 900);
    }

    [MenuItem("Idol Showdown/Palette Editor", false, -1)]
    public static void PalletteWindow()
    {
        originalColors.Clear();
        newColors.Clear();

        curChar = 0;
        outfit = 0;

        headers.Clear();

        displayChar = null;
        displayCharName = "";

        GetWindow(typeof(NewPalletteCreator), true, "Palette Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("box");

        string[] options = new string[]
        {
            "Aki",
            "Ayame",
            "Botan",
            "Coco",
            "Fubuki",
            "Ina",
            "Korone",
            "Ollie",
            "Pekora",
            "Sora",
            "Suisei",
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
            "Outfit 19",
            "Outfit 20"
        };
        outfit = EditorGUILayout.Popup(outfit, nums);

        EditorGUILayout.EndHorizontal();

        if (displayCharName != options[curChar]) updateColors = true;

        //if (displayCharName != options[curChar] || updateColors)
        //{

        if (!waitForClear) {
            EditorGUILayout.BeginHorizontal("box");

            EditorGUILayout.BeginVertical("box");

            if (updateColors)
            {
                displayCharName = options[curChar];
                displayChar = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/" + displayCharName + ".png", typeof(Texture2D));
                displayCharRef = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Editor/PaletteImages/" + displayCharName + "Ref.png", typeof(Texture2D));

                if (originalColors.Count > 0)
                {
                    originalColors.Clear();
                    newColors.Clear();

                    headers.Clear();
                }

                if (originalColors.Count == 0)
                {
                    string spriteData = "/Editor/SpriteData/" + displayCharName + ".txt";

                    string line;
                    int colorCount = 0;
                    int headerCount = -1;

                    using (StreamReader sr = new StreamReader(Application.dataPath + spriteData))
                    {
                        do
                        {
                            line = sr.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                if (line.StartsWith("header"))
                                {
                                    if (headerCount != -1)
                                    {
                                        HeaderData lhd = headers[headerCount];
                                        lhd.Count = colorCount;
                                        headers[headerCount] = lhd;
                                        colorCount = 0;
                                    }

                                    if (line.EndsWith("Break")) break;

                                    headerCount++;
                                    HeaderData hd = new HeaderData();

                                    hd.Header = line.Split(';')[1];

                                    headers.Add(hd);
                                }
                                else
                                {
                                    string[] lineData = line.Split(';');
                                    Color color = new Color(float.Parse(lineData[0]), float.Parse(lineData[1]), float.Parse(lineData[2]), float.Parse(lineData[3]));

                                    originalColors.Add(color);
                                    newColors.Add(color);

                                    colorCount++;
                                }
                            }
                        } while (!string.IsNullOrEmpty(line));
                    }
                }

                updateColors = false;
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
                headers.Clear();

                displayChar = null;
                displayCharName = string.Empty;

                waitForClear = true;

                string directory = @"Assets\Editor\PaletteImages";

                foreach (string meta in Directory.GetFiles(directory, "*.meta"))
                    File.Delete(meta);

                ResetAllMeta();

                AssetDatabase.Refresh();

                updateColors = true;
            }

            if (GUILayout.Button("Export"))
            {
                ExportPallete();

                PaletteMetadataEditor.ShowWindow(displayCharName, outfit);
            }

            EditorGUILayout.EndVertical();


            if (displayChar == null) return;

            EditorGUILayout.BeginVertical("box");

            int headerCounta = -1;
            int colorCounta = 0;
            int k = 0;

        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        style.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            for (int i = 0; i < newColors.Count + headers.Count; i++)
            {
                if (colorCounta == 0)
                {
                    if (headerCounta == headers.Count - 1) break;

                    headerCounta++;
                    EditorGUILayout.LabelField(headers[headerCounta].Header, style);

                    colorCounta = headers[headerCounta].Count;
                }
                else
                {

                    Color refCol = newColors[k];
                    newColors[k] = EditorGUILayout.ColorField(newColors[k]);

                    if (refCol != newColors[k])
                    {
                        for (int x = 0; x < displayChar.width; x++)
                        {
                            for (int y = 0; y < displayChar.height; y++)
                            {
                                if (displayCharRef.GetPixel(x, y) == originalColors[k])
                                {
                                    displayChar.SetPixel(x, y, newColors[k]);
                                }
                            }
                        }

                        displayChar.Apply();
                    }

                    colorCounta--;
                    k++;
                }
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
        for (int i = 0; i < 11; i++)
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
                    charName = "Pekora";
                    break;
                case 9:
                    charName = "Ollie";
                    break;
                case 10:
                    charName = "Ina";
                    break;
                case 11:
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

    public struct HeaderData
    {
        public string Header;
        public int Count;
    }
}
