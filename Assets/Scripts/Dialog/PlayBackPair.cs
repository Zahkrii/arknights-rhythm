using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBackPair : MonoBehaviour
{
    public RectTransform content;

    private RectTransform pair;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        pair = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        pair.sizeDelta = new Vector2(pair.rect.width, content.rect.height + 15);
    }
}
