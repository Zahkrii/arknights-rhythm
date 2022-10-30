using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private Toggle panelToggle;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        panelToggle.isOn = false;
    }

    public void TogglePanel()
    {
        if (panelToggle.isOn)
        {
            rectTransform.DOAnchorPosX(-482, 0.5f);
        }
        else
        {
            rectTransform.DOAnchorPosX(-64, 0.5f);
        }
    }
}