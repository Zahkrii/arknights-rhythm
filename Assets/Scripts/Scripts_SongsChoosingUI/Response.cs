using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
//实现不同星级显示
public class Response : Data
{
    public GameObject[] icon0;
    public GameObject[] icon1;
    public GameObject[] icon2;
    public GameObject[] icon3;

    private void Awake()
    {
        Close0();
        Close1();
        Close2();
        Close3();
    }
    private void Update()
    {
        if (Score[3, num] != 0 && icon3[0] != null)
        {
            Close0();
            Close1();
            Close2();
            Special();
        }
        else if (Score[2, num] != 0)
        {
            Close0();
            Close1();
            Close3();
            Hard();
        }
        else if(Score[1,num] != 0)
        {
            Close0();
            Close2();
            Close3();
            Nomal();
        }
        else if (Score[0,num] !=0)
        {
            Close1();
            Close2();
            Close3();
            Easy();
        }
        
    }
    void Easy()
    {
        if (Score[0,num] < 10000)//没过
        {
            if (icon0[0] != null) { icon0[0].SetActive(true); }
            icon0[1].SetActive(false);
            icon0[2].SetActive(false);
            icon0[3].SetActive(false);
            icon0[4].SetActive(false);
        }
        else if (Score[0, num] < 700000)//一星
        {
            if (icon0[0] != null) { icon0[0].SetActive(true); }
            icon0[1].SetActive(true);
            icon0[2].SetActive(false);
            icon0[3].SetActive(false);
            icon0[4].SetActive(false);
        }
        else if (Score[0, num] < 950000)//两星
        {
            if (icon0[0] != null) { icon0[0].SetActive(true); }
            icon0[1].SetActive(true);
            icon0[2].SetActive(true);
            icon0[3].SetActive(false);
            icon0[4].SetActive(false);
        }
        else if (Score[0,num] < 1000000)//三星
        {
            if (icon0[0] != null) { icon0[0].SetActive(true); }
            icon0[1].SetActive(true);
            icon0[2].SetActive(true);
            icon0[3].SetActive(true);
            icon0[4].SetActive(false);
        }
        else
        {
            if (icon0[0] != null) { icon0[0].SetActive(true); }
            icon0[1].SetActive(true);
            icon0[2].SetActive(true);
            icon0[3].SetActive(true);
            icon0[4].SetActive(true);
        }
    }
    void Nomal()
    {
        if (Score[1, num] < 10000)//没过
        {
            if (icon1[0] != null) { icon1[0].SetActive(true); }
            icon1[1].SetActive(false);
            icon1[2].SetActive(false);
            icon1[3].SetActive(false);
            icon1[4].SetActive(false);
        }
        else if (Score[1, num] < 700000)//一星
        {
            if (icon1[0] != null) { icon1[0].SetActive(true); }
            icon1[1].SetActive(true);
            icon1[2].SetActive(false);
            icon1[3].SetActive(false);
            icon1[4].SetActive(false);
        }
        else if (Score[1, num] < 950000)//两星
        {
            if (icon1[0] != null) { icon1[0].SetActive(true); }
            icon1[1].SetActive(true);
            icon1[2].SetActive(true);
            icon1[3].SetActive(false);
            icon1[4].SetActive(false);
        }
        else if (Score[1, num] < 1000000)//三星
        {
            if (icon1[0] != null) { icon1[0].SetActive(true); }
            icon1[1].SetActive(true);
            icon1[2].SetActive(true);
            icon1[3].SetActive(true);
            icon1[4].SetActive(false);
        }
        else
        {
            if (icon1[0] != null) { icon1[0].SetActive(true); }
            icon1[1].SetActive(true);
            icon1[2].SetActive(true);
            icon1[3].SetActive(true);
            icon1[4].SetActive(true);
        }
    }
    void Special()
    {
        if (Score[3, num] < 10000)//没过
        {
            if (icon3[0] != null) { icon3[0].SetActive(true); }
            icon3[1].SetActive(false);
            icon3[2].SetActive(false);
            icon3[3].SetActive(false);
            icon3[4].SetActive(false);
        }
        else if (Score[3, num] < 700000)//一星
        {
            if (icon3[0] != null) { icon3[0].SetActive(true); }
            icon3[1].SetActive(true);
            icon3[2].SetActive(false);
            icon3[3].SetActive(false);
            icon3[4].SetActive(false);
        }
        else if (Score[3, num] < 950000)//两星
        {
            if (icon3[0] != null) { icon3[0].SetActive(true); }
            icon3[1].SetActive(true);
            icon3[2].SetActive(true);
            icon3[3].SetActive(false);
            icon3[4].SetActive(false);
        }
        else if (Score[3, num] < 1000000)//三星
        {
            if (icon3[0] != null) { icon3[0].SetActive(true); }
            icon3[1].SetActive(true);
            icon3[2].SetActive(true);
            icon3[3].SetActive(true);
            icon3[4].SetActive(false);
        }
        else
        {
            if (icon3[0] != null) { icon3[0].SetActive(true); }
            icon3[1].SetActive(true);
            icon3[2].SetActive(true);
            icon3[3].SetActive(true);
            icon3[4].SetActive(true);
        }
    }
    void Hard()
    {
        if (Score[2, num] < 10000)//没过
        {
            if (icon2[0] != null) { icon2[0].SetActive(true); }
            icon2[1].SetActive(false);
            icon2[2].SetActive(false);
            icon2[3].SetActive(false);
            icon2[4].SetActive(false);
        }
        else if (Score[2, num] < 700000)//一星
        {
            if (icon2[0] != null) { icon2[0].SetActive(true); }
            icon2[1].SetActive(true);
            icon2[2].SetActive(false);
            icon2[3].SetActive(false);
            icon2[4].SetActive(false);
        }
        else if (Score[2, num] < 950000)//两星
        {
            if (icon2[0] != null) { icon2[0].SetActive(true); }
            icon2[1].SetActive(true);
            icon2[2].SetActive(true);
            icon2[3].SetActive(false);
            icon2[4].SetActive(false);
        }
        else if (Score[2, num] < 1000000)//三星
        {
            if (icon2[0] != null) { icon2[0].SetActive(true); }
            icon2[1].SetActive(true);
            icon2[2].SetActive(true);
            icon2[3].SetActive(true);
            icon2[4].SetActive(false);
        }
        else
        {
            if (icon2[0] != null) { icon2[0].SetActive(true); }
            icon2[1].SetActive(true);
            icon2[2].SetActive(true);
            icon2[3].SetActive(true);
            icon2[4].SetActive(true);
        }
    }
    void Close0()
    {
        for(int i = 0; i < icon0.Length; i++)
        {
            icon0[i].SetActive(false);
        }
    }
    void Close1()
    {
        for (int i = 0; i < icon1.Length; i++)
        {
            icon1[i].SetActive(false);
        }
    }
    void Close2()
    {
        for (int i = 0; i < icon2.Length; i++)
        {
            icon2[i].SetActive(false);
        }
    }
    void Close3()
    {
        if(icon3 != null)
        {
            for (int i = 0; i < icon3.Length; i++)
            {
                icon3[i].SetActive(false);
            }
        }
    }
}
