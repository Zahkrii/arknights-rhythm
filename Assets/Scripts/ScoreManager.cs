using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    //��Է���
    private float _relativeScore = 0;

    //��note��
    private float _totalNotes = 0;

    //�ṩ��note�����޸����ȡ
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

    private RectTransform scoreTransform;
    private RectTransform comboTransform;

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

        //��ʼ��
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
        //�����ж���
        _paddingScore = (_relativeScore / _totalNotes) * 900000;
        //����������
        if (_comboCount == _bestCombo)
        {
            _bestCombo++;
            //Debug
            DebugPanel.Instance.LogBestCombo(_bestCombo);
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //����UI
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
        //�����ж���
        _paddingScore = (_relativeScore / _totalNotes) * 900000;
        //����������
        if (_comboCount == _bestCombo)
        {
            _bestCombo++;
            //Debug
            DebugPanel.Instance.LogBestCombo(_bestCombo);
            _comboScore = (_bestCombo / _totalNotes) * 100000;
        }
        _comboCount++;

        //����UI
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