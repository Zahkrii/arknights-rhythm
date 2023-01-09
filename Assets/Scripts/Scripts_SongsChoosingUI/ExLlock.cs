using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//实现突袭关卡锁定和解锁
[Serializable]
public class ExLlock : Data
{
    public List<GameObject> Exbuttons;
    public List<GameObject> ExStartBtns;
    public int UnlockPassNum;
    public int UnlockPassScore;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Exbuttons.Count; i++)//突袭关卡锁定
        {
            Exbuttons[i].SetActive(false);
            ExStartBtns[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (check())
        {
            for (int i = 0; i < Exbuttons.Count; i++)//突袭关卡解锁
            {
                Exbuttons[i].SetActive(true);
            }
        }
    }

    bool check()//检测是否满足解锁条件,第六关磨难环境大于950000分即可
    {
        if (Score[2,UnlockPassNum-1] < UnlockPassScore) { return false; }
        return true;
    }
}
