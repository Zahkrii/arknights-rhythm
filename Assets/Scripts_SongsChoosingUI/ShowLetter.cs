using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//显示作战字母
public class ShowLetter : Data
{
    public TextMeshProUGUI LetterText;
    private void Update()
    {
        if (LetterText != null&& Score[level, num]< 600000)//600000以下为灰色F
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(119, 123, 120, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "F" ;
        }
        else if (LetterText != null && Score[level, num] < 700000)//600000-699999为绿色C
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(23, 164, 76, 255); 
            LetterText.GetComponent<TextMeshProUGUI>().text = "C";
        }
        else if (LetterText != null && Score[level, num] < 800000)//700000-799999为蓝色B
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(25, 180, 180, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "B";
        }
        else if (LetterText != null && Score[level, num] < 900000)//800000-899999为红色A
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(224, 73, 18, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "A";
        }
        else if (LetterText != null && Score[level, num] < 950000)//900000-949999为白色S
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = Color.white;
            LetterText.GetComponent<TextMeshProUGUI>().text = "S";
        }
        else if (LetterText != null && Score[level, num] < 980000)//950000-979999为橙色S
        {
            LetterText.GetComponent<TextMeshProUGUI>().color = new Color32(229, 128, 23, 255);
            LetterText.GetComponent<TextMeshProUGUI>().text = "S";
        }
        else if (LetterText != null && Score[level, num] < 1000000)//980000-999999为金色S
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
