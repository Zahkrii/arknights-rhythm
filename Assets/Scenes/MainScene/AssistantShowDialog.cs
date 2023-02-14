using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantShowDialog : MonoBehaviour
{
    public GameObject dialog;

    public void OnAssistantClick()
    {
        dialog.SetActive(true);
        StartCoroutine(HideDialog());
    }
    IEnumerator HideDialog()
    {
        yield return new WaitForSeconds(5);
        dialog.SetActive(false);
    }
}
