using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NameOperatornumLevelShowing : MonoBehaviour
{
    public TextMeshProUGUI NameID;
    public TextMeshProUGUI UnlockOperatorNum;
    private int UnlockOperatorsNum;
    public TextMeshProUGUI Level;
    // Start is called before the first frame update
    void Start()
    {
        //SaveManager.Init(playerID: "Garcia");
        if(SaveManager.PlayerSaveExists())
        {
            SaveManager.Open();
            //Debug.Log("�Ѵ򿪴浵�ļ�");
            //������ȡ
            if (NameID!=null) NameID.GetComponent<TextMeshProUGUI>().text = "���ID:\t\t\t" + SaveManager.PlayerSave.playerID;
            //��Ա����������ȡ
            for (int i = 0; i < SaveManager.PlayerSave.opreators.Count; i++)
            {
                UnlockOperatorsNum++;
            }
            if (UnlockOperatorNum != null) UnlockOperatorNum.GetComponent<TextMeshProUGUI>().text = "��Ա��������:\t"+UnlockOperatorsNum+"/9";
            UnlockOperatorsNum = 0;
            //�ȼ���ȡ
            Level.GetComponent<TextMeshProUGUI>().text = "��ʿ�ȼ�:\t\t" + SaveManager.PlayerSave.level + "/120";
            SaveManager.Close();
            //Debug.Log("�ѹرմ浵�ļ�");
        }
        else
        {
            Debug.Log("�Ҳ����浵�ļ�");
        }
        
    }

}
