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
    //�״ε�½ҳ��
    public TMP_InputField NameField;
    public Button ConfirmButton;
    private Sequence NameCardImage; //��Ϣ����������
    public float MovingLength;
    public GameObject NameCard;//��Ϣ��Ƭ
    public Image DocImage;//��ʿͷ��
    public Image Line_1;//�Ϸָ���
    public Image Line_2;//�·ָ���
    public Image BtnImage;//ȷ�ϰ�ťͼƬ
    public TextMeshProUGUI Guide;//ָ������
    public TextMeshProUGUI PreInput;//���������
    private Sequence NameCardOut; //��Ϣ����������
    public Image Loading_f;//��½��ťͼƬ
    public Image Title_f;//��Ϸ����
    public GameObject LoadingBtn_f;//��¼��ť
    //��ͨ��½ҳ��
    public Image Loading;//��½��ť
    private Sequence LoadingBtnImage; //��½��ť��������

    private void Awake()
    {
        if(!SaveManager.PlayerSaveExists())//��������ڴ浵�ļ�,������״ε�½ҳ��
        {
            FirstLoadingUI.SetActive(true);
            LoadingBtn_f.SetActive(false);
            LoadingUI.SetActive(false);

            NameField.onEndEdit.AddListener(delegate { LockInput(NameField); });
            NameCardImage = DOTween.Sequence();//��ʼ����������
            NameCardImage.Append(NameCard.transform.DORotate(new Vector3(0, 0, 12f), 0.5f, RotateMode.FastBeyond360));
            NameCardImage.Append(NameCard.transform.DOMove(new Vector3(NameCard.transform.position.x - MovingLength, NameCard.transform.position.y, NameCard.transform.position.z), 1f).SetEase(Ease.OutQuad));
            NameCardImage.Append(DocImage.DOFade(1,1));//��ʿͷ�����
            NameCardImage.Append(Line_1.DOFade(1, 0.5f));//�Ϸָ��߳���
            string text = "���ò�ʿ����ӭ��½RROS\r\n����\r\n��������Ļ���һ����֪\r\n�����벻Ҫ����\r\n�ҽ���ʱ����PRTS\r\nΪ���ṩ��Ӧ��ϵͳָ��\r\n......\r\n��ô�������û�����......";
            NameCardImage.Append(DOTween.To(() => string.Empty, value => Guide.text = value, text, 3f).SetEase(Ease.Linear));
            NameCardImage.Append(Line_2.DOFade(1, 0.5f));//�·ָ��߳���
            string text2 = "����������";
            NameCardImage.Append(DOTween.To(() => string.Empty, value => PreInput.text = value, text2, 1f).SetEase(Ease.Linear));
            NameCardImage.Append(BtnImage.DOFade(1, 0.5f));//�·ָ��߳���
        }
        else//������ͨ��½ҳ��
        {
            FirstLoadingUI.SetActive(false);
            LoadingUI.SetActive(true);
            LoadingBtnImage = DOTween.Sequence();//��ʼ����������
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

        NameCardOut = DOTween.Sequence();//��ʼ����������
        NameCardOut.Append(NameCard.transform.DORotate(new Vector3(0, 0, 0f), 0.5f, RotateMode.FastBeyond360));
        NameCardOut.Append(NameCard.transform.DOMove(new Vector3(NameCard.transform.position.x , NameCard.transform.position.y - MovingLength-100 , NameCard.transform.position.z), 1.5f).SetEase(Ease.InQuad));
        NameCardOut.Append(Title_f.DOFade(1, 2f).SetEase(Ease.InOutQuad));
        NameCardOut.Append(Loading_f.DOFade(0.3f, 2f).SetEase(Ease.InOutQuad));
        LoadingBtnImage = DOTween.Sequence();//��ʼ����������       
        LoadingBtnImage.Append(Loading_f.DOFade(1, 2f).SetEase(Ease.InOutQuad));
        LoadingBtnImage.Append(Loading_f.DOFade(0.3f, 2f).SetEase(Ease.InOutQuad));
        LoadingBtnImage.SetLoops(-1);
        NameCardOut.Append(LoadingBtnImage);

        if (input.text.Length > 0)//��ʼ���浵
        {
            Debug.Log("��ʼ���浵");
            SaveManager.Init(input.text);
        }
        else if (input.text.Length == 0)//����Ϊ�վ�Ĭ������
        {
            Debug.Log("Ĭ�ϳ�ʼ���浵");
            SaveManager.Init("YouKnowWho");
        }
    }
}
