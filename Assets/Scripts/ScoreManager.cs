using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    //��Է���
    private float _relativeScore = 0;

    //��note��
    private float _totalNotes = 0;

    public float TotalNotes
    { get { return _totalNotes; } set { _totalNotes = value; } }

    //�ж���
    private float _paddingScore = 0;

    //������
    private float _comboScore = 0;

    //������
    private int _comboCount = 0;

    //���combo
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
        //�����ж���
        _paddingScore = (_relativeScore / _totalNotes) * 900000;
        //����������
        if (_comboCount == _bestCombo)
        {
            _bestCombo++;
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //����UI
        scoreText.text = Mathf.CeilToInt(Mathf.Clamp(_paddingScore + _comboScore, 0, 1000000)).ToString();
        comboText.text = _comboCount.ToString();
    }

    public void ScoreDrag()
    {
        _relativeScore += 1;
        //�����ж���
        _paddingScore = (_relativeScore / _totalNotes) * 900000;
        //����������
        if (_comboCount == _bestCombo)
        {
            _bestCombo++;
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //����UI
        scoreText.text = Mathf.CeilToInt(Mathf.Clamp(_paddingScore + _comboScore, 0, 1000000)).ToString();
        comboText.text = _comboCount.ToString();
    }

    public void MissNote()
    {
        _comboCount = 0;
        comboText.text = _comboCount.ToString();
    }
}