using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//实现关卡参数UI各种交互功能
[Serializable]
public class MusicBTN
{
    public int num;//按钮编号
    public GameObject CurrentButton;//关卡
    public Button RealCurrentButton;//关卡按钮
    public GameObject SpeficChoicesUI;//关卡参数UI
    public Button EndButton;
}
public class ButtonManager : MonoBehaviour
{
    public int BtnNum;//所有关卡数量
    public List<MusicBTN> Buttons;

    void Awake()
    {
        AudioManager.Instance.MusicVolume = 0.6f;//设置音量
        AudioManager.Instance.SFXVolume = 0.6f;
        AudioManager.Instance.StopMusic();

        if (Buttons[6].CurrentButton != null ) { Buttons[6].CurrentButton.SetActive(true); }
        for (int i = 0; i < BtnNum; i++)
        {
            if (Buttons[i].CurrentButton != null) { Buttons[i].SpeficChoicesUI.SetActive(false); }//所有关卡具体参数先不出现
        }
        for (int i = 1; i < BtnNum; i++)
        {
            if (Buttons[i].CurrentButton != null&& i !=6 ) { Buttons[i].CurrentButton.SetActive(false);}//除了第一个按钮其他按钮先不出现
        }
        for(int i = 0; i < BtnNum; i++)
        {
            int temp = i;
            if (Buttons[i].CurrentButton != null) 
            { 
                Buttons[i].RealCurrentButton.onClick.AddListener(() => { isCurBtnClicked(temp); });//监听触发关卡具体参数UI的按钮
                Buttons[i].EndButton.onClick.AddListener(() => { isEnd(temp); }); //监听事件结束按钮
            }
        }
    }

    void isCurBtnClicked(int num)
    {
        Debug.Log("UIopen");
        Buttons[num].SpeficChoicesUI.SetActive(true);//打开具体参数
        Buttons[num].SpeficChoicesUI.GetComponent<CanvasGroupShow>().showUi();
        AudioManager.Instance.PlayMusic((num).ToString());//播放音乐
        Debug.Log("MUSIC PLAYED:"+(num));
    }

    void isEnd(int num)
    {
        Debug.Log("UIclose");
        AudioManager.Instance.StopMusic();
        Debug.Log("MUSIC CLOSED");
        Buttons[num].SpeficChoicesUI.GetComponent<CanvasGroupShow>().closeUI();
        //StartCoroutine(EndTask());//等待淡出画面
        Buttons[num].SpeficChoicesUI.SetActive(false);

    }

}
