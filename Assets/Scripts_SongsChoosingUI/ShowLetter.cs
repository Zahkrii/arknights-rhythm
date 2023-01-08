using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//��ʾ��ս��ĸ
public class ShowLetter : Data
{
    public TextMeshProUGUI LetterText;
    private void Update()
    {
        if (LetterText != null&& Score[level, num]< 600000)//600000����Ϊ��ɫF
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(119, 123, 120, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "F" ;
        }
        else if (LetterText != null && Score[level, num] < 700000)//600000-699999Ϊ��ɫC
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(23, 164, 76, 255); 
            LetterText.GetComponent<TextMeshProUGUI>().text = "C";
        }
        else if (LetterText != null && Score[level, num] < 800000)//700000-799999Ϊ��ɫB
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(25, 180, 180, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "B";
        }
        else if (LetterText != null && Score[level, num] < 900000)//800000-899999Ϊ��ɫA
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(224, 73, 18, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "A";
        }
        else if (LetterText != null && Score[level, num] < 950000)//900000-949999Ϊ��ɫS
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = Color.white;
            LetterText.GetComponent<TextMeshProUGUI>().text = "S";
        }
        else if (LetterText != null && Score[level, num] < 980000)//950000-979999Ϊ��ɫS
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(229, 128, 23, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "S";
        }
        else if (LetterText != null && Score[level, num] < 1000000)//980000-999999Ϊ��ɫS
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(241, 214, 11, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "S";
        }
        else
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(241, 214, 11, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "X";
        }
    }
}
