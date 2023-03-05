using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharListBtnsManager : MonoBehaviour
{
    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
