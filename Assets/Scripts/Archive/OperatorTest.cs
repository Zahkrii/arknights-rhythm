using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OperatorTest : MonoBehaviour
{
    public TextMeshProUGUI OperatorName;
    public Image OperatorImage;
    private AssistantData assistantData;
    private Sprite sprite;
    private Color color;
    // Update is called once per frame
    void Awake()
    { 
        if (SaveManager.PlayerSaveExists())
        {
            SaveManager.Open();
            assistantData = PlayerCardDataLoader.Instance.GetDataFromID(SaveManager.PlayerSave.assistantID);
            sprite = PlayerCardDataLoader.Instance.GetSpriteFromID(SaveManager.PlayerSave.assistantID);
            color = PlayerCardDataLoader.Instance.GetAccentColorFromID(SaveManager.PlayerSave.assistantID);
            Debug.Log(assistantData+" "+ sprite+" "+ color);
            SaveManager.Close();
        }
        OperatorImage.sprite = sprite;
        if(assistantData.opreatorID.ToString().Length>=11)
            OperatorName.text = "Nearl";
        else
            OperatorName.text = "" + assistantData.opreatorID;
    }
}
