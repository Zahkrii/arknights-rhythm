using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class HomePageAssistantManager : MonoBehaviour
{
    [Header("��Ա����")]
    [HideInInspector] public Character_SO characterData;


    public TextMeshProUGUI dialogTextTMP;

    private Opreator assistantID = new() { id = OpreatorID.Amiya };
    // Start is called before the first frame update
    void Start()
    {
        string tmp_id="CB05";
        
        //�����������,Ĭ��Ϊ����櫵�Ĭ��Ƥ��
        //badcode55������Ϊ��Ӣ��0����ʼƤ������Ĭ��Ƥ��

        //��ѯ��Ĭ��Ƥ����
        if (SaveManager.PlayerSave.opreators.Exists(item => item.id == OpreatorID.Amiya)){
            assistantID = new Opreator { id = OpreatorID.Amiya };
            //Ĭ�����֣������ Ĭ��Ƥ��
        }
        else
        {
            Debug.Log($"Error! Assistant {assistantID.id} SkinIndex {assistantID.skinIndex} doesn't exsist!");
        }

        if (SaveManager.PlayerSaveExists())
        {
            SaveManager.Init(playerID: tmp_id);
            //��ʼ���浵
        }


        //���޸�Ϊ��ѯ���֣��������ð����

        if (!SaveManager.PlayerSaveExists())
        {
            //��һ�ν���������
            //dialogTextTMP.text = characterData.characterAudios;
            //������hello������
        }
        else
        {
            SaveManager.Open();
            //��ȡ�浵

            //������Ա�Ի�����
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveAssisant()
    {

        //������
    }
}
