using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScript : MonoBehaviour
{
    //需要hold的时间
    private float holdTime = 0;

    //计时器
    private float Timer = -10f / (5 * 1);

    //判定序列锁
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
        //计时
        Timer += Time.deltaTime;
        //时间到添加到判定序列
        if (add && Timer > -0.17f)
        {
            DataManager.Instance.holdHeadPaddingList.Add(this);
            add = false;
        }
        else if (remove && Timer > 0.17f)
        {
            DataManager.Instance.holdHeadPaddingList.Remove(this);
            remove = false;
            Miss();
        }
        //移动
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //特效
        material.color = new Color(1, 1, 1, 0.3f);
        //计算分数
        //移出判定序列
        DataManager.Instance.holdHeadPaddingList.Remove(this);
        DataManager.Instance.holdingPaddingList.Remove(this);
        Destroy(gameObject);
    }

    public bool HeadPadding(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * 1)
        {
            //从头部判定序列移除
            remove = false;
            DataManager.Instance.holdHeadPaddingList.Remove(this);
            //加入holding判定
            DataManager.Instance.holdingPaddingList.Add(this);
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
            //按下时间的计时器
            holdingTime += Time.deltaTime;
            holding = false;
            transform.position = new Vector3(1, (holdTime - holdingTime) * 7.5f, 1);
            transform.Translate(0, 7.5f * Time.deltaTime, 0);
            if (holdingTime > holdTime)
            {
                DataManager.Instance.holdingPaddingList.Remove(this);
                //生成特效
                //计算分数
                Destroy(gameObject);
                break;
            }
            //每一帧执行一次
            yield return 0;
        }
        if (holdingTime < holdTime)
            Miss();
    }
}