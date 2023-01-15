using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    //滑块
    public Slider VoiceSlider;
    public Slider SFXSlider;
    public Slider SpeedSlider;

    //数值显示文本框
    public TextMeshProUGUI VoiceText;
    public TextMeshProUGUI SFXText;
    public TextMeshProUGUI SpeedText;


    // Start is called before the first frame update
    void Start()
    {
        //默认值设定
        VoiceSlider.value = 0.5f;
        SpeedSlider.value = 7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        SettingsManager.ChartSpeed = (int)SpeedSlider.value;//谱面流速变量传递
        SpeedText.GetComponent<TextMeshProUGUI>().text = ""+SettingsManager.ChartSpeed;//数值显示

        SettingsManager.SFXVolume = SFXSlider.value;//音效音量变量传递
        string ssf = string.Format("{0:f0}", SettingsManager.SFXVolume*100);//float变量小数位固定为后两位
        SFXText.GetComponent<TextMeshProUGUI>().text = ssf;//数值显示

        SettingsManager.MusicVolume = VoiceSlider.value;//音乐音量变量传递
        string sm = string.Format("{0:f0}", SettingsManager.MusicVolume*100);//float变量小数位固定为后两位
        VoiceText.GetComponent<TextMeshProUGUI>().text = sm;//数值显示
    }
}
