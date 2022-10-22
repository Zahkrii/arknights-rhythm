using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Chart
{
    public int speed;
    public List<Note> notes;
    public List<Link> links;
}

[Serializable]
public class Note
{
    public int id;
    public int type;
    public float pos;
    public float size;
    public float time;
}

[Serializable]
public class Link
{
    public List<LinkNote> notes;
}

[Serializable]
public class LinkNote
{
    public int id;
}

public class DataManager : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Update()
    {
    }
}