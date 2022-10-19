using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchInput : MonoBehaviour
{
    private List<float> tapPosXInfos = new List<float>(); //存储点击手势信息
    private List<float> touchPosXInfos = new List<float>(); //存储触摸手势信息

    private List<GameObject> touchLinesList = new List<GameObject>(); //触摸指示线

    [Header("触摸指示线开关")]
    [SerializeField] private Toggle lineToggle; //触摸指示线开关

    [Header("触摸指示线")]
    [SerializeField] private GameObject touchLine; //触摸指示线

    [Header("触摸指示线物体节点")]
    [SerializeField] private Transform touchLinesRoot; //场景物体节点

    [Header("Debug")]
    [SerializeField] private TMP_Text touchText;

    [SerializeField] private TMP_Text tapText;
    [SerializeField] private TMP_Text lineText;

    private void Start()
    {
    }

    private void Update()
    {
        //清空上一帧触摸输入数据
        ClearInputPosInfo();

        //触摸操作
        GetTouchInputs();

#if UNITY_EDITOR
        //鼠标操作
        GetMouseInputs();
#endif

        //生成触摸指示线
        GenerateTouchLine();
    }

    /// <summary>
    /// 清空触摸输入数据
    /// </summary>
    private void ClearInputPosInfo()
    {
        tapPosXInfos.Clear();
        touchPosXInfos.Clear();
    }

    /// <summary>
    /// 获取触摸操作
    /// </summary>
    private void GetTouchInputs()
    {
        foreach (Touch finger in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(finger.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                touchPosXInfos.Add(hit.point.x);
                if (finger.phase == TouchPhase.Began)
                {
                    tapPosXInfos.Add(hit.point.x);
                }
            }
        }
        touchText.text = $"Touch Count: {touchPosXInfos.Count}";
        tapText.text = $"Tap Count: {tapPosXInfos.Count}";
    }

#if UNITY_EDITOR

    /// <summary>
    /// 获取鼠标操作
    /// </summary>
    private void GetMouseInputs()
    {
        Ray rayM = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitM;
        if (Physics.Raycast(rayM, out hitM))
        {
            if (Input.GetMouseButtonDown(0))
            {
                tapPosXInfos.Add(hitM.point.x);
            }
            if (Input.GetMouseButton(0))
            {
                touchPosXInfos.Add(hitM.point.x);
            }
        }
    }

#endif

    /// <summary>
    /// 生成指示线
    /// </summary>
    private void GenerateTouchLine()
    {
        if (lineToggle.isOn)
        {
            //生成触摸指示线
            for (int i = 0; i < touchPosXInfos.Count; i++)
            {
                if (i == touchLinesList.Count)
                {
                    GameObject go = Instantiate(touchLine, touchLinesRoot);
                    touchLinesList.Add(go);
                }
                Vector3 pos = touchLinesList[i].transform.position;
                pos.x = touchPosXInfos[i];
                touchLinesList[i].transform.position = pos;
            }
            lineText.text = $"Line Count: {touchLinesList.Count}";
            //销毁多余指示线
            for (int i = touchPosXInfos.Count; i < touchLinesList.Count;)
            {
                Destroy(touchLinesList[i]);
                touchLinesList.RemoveAt(i);
            }
            lineText.text = $"Line Count: {touchLinesList.Count}";
        }
    }
}