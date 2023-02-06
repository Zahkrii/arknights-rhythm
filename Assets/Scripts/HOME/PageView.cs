using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// ����ҳ��Ч��
/// </summary>
public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private ScrollRect rect;//ScrollRect���
    private float targethorizontal = 0;
    private List<float> posList = new List<float>();//��ͼƬ��λ��(0, 0.333, 0.666, 1) 
    private bool isDrag = true;//�Ƿ��϶�
    private float startTime = 0;
    private float startDragHorizontal;
    private int curIndex = 0;

    public float speed = 3;//�����ٶ�  


    void Start()
    {
        rect = GetComponent<ScrollRect>();//��ʼ��ScrollRect���
        float horizontalLength = rect.content.rect.width - GetComponent<RectTransform>().rect.width;//ˮƽ���ȵ���content�Ŀ�ȼ�ȥ��ǰUI�Ŀ��
        var _rectWidth = GetComponent<RectTransform>().rect.width;//_rectWidth�ǵ�ǰUI�Ŀ��
        for (int i = 0; i < rect.content.transform.childCount; i++)//���content�µ�����ͼƬ��λ����Ϣ��posList��ȥ
        {
            posList.Add(_rectWidth * i / horizontalLength);//��ͼƬλ�� 
        }
        curIndex = 0;//��ǰҳ��������0
    }

    void Update()
    {
        if (!isDrag)
        {
            startTime += Time.deltaTime;
            float t = startTime * speed;
            //���ٻ���Ч��
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, t);//��ֵ����
            //�������ٻ���Ч��
            //rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, Time.deltaTime * speed);
            Debug.Log("�϶�����");

            //������Ӷ����任���������ڱ�Ľű�����λ�ñ任��
        }
    }

    public void OnBeginDrag(PointerEventData eventData)//��ʼ�϶�
    {
        isDrag = true;
        startDragHorizontal = rect.horizontalNormalizedPosition;  //horizontalNormalizedPosition���������scrollRect�����ڼ�仯��x����ֵ���ڣ�0�� 1��֮��
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        float posX = rect.horizontalNormalizedPosition;
        int index = 0;
        float offset = Mathf.Abs(posList[index] - posX);  //���㵱ǰλ�����һҳ��ƫ��������ʼ��offect
        for (int i = 1; i < posList.Count; i++)
        {    //����ҳǩ��ѡȡ��ǰxλ�ú�ÿҳƫ������С���Ǹ�ҳ��
            float temp = Mathf.Abs(posList[i] - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        curIndex = index;
        targethorizontal = posList[curIndex]; //���õ�ǰ���꣬���º������в�ֵ  
        isDrag = false;
        startTime = 0;
    }
}

//�ƺ��Ǹ���content���������λ�úͿ�Ƚ�����scrollview���ݻ��ֳɶ���������û�϶���һ���̶������������ĳ�������ڣ��϶�������ֵisDrug=trueֱ�Ӳ�ֵ���������л������ڵ�����
//GetComponent<RectTransform>().rect.width���ڻ�ȡ����ǰUI�Ŀ��
//ע����isDrug=falseʱִ���л�