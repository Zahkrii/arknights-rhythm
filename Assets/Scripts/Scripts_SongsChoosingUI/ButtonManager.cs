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
    public Button LeftArray;//左箭头
    public Button RightArray;//右箭头
    public Button EndButton;
}
public class ButtonManager : Data
{
    public int BtnNum;//所有关卡数量
    public List<MusicBTN> Buttons;
    public int LockNum;
    public int UnlockScore;

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

    void isCurBtnClicked(int num)//这里传入的num就是i
    {
        Debug.Log("UIopen");
        Buttons[num].SpeficChoicesUI.SetActive(true);//打开具体参数
        Buttons[num].SpeficChoicesUI.GetComponent<CanvasGroupShow>().showUi(num);

        int temp2 = num;
        boolArray(num); //判断右箭头是否有效
        Buttons[num].LeftArray.onClick.AddListener(() => { isCurLeftArrayClicked(temp2); });//给左箭头添加事件
        Buttons[num].RightArray.onClick.AddListener(() => { isCurRightArrayClicked(temp2); });//给右箭头添加事件
        AudioManager.Instance.PlayMusic((num).ToString());//播放音乐
    }

    void isCurLeftArrayClicked(int number)//左箭头按下切换SpecificUI函数
    {
        isEnd(number);
        number--;
        isCurBtnClicked(number);
        //Debug.Log("第" + number + "关左箭头按下");
    }
    void isCurRightArrayClicked(int number)//右箭头按下切换SpecificUI函数
    {
        isEnd(number);
        number++;
        isCurBtnClicked(number);
        //Debug.Log("第" + number + "关右箭头按下");
    }
    void boolArray(int number)//判断右箭头是否有效函数
    {
        //Buttons[number].RightArray.GetComponent<Button>().interactable = false;
        //Buttons[number].LeftArray.GetComponent<Button>().interactable = false;

        Debug.Log("等一会儿");
        if ((Buttons[number+1].SpeficChoicesUI != null) && Score[0, number] <= 10000 && Score[1, number] <= 10000 && Score[2, number] <= 10000 )
        {
            Buttons[number].RightArray.GetComponent<Button>().interactable = false;
            //Debug.Log("右箭头检测无效化");
        }
        else if(number==LockNum-2 && (Score[1, LockNum - 2] < UnlockScore && Score[2, LockNum - 2] < UnlockScore))//特殊关卡解锁
        {
            Buttons[number].RightArray.GetComponent<Button>().interactable = false;
            //Debug.Log("右箭头检测无效化");
        }
        else if ((Buttons[number+1].SpeficChoicesUI != null))
        {
            Buttons[number].RightArray.GetComponent<Button>().interactable = true;
            //Debug.Log("右箭头检测恢复效用");
        }
        if (number == 0 || number == 6 && Buttons[number] != null) Buttons[number].LeftArray.GetComponent<Button>().interactable = false;//1-1、SE-1左箭头无效化        
        if (number == 5 || number == 10 && Buttons[number] != null) Buttons[number].RightArray.GetComponent<Button>().interactable = false;//1-6、SE-5右箭头无效化
        //Debug.Log("检测完成"+number);
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
