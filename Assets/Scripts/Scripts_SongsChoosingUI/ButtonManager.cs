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
    public Button EndButton;
}
public class ButtonManager : MonoBehaviour
{
    public int BtnNum;//���йؿ�����
    public List<MusicBTN> Buttons;

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
            }
        }
    }

    void isCurBtnClicked(int num)
    {
        Debug.Log("UIopen");
        Buttons[num].SpeficChoicesUI.SetActive(true);//�򿪾������
        Buttons[num].SpeficChoicesUI.GetComponent<CanvasGroupShow>().showUi();
        AudioManager.Instance.PlayMusic((num).ToString());//��������
        Debug.Log("MUSIC PLAYED:"+(num));
    }

    void isEnd(int num)
    {
        Debug.Log("UIclose");
        AudioManager.Instance.StopMusic();
        Debug.Log("MUSIC CLOSED");
        Buttons[num].SpeficChoicesUI.GetComponent<CanvasGroupShow>().closeUI();
        //StartCoroutine(EndTask());//�ȴ���������
        Buttons[num].SpeficChoicesUI.SetActive(false);

    }

}
