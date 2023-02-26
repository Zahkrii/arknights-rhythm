using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectSceneManager : MonoBehaviour
{
    public void OperatorSceneManage()
    {
        SceneManager.LoadScene("CharacterList");
        //SceneManager.LoadScene("1");
    }
    public void TuorialSceneManage()
    {
        //SceneManager.LoadScene("")£»
    }
    public void OptionsSceneManage()
    {
        //SceneManager.LoadScene("")£»
    }
    public void OtherSceneManage()
    {
        //SceneManager.LoadScene("")£»
    }
}
