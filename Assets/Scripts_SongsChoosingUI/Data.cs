using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int level;//�Ѷȵȼ�
    public int num;

    protected static int[,] Score=new int[4,12];//�洢ÿ�ص����ͨ�����

    // ��ʱ�����ӿڣ��Խ�ʱ��ɾ��������д�����������
    public void NotPass()//û��
    {
        Score[level,num] = 10 > Score[level,num] ? 10 : Score[level,num];
    }
    public void Good()//һ��
    {
        Score[level,num] = 10000 > Score[level,num] ? 10000 : Score[level,num];
    }
    public void Great()//����
    {
        Score[level,num] = 700000 > Score[level,num] ? 700000 : Score[level,num];
    }
    public void Perfect()//����
    {
        Score[level,num] = 950000 > Score[level,num] ? 950000 : Score[level,num];
    }
    public void Excellent()//ͻϮ
    {
        Score[level,num] = 1000000 > Score[level, num] ? 1000000 : Score[level, num];
    }
    

}
