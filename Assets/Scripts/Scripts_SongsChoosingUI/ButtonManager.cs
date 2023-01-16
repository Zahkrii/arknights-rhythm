using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//ʵ�ֹؿ�����UI���ֽ�������
[Serializable]
public class MusicBTN
{
    public int num;//��ť���
    public GameObject CurrentButton;//�ؿ�
    public Button RealCurrentButton;//�ؿ���ť
    public GameObject SpeficChoicesUI;//�ؿ�����UI
    public Button LeftArray;//���ͷ
    public Button RightArray;//�Ҽ�ͷ
    public Button EndButton;
}
public class ButtonManager : Data
{
    public int BtnNum;//���йؿ�����
    public List<MusicBTN> Buttons;
    public int LockNum;
    public int UnlockScore;

    void Awake()
    {
        AudioManager.Instance.MusicVolume = 0.6f;//��������
        AudioManager.Instance.SFXVolume = 0.6f;
        AudioManager.Instance.StopMusic();

        if (Buttons[6].CurrentButton != null ) { Buttons[6].CurrentButton.SetActive(true); }
        for (int i = 0; i < BtnNum; i++)
        {
            if (Buttons[i].CurrentButton != null) { Buttons[i].SpeficChoicesUI.SetActive(false); }//���йؿ���������Ȳ�����
        }
        for (int i = 1; i < BtnNum; i++)
        {
            if (Buttons[i].CurrentButton != null&& i !=6 ) { Buttons[i].CurrentButton.SetActive(false);}//���˵�һ����ť������ť�Ȳ�����
        }
        for(int i = 0; i < BtnNum; i++)
        {
            int temp = i;
            if (Buttons[i].CurrentButton != null) 
            { 
                Buttons[i].RealCurrentButton.onClick.AddListener(() => { isCurBtnClicked(temp); });//���������ؿ��������UI�İ�ť
                Buttons[i].EndButton.onClick.AddListener(() => { isEnd(temp); }); //�����¼�������ť
                Buttons[i].LeftArray.onClick.AddListener(() => { isCurLeftArrayClicked(temp); });//�����ͷ����¼�
                Buttons[i].RightArray.onClick.AddListener(() => { isCurRightArrayClicked(temp); });//���Ҽ�ͷ����¼�
                if (i == 0 || i == 6 && Buttons[i] != null) Buttons[i].LeftArray.GetComponent<Button>().interactable = false;//1-1��SE-1���ͷ��Ч��        
                if (i == 5 || i == 10 && Buttons[i] != null) Buttons[i].RightArray.GetComponent<Button>().interactable = false;//1-6��SE-5�Ҽ�ͷ��Ч��
                boolArray(i);
            }
        }
    }

    void isCurBtnClicked(int num)//���ﴫ���num����i
    {
        Debug.Log("UIopen");
        Buttons[num].SpeficChoicesUI.SetActive(true);//�򿪾������
        Buttons[num].SpeficChoicesUI.GetComponent<CanvasGroupShow>().showUi(num);
        AudioManager.Instance.PlayMusic((num).ToString());//��������
    }

    void isCurLeftArrayClicked(int number)//���ͷ�����л�SpecificUI����
    {
        AudioManager.Instance.StopMusic();
        Buttons[number].SpeficChoicesUI.SetActive(false);
        Buttons[number - 1].SpeficChoicesUI.SetActive(true);
        Buttons[number - 1].SpeficChoicesUI.GetComponent<CanvasGroupShow>().showUi(number - 1);
        AudioManager.Instance.PlayMusic((number-1).ToString());//��������
        Debug.Log("��" + number + "�����ͷ����");
    }
    void isCurRightArrayClicked(int number)//�Ҽ�ͷ�����л�SpecificUI����
    {
        AudioManager.Instance.StopMusic();
        Buttons[number].SpeficChoicesUI.SetActive(false);
        Buttons[number + 1].SpeficChoicesUI.SetActive(true);
        Buttons[number + 1].SpeficChoicesUI.GetComponent<CanvasGroupShow>().showUi(number + 1);
        AudioManager.Instance.PlayMusic((number+1).ToString());//��������
        Debug.Log("��" + number + "���Ҽ�ͷ����");
    }

    void boolArray(int number)//�ж��Ҽ�ͷ�Ƿ���Ч����
    {
        if ((Buttons[number+1].SpeficChoicesUI != null) && Score[0, number] <= 10000 && Score[1, number] <= 10000 && Score[2, number] <= 10000 )
        {
            Buttons[number].RightArray.GetComponent<Button>().interactable = false;
            //Debug.Log("�Ҽ�ͷ�����Ч��");
        }
        else if(number==LockNum-2 && (Score[1, LockNum - 2] < UnlockScore && Score[2, LockNum - 2] < UnlockScore))//����ؿ�����
        {
            Buttons[number].RightArray.GetComponent<Button>().interactable = false;
            //Debug.Log("�Ҽ�ͷ�����Ч��");
        }
        else if ((Buttons[number+1].SpeficChoicesUI != null))
        {
            Buttons[number].RightArray.GetComponent<Button>().interactable = true;
            //Debug.Log("�Ҽ�ͷ���ָ�Ч��");
        }
        //Debug.Log("������"+number);
    }

    void isEnd(int num)
    {
        //Debug.Log("UIclose");
        AudioManager.Instance.StopMusic();
        //Debug.Log("MUSIC CLOSED");
        //Buttons[num].SpeficChoicesUI.GetComponent<CanvasGroupShow>().closeUI();
        //StartCoroutine(EndTask());//�ȴ���������
        Buttons[num].SpeficChoicesUI.SetActive(false);
    }
}
