using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private Toggle panelToggle;

    [Header("Debug")]
    [SerializeField] private TMP_Text bestComboText;

    [SerializeField] private TMP_Text comboScoreText;
    [SerializeField] private TMP_Text paddingScoreText;

    private RectTransform rectTransform;

    public static DebugPanel Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        panelToggle.isOn = false;
    }

    public void TogglePanel()
    {
        if (panelToggle.isOn)
        {
            rectTransform.DOAnchorPosX(-20, 0.5f);
        }
        else
        {
            rectTransform.DOAnchorPosX(400, 0.5f);
        }
    }

    public void LogBestCombo(int bestCombo)
    {
        bestComboText.text = $"Best Combo: {bestCombo}";
    }

    public void LogPaddingScore(float paddingScore)
    {
        paddingScoreText.text = $"Padding Score: {Mathf.CeilToInt(paddingScore)}";
    }

    public void LogComboScore(float comboScore)
    {
        comboScoreText.text = $"Padding Score: {Mathf.CeilToInt(comboScore)}";
    }
}