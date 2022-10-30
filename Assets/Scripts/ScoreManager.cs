using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    //相对分数
    private float _relativeScore = 0;

    //总note数
    private float _totalNotes = 0;

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

        scoreText.text = "0";
        comboText.text = "0";
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
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //更新UI
        scoreText.text = Mathf.CeilToInt(Mathf.Clamp(_paddingScore + _comboScore, 0, 1000000)).ToString();
        comboText.text = _comboCount.ToString();
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
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //更新UI
        scoreText.text = Mathf.CeilToInt(Mathf.Clamp(_paddingScore + _comboScore, 0, 1000000)).ToString();
        comboText.text = _comboCount.ToString();
    }

    public void MissNote()
    {
        _comboCount = 0;
        comboText.text = _comboCount.ToString();
    }
}