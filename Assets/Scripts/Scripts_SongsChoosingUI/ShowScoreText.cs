using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//��ʾ��ս����
public class ShowScoreText : Data
{
    public TextMeshProUGUI scoreText;
    private void Update()
    {
        //Debug.Log("level:" + level + " num:" + num + " score:" + Score[level, num]);
        if(scoreText != null)
        {
            scoreText.GetComponent<TextMeshProUGUI>().text = "Highest:" + Score[level, num];
        }
    }
}
