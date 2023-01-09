using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//此脚本用于档案界面显示关卡通关状况
public class PassesShowing : Data
{
    public TextMeshProUGUI Easy;
    public TextMeshProUGUI Hard;
    public TextMeshProUGUI Normal;
    public TextMeshProUGUI Extra;

    private int E,E1,E2,E3,E4;
    private int H,H1,H2,H3,H4;
    private int N,N1,N2,N3,N4;
    private int EX,EX1,EX2,EX3,EX4;

    private void Start()
    {
        E = E1 = E2 = E3 = E4 = 0;
        H = H1 = H2 = H3 = H4 = 0;
        N = N1 = N2 = N3 = N4 = 0;
        EX = EX1 = EX2 = EX3 = EX4 = 0;
        count();
    }
    void count()
    {
        for(int i = 0; i<12; i++)
        {
            //EASY
            if (Score[0, i] >= 10000 && Score[0, i]<700000) E1++;
            else if (Score[0, i] >=700000 && Score[0, i] < 950000) E2++;
            else if (Score[0, i] >=950000 && Score[0, i] < 1000000) E3++;
            else if (Score[0, i] >= 1000000) E4++;
            //NORMAL
            if (Score[1, i] >= 10000 && Score[1, i] < 700000) N1++;
            else if (Score[1, i] >= 700000 && Score[1, i] < 950000) N2++;
            else if (Score[1, i] >= 950000 && Score[1, i] < 1000000) N3++;
            else if (Score[1, i] >= 1000000) N4++;
            //HARD
            if (Score[2, i] >= 10000 && Score[2, i] < 700000) H1++;
            else if (Score[2, i] >= 700000 && Score[2, i] < 950000) H2++;
            else if (Score[2, i] >= 950000 && Score[2, i] < 1000000) H3++;
            else if (Score[2, i] >= 1000000) H4++;
            //EXTRA
            if (Score[3, i] >= 10000 && Score[3, i] < 700000) EX1++;
            else if (Score[3, i] >= 700000 && Score[3, i] < 950000) EX2++;
            else if (Score[3, i] >= 950000 && Score[3, i] < 1000000) EX3++;
            else if (Score[3, i] >= 1000000) EX4++;
        }
        E = E1 + E2 + E3 + E4;
        N = N1 + N2 + N3 + N4;
        H = H1 + H2 + H3 + H4;
        EX = EX1 + EX2 + EX3 + EX4;

        Easy.GetComponent<TextMeshProUGUI>().text = "EASY(" + E + "/11):\tLev1:" + E1 + "\tLev2:" + E2 + "\tLev3:" + E3 + "\tLev4:" + E4;
        Normal.GetComponent<TextMeshProUGUI>().text = "NORMAL(" + N + "/11):\tLev1:" + N1 + "\tLev2:" + N2 + "\tLev3:" + N3 + "\tLev4:" + N4;
        Hard.GetComponent<TextMeshProUGUI>().text = "HARD(" + H + "/11):\tLev1:" + H1 + "\tLev2:" + H2 + "\tLev3:" + H3 + "\tLev4:" + H4;
        Extra.GetComponent<TextMeshProUGUI>().text = "EXTRA(" + EX + "/3):\tLev1:" + EX1 + "\tLev2:" + EX2 + "\tLev3:" + EX3 + "\tLev4:" + EX4;
    }
  
}
