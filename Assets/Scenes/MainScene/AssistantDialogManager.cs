using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssistantDialogManager : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    private bool dialogshow = true;
    public TextMeshProUGUI dialogtext;

    private Coroutine IsStartCoroutine;
    //Э���Ƿ�ʼ����

    private List<string> dialogs = new List<string> { 
        "��ʿ���㻹�кܶ�����û����ɣ����ڻ�������Ϣ��",
        "��ʿ������ô�����ˣ�"
    };

    private void Start()
    {
        dialogshow = true;
        string helloDialag = "��ӭ�ؼң���ʿ";
        dialogtext.text = helloDialag;
        IsStartCoroutine=StartCoroutine(HideDialog());
    }

    public void PanelFadeIn()
    {
        if(IsStartCoroutine!=null)
        {
            IsStartCoroutine=null;
            StopCoroutine(nameof(HideDialog));

        }



        if (dialogshow == true)
        {
            dialogshow= false;
            PanelFadeOut();

        }
        else
        {
            dialogshow = true;
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, fadeTime);
            dialogtext.text = dialogs[(int)(Random.value * 10) % (dialogs.Count)];
            IsStartCoroutine = StartCoroutine(HideDialog());
        }
        //fadeTime �ڱ�Ϊ��ʾ���Ի���
    }
    public void PanelFadeOut()
    {

        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
        //dialogtext.text = dialogs[(int)(Random.value * 10) % (dialogs.Count)];
        //fadeTime �ڱ�Ϊ��ʧ���Ի���
    }
    IEnumerator HideDialog()
    {
        yield return new WaitForSeconds(5);
        if (dialogshow == true) { 
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
        //dialogtext.text = dialogs[(int)(Random.value * 10) % (dialogs.Count)];
        }
    }
}