using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

/// <summary>
/// 滑动页面效果
/// </summary>
public class PageView : MonoBehaviour , IBeginDragHandler, IEndDragHandler
{
    private ScrollRect rect;//ScrollRect组件
    private float targethorizontal = 0;
    private List<float> posList = new List<float>();//存图片的位置(0, 0.333, 0.666, 1) 
    private bool isDrag = true;//是否拖动
    private float startTime = 0;
    private float startDragHorizontal;
    private int curIndex = 0;
    public float speed = 3;//滑动速度  

    public Arrayanimation Array;//实例化箭头对象

     void OnUpdateAnimation(int index)//拖动结束时的切换事件
     {
        //在下面加动画变换函数或者在别的脚本里检测位置变换
        if (Array.leftarray != null && Array.rightarray != null)
            releasearray();//箭头复位
        Array.ChInfoSwitch(index);//信息板更换
        //号标更换
     }
    void EndUpdateAnimation(int index)//拖动开始时的事件
    {
        //在下面加动画变换函数或者在别的脚本里检测位置变换
        if (Array.leftarray != null && Array.rightarray != null)
            hidearray();//箭头消失
    }

    void releasearray()//箭头复位函数
    {
        //箭头出现
        Array.leftarray.SetActive(true);
        Array.rightarray.SetActive(true);
        //重新播放动画
        Array.leftsequence.Restart();
        Array.rightsequence.Restart();
        Debug.Log("动画重置");
    }
    void hidearray()//拖动时隐藏箭头函数
    {
        Array.leftarray.SetActive(false);
        Array.rightarray.SetActive(false);
    }


    void Start()
    {
        rect = GetComponent<ScrollRect>();//初始化ScrollRect组件
        float horizontalLength = rect.content.rect.width - GetComponent<RectTransform>().rect.width;//水平长度等于content的宽度减去当前UI的宽度
        var _rectWidth = GetComponent<RectTransform>().rect.width;//_rectWidth是当前UI的宽度
        for (int i = 0; i < rect.content.transform.childCount; i++)//添加content下的四张图片的位置信息到posList里去
        {
            posList.Add(_rectWidth * i / horizontalLength);//存图片位置 
        }
        curIndex = 0;//当前页面索引是0
    }
    void Update()
    {
        if (!isDrag)
        {
            startTime += Time.deltaTime;
            float t = startTime * speed;
            //加速滑动效果
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, t);//插值函数
            //缓慢匀速滑动效果
            //rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, Time.deltaTime * speed);
            Debug.Log("拖动结束");          
        }
    }


    public void OnBeginDrag(PointerEventData eventData)//开始拖动
    {
        isDrag = true;
        startDragHorizontal = rect.horizontalNormalizedPosition;  //horizontalNormalizedPosition这个参数是scrollRect滑动期间变化的x坐标值，在（0， 1）之间

        //根据UI切换控制动画变换事件函数
        EndUpdateAnimation(curIndex);
    }

    public void OnEndDrag(PointerEventData eventData)//结束拖动
    {
        //Debug.Log("OnEndDrag");
        float posX = rect.horizontalNormalizedPosition;
        int index = 0;
        float offset = Mathf.Abs(posList[index] - posX);  //计算当前位置与第一页的偏移量，初始化offect
        for (int i = 1; i < posList.Count; i++)
        {    //遍历页签，选取当前x位置和每页偏移量最小的那个页面
            float temp = Mathf.Abs(posList[i] - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        curIndex = index;
        targethorizontal = posList[curIndex]; //设置当前坐标，更新函数进行插值  
        isDrag = false;
        startTime = 0;

        //根据UI切换控制动画变换事件函数
        OnUpdateAnimation(curIndex);
    }
}

//似乎是根据content下子物体的位置和宽度将整个scrollview内容划分成多个区域，鼠标没拖动到一定程度坐标就锁死在某个区域内，拖动超过阈值isDrug=true直接插值渐变坐标切换到相邻的区域
//GetComponent<RectTransform>().rect.width用于获取到当前UI的宽高
//注意是isDrug=false时执行切换