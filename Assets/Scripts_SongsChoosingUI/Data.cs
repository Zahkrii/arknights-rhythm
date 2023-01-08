using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int level;//难度等级
    public int num;

    protected static int[,] Score=new int[4,12];//存储每关的最高通关情况

    // 临时分数接口，对接时请删除并重新写赋予分数函数
    public void NotPass()//没过
    {
        Score[level,num] = 10 > Score[level,num] ? 10 : Score[level,num];
    }
    public void Good()//一星
    {
        Score[level,num] = 10000 > Score[level,num] ? 10000 : Score[level,num];
    }
    public void Great()//两星
    {
        Score[level,num] = 700000 > Score[level,num] ? 700000 : Score[level,num];
    }
    public void Perfect()//三星
    {
        Score[level,num] = 950000 > Score[level,num] ? 950000 : Score[level,num];
    }
    public void Excellent()//突袭
    {
        Score[level,num] = 1000000 > Score[level, num] ? 1000000 : Score[level, num];
    }
    

}
