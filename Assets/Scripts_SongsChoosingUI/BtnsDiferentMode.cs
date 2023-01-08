using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnsDiferentMode : MonoBehaviour
{
    public GameObject curButton;
    public GameObject othButton1;
    public GameObject othButton2;
    public GameObject othButton3;

    public GameObject curText;
    public GameObject othText1;
    public GameObject othText2;
    public GameObject othText3;

    public void Start()
    {
        if(othText1 != null) othText1.SetActive(false);
        if(othText2 != null) othText2.SetActive(false);
        if(othText3 != null) othText3.SetActive(false);
        if(curText != null) curText.SetActive(false);
    }

    public void activate()
    {
        curButton.SetActive(true); 
        curText.SetActive(true);
    }

    public void disable()
    {
        if (othButton1 != null)
            othButton1.SetActive(false);
        if (othButton2 != null)
            othButton2.SetActive(false);
        if (othButton3 != null)
            othButton3.SetActive(false);

        if (othText1 != null)
            othText1.SetActive(false);
        if (othText2 != null)
            othText2.SetActive(false);
        if (othText3 != null)
            othText3.SetActive(false);
    }
}
