using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupShow : MonoBehaviour
{
    public CanvasGroup ex;

    public void closeUI()//�رպ���
    {
        //Debug.Log("closing������");
        ex.alpha = 0;
        //StartCoroutine(CloseTask());
    }

    public void showUi()//�򿪺���
    {
       // Debug.Log("loading������");
        StartCoroutine(ShowTask());
    }

    IEnumerator ShowTask()//Э�̵�������Э������ѭ����ʵ��͸���ȵ���
    {
        for(int i=0;i<10;i++)
        {
            ex.alpha += 0.1f;
            //Debug.Log("al:" + ex.alpha);
            yield return new WaitForSeconds(0.025f);
        }
    }

    /*IEnumerator CloseTask()//Э�̵�������Э������ѭ����ʵ��͸���ȵ���
    {
        for (int i = 0; i < 10; i++)
        {
            ex.alpha -= 0.1f;
            Debug.Log("al:" + ex.alpha);
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}

