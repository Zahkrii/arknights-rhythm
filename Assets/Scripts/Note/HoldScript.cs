using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScript : MonoBehaviour
{
    //需要hold的时间
    public float holdTime;

    //计时器
    private float Timer;

    //判定序列锁
    private bool add, remove, holding;

    private float holdingTime;

    private Vector2 tmpSize;

    private bool isPerfect;

    //用于改变透明度
    private SpriteRenderer spriteRenderer;

    //销毁处理
    private Action<HoldScript> destroyAction;

    public void SetDestroyAction(Action<HoldScript> destroyAction) => this.destroyAction = destroyAction;

    private void OnEnable()
    {
        Timer = -10f / 5;
        add = true;
        remove = true;
        holding = false;
        holdingTime = 0;
        isPerfect = false;

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void SetHoldTime(float holdTime)
    {
        this.holdTime = holdTime;
        spriteRenderer.size = new Vector2(2.63f, (5 * holdTime));
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
        transform.Translate(0, -(5 * 1) * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //特效
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        VFXManager.Instance.ShowMissEffect(this.transform.position.x);
        //计算分数
        ScoreManager.Instance.MissNote();
        //移出判定序列
        ChartManager.Instance.holdHeadPaddingList.Remove(this);
        ChartManager.Instance.holdingPaddingList.Remove(this);
        //Destroy(gameObject, holdTime);
        destroyAction.Invoke(this);
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
            //生成特效
            VFXManager.Instance.ShowPaddingEffect(Timer, xPos);
            AudioManager.Instance.PlayHitSFX();
            //计算分数
            if (Timer <= 0.085f)
            {
                isPerfect = true;
            }
            else
            {
                isPerfect = false;
            }
            //从头部判定序列移除
            remove = false;
            ChartManager.Instance.holdHeadPaddingList.Remove(this);
            //加入holding判定
            ChartManager.Instance.holdingPaddingList.Add(this);
            holding = true;

            //Vector3 tmp = transform.position;
            //spriteRenderer.size = new Vector2(2.63f, tmpSize.y - (-4.5f - tmp.z));
            //transform.position = new Vector3(tmp.x, 1.001f, -4.5f);

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
            spriteRenderer.size = new Vector2(2.63f, 5 * (holdTime - holdingTime));
            //生成特效
            //VFXManager.Instance.ShowDragEffect(this.transform.position.x);
            if (holdingTime > holdTime)
            {
                ChartManager.Instance.holdingPaddingList.Remove(this);

                //计算分数
                ScoreManager.Instance.ScoreHold(isPerfect);
                //Destroy(gameObject);
                destroyAction.Invoke(this);
                break;
            }
            //每一帧执行一次
            yield return 0;
        }
        if (holdingTime < holdTime && holdingTime >= (holdTime - 0.085f))
        {
            ScoreP();
        }
        else if (holdingTime < holdTime && holdingTime >= (holdTime - 0.17f))
        {
            ScoreG();
        }
        else if (holdingTime < holdTime)
        {
            Miss();
        }
    }

    private void ScoreP()
    {
        //特效
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        //长按到了perfect阶段，仅根据头判判断是p还是g
        //VFXManager.Instance.ShowEffect(isPerfect, this.transform.position.x);
        //计算分数
        ScoreManager.Instance.ScoreHold(isPerfect);
        //移出判定序列
        //ChartManager.Instance.holdHeadPaddingList.Remove(this);
        ChartManager.Instance.holdingPaddingList.Remove(this);
        //Destroy(gameObject, holdTime);
        destroyAction.Invoke(this);
    }

    private void ScoreG()
    {
        //特效
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        //如果头判perfect，但仅长按到了good阶段，判为good
        if (isPerfect)
        {
            isPerfect = false;
            //VFXManager.Instance.ShowEffect(isPerfect, this.transform.position.x);
            //计算分数
            ScoreManager.Instance.ScoreHold(isPerfect);
        }
        else//如果头判good，又仅长按到了good阶段，判为miss
        {
            VFXManager.Instance.ShowMissEffect(this.transform.position.x);
            //计算分数
            ScoreManager.Instance.MissNote();
        }
        //移出判定序列
        //ChartManager.Instance.holdHeadPaddingList.Remove(this);
        ChartManager.Instance.holdingPaddingList.Remove(this);
        //Destroy(gameObject, holdTime);
        destroyAction.Invoke(this);
    }
}