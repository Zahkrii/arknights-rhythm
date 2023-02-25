using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantDialogManager : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    private bool dialogshow = true;
    public void PanelFadeIn()
    {
        if (dialogshow == true)
        {
            PanelFadeOut();

        }
        else
        {
            dialogshow = true;
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, fadeTime);
            StartCoroutine(HideDialog());
        }
        //fadeTime 内变为显示出对话框
    }
    public void PanelFadeOut()
    {
        dialogshow = false;
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
        //fadeTime 内变为消失出对话框
    }
    IEnumerator HideDialog()
    {
        yield return new WaitForSeconds(5);
        dialogshow = false;
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
    }
}