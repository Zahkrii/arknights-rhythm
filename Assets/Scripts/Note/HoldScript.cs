using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScript : MonoBehaviour
{
    //��Ҫhold��ʱ��
    private float holdTime = 0;

    //��ʱ��
    private float Timer = -10f / (5 * 1);

    //�ж�������
    private bool add = true, remove = true, holding = false;

    private float holdingTime = 0;

    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void OnEnable()
    {
        //holdTime = DataManager.Instance.holdTime;
        //transform.GetChild(0).transform.localScale = new Vector3(0.8f, 0, 1);
        transform.position = new Vector3(0.8f, holdTime * 2.5f, 1);
        transform.Translate(0, holdTime * 2.5f, 0);
    }

    private void Update()
    {
        //��ʱ
        Timer += Time.deltaTime;
        //ʱ�䵽��ӵ��ж�����
        if (add && Timer > -0.17f)
        {
            ChartManager.Instance.holdHeadPaddingList.Add(this);
            add = false;
        }
        else if (remove && Timer > 0.17f)
        {
            ChartManager.Instance.holdHeadPaddingList.Remove(this);
            remove = false;
            Miss();
        }
        //�ƶ�
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //��Ч
        material.color = new Color(1, 1, 1, 0.3f);
        //�������
        //�Ƴ��ж�����
        ChartManager.Instance.holdHeadPaddingList.Remove(this);
        ChartManager.Instance.holdingPaddingList.Remove(this);
        Destroy(gameObject);
    }

    public bool HeadPadding(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * 1)
        {
            //��ͷ���ж������Ƴ�
            remove = false;
            ChartManager.Instance.holdHeadPaddingList.Remove(this);
            //����holding�ж�
            ChartManager.Instance.holdingPaddingList.Add(this);
            holding = true;
            StartCoroutine("HoldingTimer");
            //Destroy(gameObject);
            return true;
        }
        return false;
    }

    public bool HoldingPadding(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * 1)
        {
            holding = true;
            return true;
        }
        return false;
    }

    private IEnumerator HoldingTimer()
    {
        while (holding)
        {
            //����ʱ��ļ�ʱ��
            holdingTime += Time.deltaTime;
            holding = false;
            transform.position = new Vector3(1, (holdTime - holdingTime) * 7.5f, 1);
            transform.Translate(0, 7.5f * Time.deltaTime, 0);
            if (holdingTime > holdTime)
            {
                ChartManager.Instance.holdingPaddingList.Remove(this);
                //������Ч
                //�������
                Destroy(gameObject);
                break;
            }
            //ÿһִ֡��һ��
            yield return 0;
        }
        if (holdingTime < holdTime)
            Miss();
    }
}