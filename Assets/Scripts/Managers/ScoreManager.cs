using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    //相对分数
    private float _relativeScore = 0;

    //总note数
    private float _totalNotes;

    private Difficulty _difficulty;
    private short _level;

    private ChartID _chartID;

    //判定分
    private float _paddingScore = 0;

    //连击分
    private float _comboScore = 0;

    //连击数
    private int _comboCount = 0;

    //最高combo
    private int _bestCombo = 0;

    private short _perfectCount = 0;
    private short _goodCount = 0;
    private short _missCount = 0;

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

    public void Init(ChartID id, Difficulty diff, short level, int totalNotes)
    {
        _chartID = id;
        _difficulty = diff;
        _level = level;
        _totalNotes = totalNotes;
    }

    public void ScoreTap(float hitTime)
    {
        hitTime = Mathf.Abs(hitTime);
        if (hitTime <= 0.085f)
        {
            _relativeScore += 1;
            _perfectCount++;
        }
        else
        {
            _relativeScore += 0.6f;
            _goodCount++;
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

    public void ScoreHold(bool isPerfect)
    {
        if (isPerfect)
        {
            _relativeScore += 1;
            _perfectCount++;
        }
        else
        {
            _relativeScore += 0.6f;
            _goodCount++;
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
        _perfectCount++;
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
        _missCount++;
        DOTween.To((value) => { comboTransform.localScale = new Vector3(value, value); }, 2f, 1, 0.1f);
        comboText.text = _comboCount.ToString();
    }

    /// <summary>
    /// 统计Ranks
    /// </summary>
    /// <returns></returns>
    private float GetRanks()
    {
        if (_paddingScore < 60)
            return 0;
        return Mathf.Pow((100 * _paddingScore - 60) / 40, 2) * _level;
    }

    /// <summary>
    /// 计算技术等级
    /// </summary>
    private void CalculateLevel()
    {
        SaveManager.Open();

        SaveManager.PlayerSave.chartScores[(int)_chartID].SetScore(_difficulty, _paddingScore, _comboScore, GetRanks());

        List<float> rks = new List<float>();
        foreach (ChartScore chart in SaveManager.PlayerSave.chartScores)
        {
            if (chart.scoreEZ.ranks > 0)
                rks.Add(chart.scoreEZ.ranks);
            if (chart.scoreNM.ranks > 0)
                rks.Add(chart.scoreNM.ranks);
            if (chart.scoreHD.ranks > 0)
                rks.Add(chart.scoreHD.ranks);
            if (chart.scoreEX.ranks > 0)
                rks.Add(chart.scoreEX.ranks);
        }
        float level = 0;
        for (int i = 0; i < 10; i++)
        {
            level += rks.Max();
            rks.Remove(rks.Max());
        }
        SaveManager.PlayerSave.level = level / 100 * 120;

        SaveManager.Close();
    }
}