using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// ʵ�������������޸�
/// </summary>
public class NameChange : MonoBehaviour
{
    public TextMeshProUGUI DoctorNamePre;
    public GameObject NameInputField;
    //public TMP_InputField NameInputField;
    public Button ChangeNameBtn;//�޸İ�ť
    public Button ConfirmNameBtn;//ȷ�ϰ�ť
    public TextMeshProUGUI NameID;
    
private void Awake()
    {
        NameInputField.SetActive(false);
        ChangeNameBtn.onClick.AddListener(() => { ChangeName(); });//����ť����¼�����ť��������������
        ConfirmNameBtn.onClick.AddListener(() => { ConfirmName(); });//����ť����¼�����ť�������������ʧ
        if (SaveManager.PlayerSaveExists())
        {
            SaveManager.Open();
            DoctorNamePre.GetComponent<TextMeshProUGUI>().text = SaveManager.PlayerSave.playerName;
            SaveManager.Close();
        }
        NameInputField.GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { LockInput(NameInputField.GetComponent<TMP_InputField>()); });//����༭����ʱ�¼����޸Ĵ浵������
    }
    void ChangeName()
    {
        NameInputField.SetActive(true);
        NameID.GetComponent<TextMeshProUGUI>().text = "���ID:\t\t\t";
    }

    void LockInput(TMP_InputField input)//�޸���������
    {
        if (input.text.Length > 0)
        {
            Debug.Log("Text has been entered");
            if (SaveManager.PlayerSaveExists())
            {
                SaveManager.Open();
                SaveManager.PlayerSave.playerName = input.text;
                DoctorNamePre.GetComponent<TextMeshProUGUI>().text = SaveManager.PlayerSave.playerName;
                SaveManager.Close();
            }
        }
        else if (input.text.Length == 0)//����Ϊ�վͲ��޸�
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
            NameID.GetComponent<TextMeshProUGUI>().text = "���ID:\t\t\t" + SaveManager.PlayerSave.playerName;
            SaveManager.Close();
        }
    }
}
