using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private AudioSource audioSource;
    private Chart data;

    [SerializeField] private GameObject tapPrefab;
    [SerializeField] private GameObject dragPrefab;
    [SerializeField] private Transform notesParent;
    [SerializeField] private Slider progressBar;

    //��ʱ��
    private float Timer = 0;

    //note����
    private int index = 0;

    private bool isGameStart = false;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        audioSource = GetComponent<AudioSource>();
        progressBar.value = 0;
        DataManager.Instance.LoadChart("���ƻ�", (Chart chart) =>
        {
            data = chart;
        });
        audioSource.Stop();
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        if (isGameStart && (index < data.notes.Count))
        {
            Timer += Time.deltaTime;
            if (Timer > (data.notes[index].time - 10f / 5))
            {
                if (data.notes[index].type == 0)
                {
                    GameObject go = Instantiate(
                        tapPrefab,
                        new Vector3(data.notes[index].pos, 1.001f, 5.5f),
                        Quaternion.Euler(new Vector3(90, 0, 0)),
                        notesParent);
                    Vector3 scale = go.transform.localScale;
                    scale.x *= data.notes[index].size;
                    go.transform.localScale = scale;
                    go.GetComponent<TapScript>().Size = data.notes[index].size;
                }

                if (data.notes[index].type == 1)
                {
                    GameObject go = Instantiate(
                        dragPrefab,
                        new Vector3(data.notes[index].pos, 1.001f, 5.5f),
                        Quaternion.Euler(new Vector3(90, 0, 0)),
                        notesParent);
                    Vector3 scale = go.transform.localScale;
                    scale.x *= data.notes[index].size;
                    go.transform.localScale = scale;
                    go.GetComponent<DragScript>().Size = data.notes[index].size;
                }

                index++;
            }
        }
        progressBar.value = Mathf.Clamp((audioSource.time) / audioSource.clip.length, 0, 1);
    }

    private void GameOver()
    {
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        ScoreManager.Instance.TotalNotes = data.notes.Count;
        isGameStart = true;
        audioSource.Play();
    }
}