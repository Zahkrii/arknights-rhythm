using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    private float Timer = -10f / 5;

    private bool add = true, remove = true;

    //Note数据
    private float size = 1;

    public float Size
    { set { size = value; } }

    // Update is called once per frame
    private void Update()
    {
        Timer += Time.deltaTime;
        if (add && Timer > -0.17f)
        {
            DataManager.Instance.tapPaddingList.Add(this);
            add = false;
        }
        else if (remove && Timer > 0.17f)
        {
            DataManager.Instance.tapPaddingList.Remove(this);
            remove = false;
            Miss();
        }
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //生成特效
        VFXController.Instance.ShowMissEffect(this.transform.position.x);
        //计算分数
        ScoreManager.Instance.MissNote();
        //从判定序列移除
        DataManager.Instance.tapPaddingList.Remove(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// 判定是否击中 Note
    /// </summary>
    /// <param name="xPos"></param>
    /// <returns></returns>
    public bool PaddingNote(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * size)
        {
            //生成特效
            VFXController.Instance.ShowPaddingEffect(Timer, xPos);
            SFXController.Instance.PlaySFX();
            //计算分数
            ScoreManager.Instance.ScoreTap(Timer);
            //从判定序列移除
            DataManager.Instance.tapPaddingList.Remove(this);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}