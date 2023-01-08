using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//控制关卡出现
public class Passes : Data
{
    public List<GameObject> passes;
    public int LockNum;
    public int UnlockScore;
    // Start is called before the first frame update
    void Start()
    {
       for(int i = 1; i <6; i++)//1-X关卡
       {
            if (passes[i] != null)
            {
                if (i == LockNum - 1)//特殊关卡解锁
                {
                    if (Score[1, LockNum - 2] >= UnlockScore || Score[2, LockNum - 2] >= UnlockScore)
                    {
                        Debug.Log("Special Pass Unlock!");
                        passes[i].SetActive(true);
                    }
                    else
                    {
                        passes[i].SetActive(false);
                    }
                }
                else//正常关卡解锁
                {
                    if (Score[0, i - 1] >= 10000 || Score[1, i - 1] >= 10000 || Score[2, i - 1] >= 10000)
                    {
                        Debug.Log("Normal Pass Unlock!");
                        passes[i].SetActive(true);
                    }
                    else
                    {
                        passes[i].SetActive(false);
                    }
                }
            }
       }
        for (int i = 7; i < 11; i++)//SE-X关卡
        {   //正常关卡解锁
            if (passes[i] != null)
            {
                if (Score[0, i - 1] >= 10000 || Score[1, i - 1] >= 10000 || Score[2, i - 1] >= 10000)
                {
                    Debug.Log("Normal Pass Unlock!");
                    passes[i].SetActive(true);
                }
                else
                {
                    passes[i].SetActive(false);
                }
            }
        }
    }

}
