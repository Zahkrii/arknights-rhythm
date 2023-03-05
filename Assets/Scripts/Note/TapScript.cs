using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    private float Timer = -10f / 5;

    private bool add = true, remove = true;

    //销毁处理
    private Action<TapScript> destroyAction;

    public void SetDestroyAction(Action<TapScript> destroyAction) => this.destroyAction = destroyAction;

    private void OnEnable()
    {
        Timer = -10f / 5;
        add = true;
        remove = true;
    }

    private void Update()
    {
        //计时
        Timer += Time.deltaTime;
        //时间到添加到判定序列
        if (add && Timer > -0.17f)
        {
            ChartManager.Instance.tapPaddingList.Add(this);
            add = false;
        }
        else if (remove && Timer > 0.17f)
        {
            ChartManager.Instance.tapPaddingList.Remove(this);
            remove = false;
            Miss();
        }
        //移动
        transform.Translate(0, -(5 * 1) * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //生成特效
        VFXManager.Instance.ShowMissEffect(this.transform.position.x);
        //计算分数
        ScoreManager.Instance.MissNote();
        //从判定序列移除
        ChartManager.Instance.tapPaddingList.Remove(this);
        //Destroy(gameObject);
        destroyAction.Invoke(this);
    }

    /// <summary>
    /// 判定是否击中 Note
    /// </summary>
    /// <param name="xPos"></param>
    /// <returns></returns>
    public bool PaddingNote(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * 1)
        {
            //生成特效
            VFXManager.Instance.ShowPaddingEffect(Timer, xPos);
            AudioManager.Instance.PlayHitSFX();
            //计算分数
            ScoreManager.Instance.ScoreTap(Timer);
            //从判定序列移除
            ChartManager.Instance.tapPaddingList.Remove(this);
            //Destroy(gameObject);
            destroyAction.Invoke(this);
            return true;
        }
        return false;
    }
}