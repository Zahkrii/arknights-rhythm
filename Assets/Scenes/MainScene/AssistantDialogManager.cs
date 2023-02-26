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
    //协程是否开始运行

    private List<string> dialogs = new List<string> { 
        "博士，你还有很多任务没有完成，现在还不能休息！",
        "博士，你怎么累死了？"
    };

    private void Start()
    {
        dialogshow = true;
        string helloDialag = "欢迎回家，博士";
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
        //fadeTime 内变为显示出对话框
    }
    public void PanelFadeOut()
    {

        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
        //dialogtext.text = dialogs[(int)(Random.value * 10) % (dialogs.Count)];
        //fadeTime 内变为消失出对话框
    }
    IEnumerator HideDialog()
    {
        yield return new WaitForSeconds(5);
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
        //dialogtext.text = dialogs[(int)(Random.value * 10) % (dialogs.Count)];
    }
}