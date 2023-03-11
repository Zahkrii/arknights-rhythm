using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class Arrayanimation : MonoBehaviour
{
    //箭头相关
    public GameObject leftarray;
    public GameObject rightarray;

    public Vector3 LPosition;
    public Vector3 RPosition;

    public float MovingLength=150;//移动距离
    public LoopType loopType=LoopType.Restart; //循环的模式
    public float FadeTime=0.75f;//淡出时间

    public Sequence leftsequence;
    public Sequence rightsequence;


    //信息板相关
    public List<GameObject> ChapterInfo;
    public List<GameObject> ChapterPainting;

    //章节号标
    public List<Image> ChapterOrder;

    //碟片
    public Image disc;
    Tween twe;
    void Awake()
    {
        LPosition = new Vector3(leftarray.transform.position.x, leftarray.transform.position.y, leftarray.transform.position.z);
        RPosition = new Vector3(rightarray.transform.position.x, rightarray.transform.position.y, rightarray.transform.position.z);
        //初始化动画序列
        leftsequence = DOTween.Sequence();
        rightsequence = DOTween.Sequence();
        if (leftarray != null && rightarray != null) 
        {
            //在动画序列中添加位移动画，且设置由慢到快
            leftsequence.Append(leftarray.transform.DOMove(new Vector3(leftarray.transform.position.x - MovingLength, leftarray.transform.position.y, leftarray.transform.position.z), 1f).SetEase(Ease.InCubic));
            rightsequence.Append(rightarray.transform.DOMove(new Vector3(rightarray.transform.position.x + MovingLength, rightarray.transform.position.y, rightarray.transform.position.z), 1f).SetEase(Ease.InCubic));
            //在动画序列中添加淡出动画，且设置后续重置动作
            leftsequence.Append(leftarray.GetComponent<Image>().DOFade(0, FadeTime).OnComplete(back));
            rightsequence.Append(rightarray.GetComponent<Image>().DOFade(0, FadeTime).OnComplete(back));
            //设置动画序列循环播放
            leftsequence.SetLoops(-1,loopType);
            rightsequence.SetLoops(-1,loopType);
        }
        

        for (int i = 1; i < ChapterInfo.Count; i++)
        {
            if(ChapterOrder[i]!=null)
                ChapterOrder[i].GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, true);
        }

        //碟片旋转
        //延时调用导致第一次需手动设置一下
        if(disc!=null)
        {
            disc.transform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc);
            float timer = 0;
            Tween t = DOTween.To(() => timer, x => timer = x, 1, 4.25f)
                          .OnStepComplete(() =>
                          {
                              disc.transform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc);
                          })
                          .SetLoops(-1);
        }
          
    }

    void back()
    {
        if (leftarray != null && rightarray != null)
        {
            //箭头复位
            leftarray.transform.position = LPosition;
            rightarray.transform.position = RPosition;
            //箭头可见
            leftarray.GetComponent<Image>().DOFade(1, 0.5f);
            rightarray.GetComponent<Image>().DOFade(1, 0.5f);
        }
    }

    public void ChInfoSwitch(int index)//上方两块ui的切换动画
    { 
        for(int i=0;i<ChapterInfo.Count;i++)
        {
            if(ChapterInfo[i]!=null)
            {
                ChapterInfo[i].GetComponent<Image>().CrossFadeAlpha(0, 0.1f, true);
                ChapterPainting[i].GetComponent<Image>().CrossFadeAlpha(0, 0.5f, true);
                ChapterOrder[i].GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, true);
            }
        }
        if (ChapterInfo[index] != null)
        {
            ChapterInfo[index].GetComponent<Image>().CrossFadeAlpha(1, 1f, true);
            ChapterPainting[index].GetComponent<Image>().CrossFadeAlpha(1, 0.5f, true);
            ChapterOrder[index].GetComponent<Image>().CrossFadeAlpha(1, 0.5f, true);
        }
          
    }

}
