using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    //����
    public Slider VoiceSlider;
    public Slider SFXSlider;
    public Slider SpeedSlider;

    //��ֵ��ʾ�ı���
    public TextMeshProUGUI VoiceText;
    public TextMeshProUGUI SFXText;
    public TextMeshProUGUI SpeedText;


    // Start is called before the first frame update
    void Start()
    {
        //Ĭ��ֵ�趨
        VoiceSlider.value = 0.5f;
        SpeedSlider.value = 7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        SettingsManager.ChartSpeed = (int)SpeedSlider.value;//�������ٱ�������
        SpeedText.GetComponent<TextMeshProUGUI>().text = ""+SettingsManager.ChartSpeed;//��ֵ��ʾ

        SettingsManager.SFXVolume = SFXSlider.value;//��Ч������������
        string ssf = string.Format("{0:f0}", SettingsManager.SFXVolume*100);//float����С��λ�̶�Ϊ����λ
        SFXText.GetComponent<TextMeshProUGUI>().text = ssf;//��ֵ��ʾ

        SettingsManager.MusicVolume = VoiceSlider.value;//����������������
        string sm = string.Format("{0:f0}", SettingsManager.MusicVolume*100);//float����С��λ�̶�Ϊ����λ
        VoiceText.GetComponent<TextMeshProUGUI>().text = sm;//��ֵ��ʾ
    }
}
