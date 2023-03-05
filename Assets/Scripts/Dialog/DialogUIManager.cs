using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUIManager : MonoBehaviour
{
    [Header("PlayBack组件")]
    public Image playBackPanel;
    public Image returnButtonImg;
    private Transform[] playBackPairs;
    public Transform playBackPairsContainer;

    private float normalSpeed;
    private float panelSpeed;

    public float fadeTime;

    public bool isOpen;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        isOpen = false;
        normalSpeed = 1f / fadeTime;
        panelSpeed = 0.7f / fadeTime;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {

    }

    public void PlayBackFadeIn()
    {
        // playBackPanel.gameObject.SetActive(true);

        // 初始化 发生变化的组件透明度都为0
        // 获取当前所有PlayBack对
        int playBackPairCount = playBackPairsContainer.childCount;
        playBackPairs = new Transform[playBackPairCount];
        for (int i = 0; i < playBackPairCount; i++)
        {
            playBackPairs[i] = playBackPairsContainer.GetChild(i);
            playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color = new Color(
                playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.r,
                playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.g,
                playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.b,
                0f
            );
            playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color = new Color(
                playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.r,
                playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.g,
                playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.b,
                0f
            );
        }
        returnButtonImg.color = new Color(returnButtonImg.color.r, returnButtonImg.color.g, returnButtonImg.color.b, 0f);
        playBackPanel.color = new Color(playBackPanel.color.r, playBackPanel.color.g, playBackPanel.color.b, 0f);

        StartCoroutine(PlayBackFade(1));
    }

    public void PlayBackFadeOut()
    {
        // 初始化 发生变化的组件透明度都为0
        // 获取当前所有PlayBack对
        int playBackPairCount = playBackPairsContainer.childCount;
        playBackPairs = new Transform[playBackPairCount];
        for (int i = 0; i < playBackPairCount; i++)
        {
            playBackPairs[i] = playBackPairsContainer.GetChild(i);
            playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color = new Color(
                playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.r,
                playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.g,
                playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.b,
                1f
            );
            playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color = new Color(
                playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.r,
                playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.g,
                playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.b,
                1f
            );
        }
        returnButtonImg.color = new Color(returnButtonImg.color.r, returnButtonImg.color.g, returnButtonImg.color.b, 1f);
        playBackPanel.color = new Color(playBackPanel.color.r, playBackPanel.color.g, playBackPanel.color.b, 0.9f);

        StartCoroutine(PlayBackFade(0));
    }

    /// <summary>
    /// PlayBack淡入淡出
    /// </summary>
    /// <param name="method">0-淡出 1-淡入</param>
    /// <returns></returns>
    IEnumerator PlayBackFade(int method)
    {
        float timer = 0f;

        float curNormalSpeed = method == 0 ? -normalSpeed : normalSpeed;
        float curPanelSpeed = method == 0 ? -panelSpeed : panelSpeed;

        while (timer <= fadeTime)
        {
            for (int i = 0; i < playBackPairs.Length; i++)
            {
                playBackPairs[i] = playBackPairsContainer.GetChild(i);
                playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color = new Color(
                    playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.r,
                    playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.g,
                    playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.b,
                    method == 0 ? Mathf.Max(0, playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.a + curNormalSpeed * Time.deltaTime) : Mathf.Min(1, playBackPairs[i].Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color.a + curNormalSpeed * Time.deltaTime)
                );
                playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color = new Color(
                    playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.r,
                    playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.g,
                    playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.b,
                    method == 0 ? Mathf.Max(0, playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.a + curNormalSpeed * Time.deltaTime) : Mathf.Min(1, playBackPairs[i].Find("Dialog Content").GetComponent<TextMeshProUGUI>().color.a + curNormalSpeed * Time.deltaTime)
                );
            }
            returnButtonImg.color = new Color(returnButtonImg.color.r, returnButtonImg.color.g, returnButtonImg.color.b, method == 0 ? Mathf.Max(0, returnButtonImg.color.a + curNormalSpeed * Time.deltaTime) : Mathf.Min(1, returnButtonImg.color.a + curNormalSpeed * Time.deltaTime));
            playBackPanel.color = new Color(playBackPanel.color.r, playBackPanel.color.g, playBackPanel.color.b, method == 0 ? Mathf.Max(0, playBackPanel.color.a + curPanelSpeed * Time.deltaTime) : Mathf.Min(0.7f, playBackPanel.color.a + curPanelSpeed * Time.deltaTime));
            timer += Time.deltaTime;

            yield return null;
        }

        if (method == 0) isOpen = false;
        else if (method == 1) isOpen = true;
        // if (method == 0)
        //     playBackPanel.gameObject.SetActive(false);
    }
}
