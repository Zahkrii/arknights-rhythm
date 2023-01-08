using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceChartSpeedManager : MonoBehaviour
{
    public GameObject UI;

    private void Start()
    {
        UI.SetActive(false);
    }

    public void CLOSE()
    {
        UI.SetActive(false);
    }
    public void OPEN()
    {
        UI.gameObject.SetActive(true);
    }
}
