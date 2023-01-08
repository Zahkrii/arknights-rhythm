using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ƹؿ�����
public class Passes : Data
{
    public List<GameObject> passes;
    public int LockNum;
    public int UnlockScore;
    // Start is called before the first frame update
    void Start()
    {
       for(int i = 1; i <6; i++)//1-X�ؿ�
       {
            if (passes[i] != null)
            {
                if (i == LockNum - 1)//����ؿ�����
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
                else//�����ؿ�����
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
        for (int i = 7; i < 11; i++)//SE-X�ؿ�
        {   //�����ؿ�����
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
