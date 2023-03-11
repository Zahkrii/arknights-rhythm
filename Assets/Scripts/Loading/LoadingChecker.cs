using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;
using DG.Tweening;
using static Unity.Burst.Intrinsics.X86.Avx;

public class LoadingChecker : MonoBehaviour
{
    public GameObject FirstLoadingUI;
    public GameObject LoadingUI;
    //首次登陆页面
    public TMP_InputField NameField;
    public Button ConfirmButton;
    private Sequence NameCardImage; //信息卡动画序列
    public float MovingLength;
    public GameObject NameCard;//信息卡片
    public Image DocImage;//博士头像
    public Image Line_1;//上分割线
    public Image Line_2;//下分割线
    public Image BtnImage;//确认按钮图片
    public TextMeshProUGUI Guide;//指导文字
    public TextMeshProUGUI PreInput;//输入框文字
    private Sequence NameCardOut; //信息卡动画序列
    public Image Loading_f;//登陆按钮图片
    public Image Title_f;//游戏标题
    public GameObject LoadingBtn_f;//登录按钮
    //普通登陆页面
    public Image Loading;//登陆按钮
    private Sequence LoadingBtnImage; //登陆按钮动画序列

    private void Awake()
    {
        if(!SaveManager.PlayerSaveExists())//如果不存在存档文件,则进入首次登陆页面
        {
            FirstLoadingUI.SetActive(true);
            LoadingBtn_f.SetActive(false);
            LoadingUI.SetActive(false);

            NameField.onEndEdit.AddListener(delegate { LockInput(NameField); });
            NameCardImage = DOTween.Sequence();//初始化动画序列
            NameCardImage.Append(NameCard.transform.DORotate(new Vector3(0, 0, 12f), 0.5f, RotateMode.FastBeyond360));
            NameCardImage.Append(NameCard.transform.DOMove(new Vector3(NameCard.transform.position.x - MovingLength, NameCard.transform.position.y, NameCard.transform.position.z), 1f).SetEase(Ease.OutQuad));
            NameCardImage.Append(DocImage.DOFade(1,1));//博士头像出现
            NameCardImage.Append(Line_1.DOFade(1, 0.5f));//上分割线出现
            string text = "您好博士，欢迎登陆RROS\r\n或许\r\n您对这里的环境一无所知\r\n但是请不要担心\r\n我将暂时代替PRTS\r\n为您提供相应的系统指引\r\n......\r\n那么，您的用户名是......";
            NameCardImage.Append(DOTween.To(() => string.Empty, value => Guide.text = value, text, 3f).SetEase(Ease.Linear));
            NameCardImage.Append(Line_2.DOFade(1, 0.5f));//下分割线出现
            string text2 = "请输入姓名";
            NameCardImage.Append(DOTween.To(() => string.Empty, value => PreInput.text = value, text2, 1f).SetEase(Ease.Linear));
            NameCardImage.Append(BtnImage.DOFade(1, 0.5f));//下分割线出现
        }
        else//进入普通登陆页面
        {
            FirstLoadingUI.SetActive(false);
            LoadingUI.SetActive(true);
            LoadingBtnImage = DOTween.Sequence();//初始化动画序列
            LoadingBtnImage.Append(Loading.DOFade(1, 2f).SetEase(Ease.InOutQuad));
            LoadingBtnImage.Append(Loading.DOFade(0.3f, 2f).SetEase(Ease.InOutQuad));
            LoadingBtnImage.SetLoops(-1);
        }
    }
    void LoadBtnBack()
    {
        Loading.DOFade(1, 4);
    }
    void LockInput(TMP_InputField input)
    {
        LoadingBtn_f.SetActive(true);

        NameCardOut = DOTween.Sequence();//初始化动画序列
        NameCardOut.Append(NameCard.transform.DORotate(new Vector3(0, 0, 0f), 0.5f, RotateMode.FastBeyond360));
        NameCardOut.Append(NameCard.transform.DOMove(new Vector3(NameCard.transform.position.x , NameCard.transform.position.y - MovingLength-100 , NameCard.transform.position.z), 1.5f).SetEase(Ease.InQuad));
        NameCardOut.Append(Title_f.DOFade(1, 2f).SetEase(Ease.InOutQuad));
        NameCardOut.Append(Loading_f.DOFade(0.3f, 2f).SetEase(Ease.InOutQuad));
        LoadingBtnImage = DOTween.Sequence();//初始化动画序列       
        LoadingBtnImage.Append(Loading_f.DOFade(1, 2f).SetEase(Ease.InOutQuad));
        LoadingBtnImage.Append(Loading_f.DOFade(0.3f, 2f).SetEase(Ease.InOutQuad));
        LoadingBtnImage.SetLoops(-1);
        NameCardOut.Append(LoadingBtnImage);

        if (input.text.Length > 0)//初始化存档
        {
            Debug.Log("初始化存档");
            SaveManager.Init(input.text);
        }
        else if (input.text.Length == 0)//内容为空就默认姓名
        {
            Debug.Log("默认初始化存档");
            SaveManager.Init("YouKnowWho");
        }
    }
}
