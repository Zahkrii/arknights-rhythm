using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScript : MonoBehaviour
{
    //需要hold的时间
    public float holdTime = 0;

    //计时器
    private float Timer = -10f / 5;

    //判定序列锁
    private bool add = true, remove = true, holding = false;

    private float holdingTime = 0;

    private Vector2 tmpSize;

    //用于改变透明度
    private SpriteRenderer spriteRenderer;

    //销毁处理
    private Action<TapScript> destroyAction;

    public void SetDestroyAction(Action<TapScript> destroyAction) => this.destroyAction = destroyAction;

    private void OnEnable()
    {
        Timer = -10f / 5;
        add = true;
        remove = true;
        holding = false;
        holdingTime = 0;

        spriteRenderer.size = new Vector2(2.63f, (2.13f * 1f) * holdTime);
        tmpSize = spriteRenderer.size;
    }

    private void Awake()
    {
        spriteRenderer = transform.Find("Hold Body").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //计时
        Timer += Time.deltaTime;
        //时间到添加到判定序列
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
        //移动
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //特效
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        //计算分数
        //移出判定序列
        ChartManager.Instance.holdHeadPaddingList.Remove(this);
        ChartManager.Instance.holdingPaddingList.Remove(this);
        Destroy(gameObject, holdTime);
    }

    /// <summary>
    /// 头部判定
    /// </summary>
    /// <param name="xPos"></param>
    /// <returns></returns>
    public bool HeadPadding(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * 1)
        {
            //从头部判定序列移除
            remove = false;
            ChartManager.Instance.holdHeadPaddingList.Remove(this);
            //加入holding判定
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
            //按下时间的计时器
            holdingTime += Time.deltaTime;
            holding = false;
            Vector3 tmp = transform.position;
            //transform.position = new Vector3(tmp.x, 1.001f, -4f - (holdTime - holdingTime) * 2f);
            //transform.Translate(0, 2f * Time.deltaTime, 0);
            transform.position = new Vector3(tmp.x, 1.001f, -4.5f);
            spriteRenderer.size = new Vector2(2.63f, tmpSize.y * (holdTime - holdingTime));
            if (holdingTime > holdTime)
            {
                ChartManager.Instance.holdingPaddingList.Remove(this);
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