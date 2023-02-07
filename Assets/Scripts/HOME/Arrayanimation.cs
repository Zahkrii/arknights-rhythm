using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Arrayanimation : MonoBehaviour
{
    
    public GameObject leftarray;
    public GameObject rightarray;

    public Vector3 LPosition;
    public Vector3 RPosition;

    public float MovingLength=150;//移动距离
    public LoopType loopType=LoopType.Restart; //循环的模式
    public float FadeTime=0.75f;//淡出时间

    public Sequence leftsequence;
    public Sequence rightsequence;

    // Start is called before the first frame update
    void Awake()
    {
        LPosition = new Vector3(leftarray.transform.position.x, leftarray.transform.position.y, leftarray.transform.position.z);
        RPosition = new Vector3(rightarray.transform.position.x, rightarray.transform.position.y, rightarray.transform.position.z);
        //初始化动画序列
        leftsequence = DOTween.Sequence();
        rightsequence = DOTween.Sequence();

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
    void back()
    {
        //箭头复位
        leftarray.transform.position = LPosition;
        rightarray.transform.position = RPosition;
        //箭头可见
        leftarray.GetComponent<Image>().DOFade(1, 0.5f);
        rightarray.GetComponent<Image>().DOFade(1, 0.5f);
    }

}
