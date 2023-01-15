using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ʵ��ͻϮ�ؿ������ͽ���
[Serializable]
public class ExLlock : Data
{
    public List<GameObject> LockButtons;
    public List<GameObject> ExStartBtns;
    public int UnlockPassNum;
    public int UnlockPassScore;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < LockButtons.Count; i++)//ͻϮ�ؿ�����
        {
            LockButtons[i].SetActive(false);
            ExStartBtns[i].SetActive(false);
        }
        if (check())
        {
            for (int i = 0; i < LockButtons.Count; i++)//ͻϮ�ؿ�����
            {
                LockButtons[i].SetActive(true);
            }
        }
    }
    

    bool check()//����Ƿ������������,������ĥ�ѻ�������950000�ּ���
    {
        if (Score[2,UnlockPassNum-1] < UnlockPassScore) { return false; }
        return true;
    }
}
