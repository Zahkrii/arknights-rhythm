using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioSource audioSource;
    private Chart data;

    public GameObject tapPrefab;
    public Transform notesParent;

    //¼ÆÊ±Æ÷
    private float Timer = 0;

    //noteË÷Òý
    private int index = 0;

    private bool isGameStart = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        LoadChart();
        audioSource.Stop();
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        if (isGameStart)
        {
            Timer += Time.deltaTime;
            if (Timer > data.notes[index].time)
            {
                Instantiate(tapPrefab, new Vector3(data.notes[index].pos, 1.001f, -4.5f), Quaternion.identity, notesParent);
                index++;
            }
        }
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        isGameStart = true;
        audioSource.Play();
    }

    private void LoadChart()
    {
        string[] filePaths = Directory.GetFiles($"{Application.dataPath}/Charts", "*.json", SearchOption.AllDirectories);
        string json = File.ReadAllText(filePaths[0]);
        data = JsonUtility.FromJson<Chart>(json);
    }
}