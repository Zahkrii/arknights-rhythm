using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public string SceneName;
    public void ChangePlayingScene()
    {
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneName);
    }
}
