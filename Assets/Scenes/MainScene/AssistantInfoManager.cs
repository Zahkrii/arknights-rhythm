using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssistantInfoManager : MonoBehaviour
{
    public TextMeshProUGUI EnglishName;
    public TextMeshProUGUI ChineseName;
    // Start is called before the first frame update
    void Start()
    {
        //EnglishName= GetComponent<TextMeshProUGUI>();
        EnglishName.text = "Amiya";
        ChineseName.text = "°¢Ã×æ«";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
