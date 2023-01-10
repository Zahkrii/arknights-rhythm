using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NameOperatornumShowing : MonoBehaviour
{
    public TextMeshProUGUI NameID;
    public TextMeshProUGUI UnlockOperatorNum;
    private int UnlockOperatorsNum;
    // Start is called before the first frame update
    void Start()
    {
        //SaveManager.Init(playerID: "Garcia");
        if(SaveManager.PlayerSaveExists())
        {
            SaveManager.Open();
            //Debug.Log("已打开存档文件");
            //姓名读取
            if (NameID!=null) NameID.GetComponent<TextMeshProUGUI>().text = "玩家ID:\t\t\t" + SaveManager.PlayerSave.playerID;
            //干员解锁数量读取
            for (int i = 0; i < SaveManager.PlayerSave.opreators.Count; i++)
            {
                UnlockOperatorsNum++;
            }
            if (UnlockOperatorNum != null) UnlockOperatorNum.GetComponent<TextMeshProUGUI>().text = "干员解锁数量:\t"+UnlockOperatorsNum+"/9";
            UnlockOperatorsNum = 0;

            SaveManager.Close();
            //Debug.Log("已关闭存档文件");
        }
        else
        {
            Debug.Log("找不到存档文件");
        }
        
    }

}
