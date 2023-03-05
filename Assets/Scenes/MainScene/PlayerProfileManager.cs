using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    public TextMeshProUGUI doctorNameTMP;
    public TextMeshProUGUI doctorIDTMP;
    public TextMeshProUGUI LevelTMP;
    public TextMeshProUGUI currentexpTMP;


    private string Profile = "Amiya";

    private string tmp_id = "CB05";
    //��ʱ������������id

    // Start is called before the first frame update
    void Start()
    {
        InitDoctorProfile(tmp_id);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitDoctorProfile(string doctorID)
    {
        //��ʼ����ʿ���棬������Ϣ�������
        doctorIDTMP.text = doctorID;
        doctorNameTMP.text = "��ʿ";
        LevelTMP.text = "120";
        currentexpTMP.text = "114" ;
    }
}
