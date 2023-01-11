using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //��������
    private Chart data;

    [SerializeField] private GameObject tapPrefab;
    [SerializeField] private GameObject dragPrefab;
    [SerializeField] private GameObject holdPrefab;
    [SerializeField] private Transform notesParent;

    //������
    [SerializeField] private Slider progressBar;

    //��ʱ��
    private float Timer = 0;

    //note����
    private int index = 0;

    private bool isGameStart = false;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        //ChartManager.Instance.LoadChart("���ƻ�", (Chart chart) =>
        //{
        //    data = chart;
        //});
        data = ChartManager.Instance.LoadChart("SE4");
        progressBar.value = 0;
    }

    private void Start()
    {
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        if (isGameStart && (index < data.notes.Count))
        {
            Timer += Time.deltaTime;
            if (Timer > (data.notes[index].time - 10f / (5 * 1)))//����λ�õ��ж���·��10����λ���ٶ�Ĭ��5��λÿ�룬���������ʱ��
            {
                if (data.notes[index].type == 0)//tap
                {
                    GameObject go = Instantiate(
                        tapPrefab,
                        new Vector3(data.notes[index].pos, 1.001f, 5.5f),
                        Quaternion.Euler(new Vector3(90, 0, 0)),
                        notesParent);
                }

                if (data.notes[index].type == 1)//drag
                {
                    GameObject go = Instantiate(
                        dragPrefab,
                        new Vector3(data.notes[index].pos, 1.001f, 5.5f),
                        Quaternion.Euler(new Vector3(90, 0, 0)),
                        notesParent);
                }

                if (data.notes[index].type == 2)//hold
                {
                    GameObject go = Instantiate(
                        holdPrefab,
                        new Vector3(data.notes[index].pos, 1.001f, 5.5f),
                        Quaternion.Euler(new Vector3(90, 0, 0)),
                        notesParent);
                    go.GetComponent<HoldScript>().holdTime = data.notes[index].holdTime;
                }

                index++;
            }
            progressBar.value = AudioManager.Instance.GetMusicPlayProgress();
        }
    }

    private void GameOver()
    {
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        ScoreManager.Instance.Init(data.id, data.difficulty, data.level, data.count);
        isGameStart = true;
        AudioManager.Instance.PlayChartMusic("SE4");
    }
}