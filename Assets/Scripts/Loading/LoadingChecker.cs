using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class LoadingChecker : MonoBehaviour
{
    public GameObject FirstLoadingUI;
    public GameObject LoadingUI;
    public InputField NameField;
    public Button ConfirmButton;
    public Text NameText;
    private void Awake()
    {
        if(!SaveManager.PlayerSaveExists())//��������ڴ浵�ļ�,������״ε�½ҳ��
        {
            FirstLoadingUI.SetActive(true);
            LoadingUI.SetActive(false);
            NameField.onEndEdit.AddListener(delegate { LockInput(NameField); });
        }
        else//������ͨ��½ҳ��
        {
            FirstLoadingUI.SetActive(false);
            LoadingUI.SetActive(true);
            SaveManager.Open();
            NameText.text = "Doc." + SaveManager.PlayerSave.playerID + ", ��ӭ����";
            SaveManager.Close();
        }
    }
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)//��ʼ���浵
        {
            Debug.Log("��ʼ���浵");
            SaveManager.Init(input.text);
        }
        else if (input.text.Length == 0)//����Ϊ�վ�Ĭ������
        {
            Debug.Log("Ĭ�ϳ�ʼ���浵");
            SaveManager.Init("Who");
        }
    }
}
