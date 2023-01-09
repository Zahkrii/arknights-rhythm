using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpUnlock : Data
{
    public GameObject EpBtn;
    public int unlockScore;
    int CanUnlock=0;
    // Start is called before the first frame update
    void Start()
    {
        EpBtn.SetActive(false);//EP不出现
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 12; i++)
        {
            if (Score[2, i] >= unlockScore) CanUnlock++;
        }
        if(CanUnlock>=11)//解锁
                EpBtn.SetActive(true);
        CanUnlock=0;
    }
}
