using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Arrayanimation : MonoBehaviour
{
    
    public GameObject leftarray;
    public GameObject rightarray;

    public Vector3 LPosition;
    public Vector3 RPosition;

    public float MovingLength=150;//�ƶ�����
    public LoopType loopType=LoopType.Restart; //ѭ����ģʽ
    public float FadeTime=0.75f;//����ʱ��

    public Sequence leftsequence;
    public Sequence rightsequence;

    // Start is called before the first frame update
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

}
