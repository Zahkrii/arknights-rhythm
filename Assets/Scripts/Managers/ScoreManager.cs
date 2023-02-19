using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    //相对分数
    private float _relativeScore = 0;

    //总note数
    private float _totalNotes = 0;

    //提供总note数的修改与获取
    public float TotalNotes
    { get { return _totalNotes; } set { _totalNotes = value; } }

    //判定分
    private float _paddingScore = 0;

    //连击分
    private float _comboScore = 0;

    //连击数
    private int _comboCount = 0;

    //最高combo
    private int _bestCombo = 0;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;

    private RectTransform scoreTransform;
    private RectTransform comboTransform;

    protected override void OnAwake()
    {
        //初始化
        scoreText.text = "0";
        comboText.text = "0";
        scoreTransform = scoreText.gameObject.GetComponent<RectTransform>();
        comboTransform = comboText.gameObject.GetComponent<RectTransform>();
    }

    public void ScoreTap(float hitTime)
    {
        hitTime = Mathf.Abs(hitTime);
        if (hitTime <= 0.085)
        {
            _relativeScore += 1;
        }
        else
        {
            _relativeScore += 0.6f;
        }
        //计算判定分
        _paddingScore = (_relativeScore / _totalNotes) * 900000;
        //计算连击分
        if (_comboCount == _bestCombo)
        {
            _bestCombo++;
            //Debug
            DebugPanel.Instance.LogBestCombo(_bestCombo);
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //更新UI
        DOTween.To((value) => { scoreTransform.localScale = new Vector3(value, value); }, 1.5f, 1, 0.1f);
        scoreText.text = Mathf.CeilToInt(Mathf.Clamp(_paddingScore + _comboScore, 0, 1000000)).ToString();
        DOTween.To((value) => { comboTransform.localScale = new Vector3(value, value); }, 1.5f, 1, 0.1f);
        comboText.text = _comboCount.ToString();

        //Debug
        DebugPanel.Instance.LogPaddingScore(_paddingScore);
        DebugPanel.Instance.LogComboScore(_comboScore);
    }

    public void ScoreDrag()
    {
        _relativeScore += 1;
        //计算判定分
        _paddingScore = (_relativeScore / _totalNotes) * 900000;
        //计算连击分
        if (_comboCount == _bestCombo)
        {
            _bestCombo++;
            //Debug
            DebugPanel.Instance.LogBestCombo(_bestCombo);
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //更新UI
        DOTween.To((value) => { scoreTransform.localScale = new Vector3(value, value); }, 1.5f, 1, 0.1f);
        scoreText.text = Mathf.CeilToInt(Mathf.Clamp(_paddingScore + _comboScore, 0, 1000000)).ToString();
        DOTween.To((value) => { comboTransform.localScale = new Vector3(value, value); }, 2f, 1, 0.1f);
        comboText.text = _comboCount.ToString();

        //Debug
        DebugPanel.Instance.LogPaddingScore(_paddingScore);
        DebugPanel.Instance.LogComboScore(_comboScore);
    }

    public void MissNote()
    {
        _comboCount = 0;
        DOTween.To((value) => { comboTransform.localScale = new Vector3(value, value); }, 2f, 1, 0.1f);
        comboText.text = _comboCount.ToString();
    }
}