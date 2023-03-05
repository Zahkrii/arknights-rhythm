using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMainCanvasAnimatorEvent : MonoBehaviour
{
    [SerializeField] private Transform characterListCanvas;
    [SerializeField] private Transform characterDetailCanvas;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        characterDetailCanvas.gameObject.SetActive(false);
    }

    public void AfterCloseCharacterDetailAnimator()
    {
        characterDetailCanvas.gameObject.SetActive(false);
    }
}
