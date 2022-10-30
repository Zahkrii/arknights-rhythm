using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System;

public class TouchInput : MonoBehaviour
{
    private List<float> tapPosXInfos = new List<float>(); //�洢���������Ϣ
    private List<float> touchPosXInfos = new List<float>(); //�洢����������Ϣ

    private List<GameObject> touchLinesList = new List<GameObject>(); //����ָʾ��

    [Header("����ָʾ�߿���")]
    [SerializeField] private Toggle lineToggle; //����ָʾ�߿���

    [Header("����ָʾ��")]
    [SerializeField] private GameObject touchLine; //����ָʾ��

    [Header("����ָʾ������ڵ�")]
    [SerializeField] private Transform touchLinesRoot; //��������ڵ�

    [Header("Debug")]
    [SerializeField] private TMP_Text touchText;

    [SerializeField] private TMP_Text tapText;
    [SerializeField] private TMP_Text lineText;

    private void Start()
    {
    }

    private void Update()
    {
        //�����һ֡������������
        ClearInputPosInfo();

        //��������
        GetTouchInputs();

#if UNITY_EDITOR
        //������
        GetMouseInputs();
#endif
        //�ж�����
        Padding();

        //���ɴ���ָʾ��
        GenerateTouchLine();
    }

    /// <summary>
    /// �ж��Ƿ����
    /// </summary>
    private void Padding()
    {
        for (int i = 0; i < DataManager.Instance.tapPaddingList.Count; i++)
        {
            for (int n = 0; n < tapPosXInfos.Count; n++)
            {
                if (DataManager.Instance.tapPaddingList[i].PaddingNote(tapPosXInfos[n]))
                {
                    tapPosXInfos.RemoveAt(n);
                }
            }
        }
        for (int i = 0; i < DataManager.Instance.dragPaddingList.Count; i++)
        {
            for (int n = 0; n < touchPosXInfos.Count; n++)
            {
                if (DataManager.Instance.dragPaddingList[i].PaddingNote(touchPosXInfos[n]))
                {
                    touchPosXInfos.RemoveAt(n);
                }
            }
        }
    }

    /// <summary>
    /// ��մ�����������
    /// </summary>
    private void ClearInputPosInfo()
    {
        tapPosXInfos.Clear();
        touchPosXInfos.Clear();
    }

    /// <summary>
    /// ��ȡ��������
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
    /// ��ȡ������
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
    /// ����ָʾ��
    /// </summary>
    private void GenerateTouchLine()
    {
        if (lineToggle.isOn)
        {
            //���ɴ���ָʾ��
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
            //���ٶ���ָʾ��
            for (int i = touchPosXInfos.Count; i < touchLinesList.Count;)
            {
                Destroy(touchLinesList[i]);
                touchLinesList.RemoveAt(i);
            }
            lineText.text = $"Line Count: {touchLinesList.Count}";
        }
    }
}