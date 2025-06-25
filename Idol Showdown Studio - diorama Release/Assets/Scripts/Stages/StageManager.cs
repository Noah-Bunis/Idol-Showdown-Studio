using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Transform cam;
    public Transform handles;

    public GameObject selectedObject;

    public bool previewMode;
    public float camX;
    public float camY;

    public Toggle previewToggle;
    public Slider xSlider;
    public Slider ySlider;

    public GameObject spriteList;

    public GameObject spriteListObject;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;

        TogglePreview();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePreview()
    {
        previewMode = previewToggle.isOn;

        if (previewMode)
        {
            xSlider.gameObject.SetActive(true);
            ySlider.gameObject.SetActive(true);
            spriteList.SetActive(false);
        }
        else
        {
            xSlider.value = 0;
            ySlider.value = 0;

            Vector3 camPos = cam.position;
            camPos.x = 0;
            camPos.y = 0;

            cam.position = camPos;

            xSlider.gameObject.SetActive(false);
            ySlider.gameObject.SetActive(false);
            spriteList.SetActive(true);
        }
    }

    public void UpdatePreview()
    {
        Vector3 camPos = cam.position;
        camPos.x = xSlider.value;
        camPos.y = ySlider.value;

        cam.position = camPos;
    }

    public void AddSprite()
    {
        //string path = EditorUtility.OpenFilePanel("Select Sprite", Application.dataPath, "png");

        //if (path.StartsWith(Application.dataPath))
        //{
        //    GameObject newObj = Instantiate(spriteListObject, spriteList.transform.Find("Scroll View/Viewport/Content"));

        //    Image image = newObj.transform.Find("Button/Image").GetComponent<Image>();
        //    image.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(path.Replace(Application.dataPath, "Assets/"), typeof(Sprite));
        //}
    }
}
