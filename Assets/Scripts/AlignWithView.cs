using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlignWithView : MonoBehaviour
{
    private void Start()
    {
        Camera cam = Camera.main;
        float distance = Vector3.Distance(this.transform.position, cam.transform.position);
        float frustumHeight = Mathf.Ceil(2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * 1.1f);
        float frustumWidth = Mathf.Ceil(frustumHeight * cam.aspect * 1.1f);

        this.transform.localScale = new Vector3(frustumWidth, frustumHeight, 1.0f);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}