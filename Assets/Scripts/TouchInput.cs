using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchInput : MonoBehaviour
{
    private List<float> tapPosXInfos = new List<float>(); //�洢���������Ϣ
    private List<float> touchPosXInfos = new List<float>(); //�洢����������Ϣ

    private List<float> lastTouchPosXInfos = new List<float>(); //�洢����������Ϣ

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

        //������
        GetMouseInputs();

        //���ɴ���ָʾ��
        GenerateTouchLine();
    }

    /// <summary>
    /// ��մ�����������
    /// </summary>
    private void ClearInputPosInfo()
    {
        lastTouchPosXInfos.Clear();
        foreach (float a in touchPosXInfos)
        {
            lastTouchPosXInfos.Add(a);
        }
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
            for (int i = touchPosXInfos.Count - 1; i < touchLinesList.Count; i++)
            {
                Destroy(touchLinesList[i]);
                touchLinesList.RemoveAt(i);
            }
            lineText.text = $"Line Count: {touchLinesList.Count}";
        }
    }
}