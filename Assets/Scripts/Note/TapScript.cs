using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    private float Timer = -10f / (5 * 1);

    private bool add = true, remove = true;

    //Note����
    //private float size = 1;

    private void Update()
    {
        //��ʱ
        Timer += Time.deltaTime;
        //ʱ�䵽���ӵ��ж�����
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
        //�ƶ�
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //������Ч
        VFXManager.Instance.ShowMissEffect(this.transform.position.x);
        //�������
        ScoreManager.Instance.MissNote();
        //���ж������Ƴ�
        DataManager.Instance.tapPaddingList.Remove(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// �ж��Ƿ���� Note
    /// </summary>
    /// <param name="xPos"></param>
    /// <returns></returns>
    public bool PaddingNote(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * 1)
        {
            //������Ч
            VFXManager.Instance.ShowPaddingEffect(Timer, xPos);
            AudioManager.Instance.PlaySFX("hit");
            //�������
            ScoreManager.Instance.ScoreTap(Timer);
            //���ж������Ƴ�
            DataManager.Instance.tapPaddingList.Remove(this);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}