using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class LoadingChecker : MonoBehaviour
{
    public GameObject FirstLoadingUI;
    public GameObject LoadingUI;
    public InputField NameField;
    public Button ConfirmButton;
    public Text NameText;
    private void Awake()
    {
        if(!SaveManager.PlayerSaveExists())//如果不存在存档文件,则进入首次登陆页面
        {
            FirstLoadingUI.SetActive(true);
            LoadingUI.SetActive(false);
            NameField.onEndEdit.AddListener(delegate { LockInput(NameField); });
        }
        else//进入普通登陆页面
        {
            FirstLoadingUI.SetActive(false);
            LoadingUI.SetActive(true);
            SaveManager.Open();
            NameText.text = "Doc." + SaveManager.PlayerSave.playerID + ", 欢迎回来";
            SaveManager.Close();
        }
    }
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)//初始化存档
        {
            Debug.Log("初始化存档");
            SaveManager.Init(input.text);
        }
        else if (input.text.Length == 0)//内容为空就默认姓名
        {
            Debug.Log("默认初始化存档");
            SaveManager.Init("Who");
        }
    }
}
