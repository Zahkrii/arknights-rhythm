using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 实现姓名输入与修改
/// </summary>
public class NameChange : MonoBehaviour
{
    public TextMeshProUGUI DoctorNamePre;
    public GameObject NameInputField;
    //public TMP_InputField NameInputField;
    public Button ChangeNameBtn;//修改按钮
    public Button ConfirmNameBtn;//确认按钮
    public TextMeshProUGUI NameID;
    
private void Awake()
    {
        NameInputField.SetActive(false);
        ChangeNameBtn.onClick.AddListener(() => { ChangeName(); });//给按钮添加事件：按钮按下输入界面出现
        ConfirmNameBtn.onClick.AddListener(() => { ConfirmName(); });//给按钮添加事件：按钮按下输入界面消失
        if (SaveManager.PlayerSaveExists())
        {
            SaveManager.Open();
            DoctorNamePre.GetComponent<TextMeshProUGUI>().text = SaveManager.PlayerSave.playerID;
            SaveManager.Close();
        }
        NameInputField.GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { LockInput(NameInputField.GetComponent<TMP_InputField>()); });//插入编辑结束时事件：修改存档中名字
    }
    void ChangeName()
    {
        NameInputField.SetActive(true);
        NameID.GetComponent<TextMeshProUGUI>().text = "玩家ID:\t\t\t";
    }

    void LockInput(TMP_InputField input)//修改姓名函数
    {
        if (input.text.Length > 0)
        {
            Debug.Log("Text has been entered");
            if (SaveManager.PlayerSaveExists())
            {
                SaveManager.Open();
                SaveManager.PlayerSave.playerID = input.text;
                DoctorNamePre.GetComponent<TextMeshProUGUI>().text = SaveManager.PlayerSave.playerID;
                SaveManager.Close();
            }
        }
        else if (input.text.Length == 0)//内容为空就不修改
        {
            Debug.Log("Main Input Empty");
        }
    }

    void ConfirmName()
    {
        NameInputField.SetActive(false);
        if (SaveManager.PlayerSaveExists())
        {
            SaveManager.Open();
            NameID.GetComponent<TextMeshProUGUI>().text = "玩家ID:\t\t\t" + SaveManager.PlayerSave.playerID;
            SaveManager.Close();
        }
    }
}
