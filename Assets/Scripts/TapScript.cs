using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    private float Timer = -10f / 5;

    private bool add = true, remove = true;

    private void Awake()
    {
    }

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
        //������Ч
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
        if (x < 0.4)
        {
            //������Ч
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