using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class Arrayanimation : MonoBehaviour
{
    //��ͷ���
    public GameObject leftarray;
    public GameObject rightarray;

    public Vector3 LPosition;
    public Vector3 RPosition;

    public float MovingLength=150;//�ƶ�����
    public LoopType loopType=LoopType.Restart; //ѭ����ģʽ
    public float FadeTime=0.75f;//����ʱ��

    public Sequence leftsequence;
    public Sequence rightsequence;


    //��Ϣ�����
    public List<GameObject> ChapterInfo;
    public List<GameObject> ChapterPainting;

    //�½ںű�
    public List<Image> ChapterOrder;

    //��Ƭ
    public Image disc;
    Tween twe;
    void Awake()
    {
        LPosition = new Vector3(leftarray.transform.position.x, leftarray.transform.position.y, leftarray.transform.position.z);
        RPosition = new Vector3(rightarray.transform.position.x, rightarray.transform.position.y, rightarray.transform.position.z);
        //��ʼ����������
        leftsequence = DOTween.Sequence();
        rightsequence = DOTween.Sequence();
        //�ڶ������������λ�ƶ�������������������
        leftsequence.Append(leftarray.transform.DOMove(new Vector3(leftarray.transform.position.x - MovingLength, leftarray.transform.position.y, leftarray.transform.position.z), 1f).SetEase(Ease.InCubic));
        rightsequence.Append(rightarray.transform.DOMove(new Vector3(rightarray.transform.position.x + MovingLength, rightarray.transform.position.y, rightarray.transform.position.z), 1f).SetEase(Ease.InCubic));
        //�ڶ�����������ӵ��������������ú������ö���
        leftsequence.Append(leftarray.GetComponent<Image>().DOFade(0, FadeTime).OnComplete(back));
        rightsequence.Append(rightarray.GetComponent<Image>().DOFade(0, FadeTime).OnComplete(back));
        //���ö�������ѭ������
        leftsequence.SetLoops(-1,loopType);
        rightsequence.SetLoops(-1,loopType);

        for (int i = 1; i < ChapterInfo.Count; i++)
        {
            ChapterOrder[i].GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, true);
        }

        //��Ƭ��ת
        //��ʱ���õ��µ�һ�����ֶ�����һ��
        disc.transform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc);
        float timer = 0;
        Tween t = DOTween.To(() => timer, x => timer = x, 1, 4.25f)
                      .OnStepComplete(() =>
                      {
                          disc.transform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc);
                      })
                      .SetLoops(-1);      
    }

    void back()
    {
        //��ͷ��λ
        leftarray.transform.position = LPosition;
        rightarray.transform.position = RPosition;
        //��ͷ�ɼ�
        leftarray.GetComponent<Image>().DOFade(1, 0.5f);
        rightarray.GetComponent<Image>().DOFade(1, 0.5f);
    }

    public void ChInfoSwitch(int index)//�Ϸ�����ui���л�����
    { 
        for(int i=0;i<ChapterInfo.Count;i++)
        {
            ChapterInfo[i].GetComponent<Image>().CrossFadeAlpha(0, 0.1f, true);
            ChapterPainting[i].GetComponent<Image>().CrossFadeAlpha(0, 0.5f, true);
            ChapterOrder[i].GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, true);
        }
        ChapterInfo[index].GetComponent<Image>().CrossFadeAlpha(1, 1f, true);
        ChapterPainting[index].GetComponent<Image>().CrossFadeAlpha(1, 0.5f, true);
        ChapterOrder[index].GetComponent<Image>().CrossFadeAlpha(1, 0.5f, true);
    }

}
