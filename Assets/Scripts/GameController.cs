using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioSource audioSource;
    private Chart data;

    public GameObject tapPrefab;
    public GameObject dragPrefab;
    public Transform notesParent;

    //计时器
    private float Timer = 0;

    //note索引
    private int index = 0;

    private bool isGameStart = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DataManager.Instance.LoadTestChart();
        DataManager.Instance.charts.TryGetValue("阴云火花", out data);
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
                    Instantiate(tapPrefab, new Vector3(data.notes[index].pos, 1.001f, 5.5f), Quaternion.Euler(new Vector3(90, 0, 0)), notesParent);
                if (data.notes[index].type == 1)
                    Instantiate(dragPrefab, new Vector3(data.notes[index].pos, 1.001f, 5.5f), Quaternion.Euler(new Vector3(90, 0, 0)), notesParent);
                index++;
            }
        }
    }

    private void GameOver()
    {
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        isGameStart = true;
        audioSource.Play();
    }
}