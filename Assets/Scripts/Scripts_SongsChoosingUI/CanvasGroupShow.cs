using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupShow : MonoBehaviour
{
    public CanvasGroup ex;

    public void closeUI()//关闭函数
    {
        //Debug.Log("closing。。。");
        ex.alpha = 0;
        //StartCoroutine(CloseTask());
    }

    public void showUi()//打开函数
    {
       // Debug.Log("loading。。。");
        StartCoroutine(ShowTask());
    }

    IEnumerator ShowTask()//协程迭代器，协程里用循环来实现透明度迭代
    {
        for(int i=0;i<10;i++)
        {
            ex.alpha += 0.1f;
            //Debug.Log("al:" + ex.alpha);
            yield return new WaitForSeconds(0.025f);
        }
    }

    /*IEnumerator CloseTask()//协程迭代器，协程里用循环来实现透明度迭代
    {
        for (int i = 0; i < 10; i++)
        {
            ex.alpha -= 0.1f;
            Debug.Log("al:" + ex.alpha);
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}

