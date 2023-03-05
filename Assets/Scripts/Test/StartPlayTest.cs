using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlayTest : MonoBehaviour
{
    //进入关卡开始游戏的例子
    private void StartPlay()
    {
        //先将选择的曲目代号存入 SettingsManager.SelectedChartID
        SettingsManager.SelectedChartID = ChartID.SE_4;
        //设置难度
        SettingsManager.SelectedDifficulty = Difficulty.Hard;
        //设置谱面流速
        SettingsManager.ChartSpeed = 5;
        //设置谱面延时
        SettingsManager.ChartDelay = 0;
        //然后切换场景
        SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        //这样就能通过读取 SettingsManager.ChartSelected 来确认选择了哪条曲目，以及玩家开始游戏前对谱面流速的设置
        //另一种实现方式：使用单例类或静态类来传参
    }
}