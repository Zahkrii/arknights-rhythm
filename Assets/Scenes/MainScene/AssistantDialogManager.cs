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
        //fadeTime �ڱ�Ϊ��ʾ���Ի���
    }
    public void PanelFadeOut()
    {
        dialogshow = false;
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
        //fadeTime �ڱ�Ϊ��ʧ���Ի���
    }
    IEnumerator HideDialog()
    {
        yield return new WaitForSeconds(5);
        dialogshow = false;
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
    }
}