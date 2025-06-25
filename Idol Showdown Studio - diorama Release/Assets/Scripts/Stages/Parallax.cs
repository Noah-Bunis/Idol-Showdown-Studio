using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxEffect;

    [Range(0f, 1f)]
    public float parallaxEffectY;

    private Vector3 startPos;

    private StageManager sm;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        sm = FindObjectOfType<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraOffset = sm.cam.position;

        transform.position = new Vector3(this.startPos.x + cameraOffset.x * this.parallaxEffect, this.startPos.y + cameraOffset.y * this.parallaxEffectY, this.startPos.z);
    }
}
