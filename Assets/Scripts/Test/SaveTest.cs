using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    /// <summary>
    /// 存档管理器使用说明
    /// </summary>
    private void Start()
    {
        //存档管理器第一次使用前一定要先初始化（建立存档）。
        //存档初始化需要提供玩家ID，因此可以在玩家第一次进入游戏，输入玩家ID时进行初始化。
        //警告：只能使用一次初始化，否则将覆盖已有存档，使用前一定要先通过 SaveManager.PlayerSaveExists() 判断
        SaveManager.Init(playerName: "CB05");

        //至于如何判断是否第一次进入游戏，可以使用 PlayerSaveExists() 来判断存档是否存在
        //不存在：第一次进入游戏，存在：那就读取存档吧
        SaveManager.PlayerSaveExists();

        //存档读取方式：
        //使用 SaveManager.PlayerSave 来访问存档，但是！！！
        //首先得调用 SaveManager.Open() 来读取存档，然后才能通过 SaveManager.PlayerSave 来访问存档
        SaveManager.Open();

        //可以直接使用 SaveManager.PlayerSave.playerID 来访问存档数据
        Debug.Log($"【直接调用】playID: {SaveManager.PlayerSave.playerID}");

        //当然也能直接通过 SaveManager.PlayerSave.playerID 来修改数据
        //注意：这里只是演示方便，不要尝试去修改玩家ID！！！
        SaveManager.PlayerSave.playerID = "Mountain";
        Debug.Log($"【直接修改数据】playID: {SaveManager.PlayerSave.playerID}");

        //也可以引用一份存档出来操作（强调：是引用）
        SaveFile file = SaveManager.PlayerSave;
        Debug.Log($"【引用操作】file.playID: {file.playerID}");

        //关于引用：
        //引用出来的存档 file 与 SaveManager 提供的 PlayerSave 都指向同一个实例，
        //使用引用还是直接使用 PlayerSave 都可以
        //知识点：引用类型与值类型
        SaveManager.PlayerSave.playerID = "Mountain#CB05";
        Debug.Log($"【设置 SaveManager.PlayerSave，获取 file 】file.playID: {file.playerID}");
        file.playerID = "Simon#CB05";
        Debug.Log($"【设置 file，获取 SaveManager.PlayerSave 】playID: {SaveManager.PlayerSave.playerID}");

        //对存档操作完一定要保存到文件，使用 SaveManager.Close() 来保存存档
        //注意：一定要记得 Close！！！不然存档只会存于内存中，程序结束就没辣！！！
        //还有一件事！引用出来的存档数据（这里的例子是 file）在调用 SaveManager.Close() 后虽然也能访问到，但对其更改并不会保存到文件
        SaveManager.Close();

        //总结
        //先 Open，再读写，最后 Close

        #region 例子

        //SaveManager.Open();

        //读取或者设置存档里的数据等等
        // ...

        //SaveManager.Close();

        #endregion 例子

        //注意：存档读写涉及IO操作，应尽量减少 Open 跟 Close 的次数。
        //因此，尽量 Open 后统一读取或者设置存档数据，然后再 Close.

        #region 避免这样做

        //SaveManager.Open();

        //Debug.Log($"playID: {SaveManager.PlayerSave.playerID}");

        //SaveManager.Close();

        //SaveManager.Open();

        //Debug.Log($"playID: {SaveManager.PlayerSave.playerID}");

        //SaveManager.Close();

        #endregion 避免这样做

        #region 尽量这样做

        //SaveManager.Open();

        //Debug.Log($"playID: {SaveManager.PlayerSave.playerID}");
        //Debug.Log($"playID: {SaveManager.PlayerSave.playerID}");

        //SaveManager.Close();

        #endregion 尽量这样做

        //关于曲目分数的读写
        //因为 ChartID 与 chartScores 的 index 刚好对应，所以可以直接通过 (int)ChartID.L1_1 索引查找关卡 1-1 对应的曲目分数
        SaveManager.Open();
        //设置分数
        //Tip：可以通过此方法来设置分数到关卡解锁的条件，然后再读取分数看关卡是否满足解锁条件，来解锁关卡，以进行测试，正式版本不应该在除ScoreManager以外的场景使用
        SaveManager.PlayerSave.chartScores[(int)ChartID.L1_5].SetScore(Difficulty.Easy, padding: 850000, combo: 50000, ranks: 0);
        //读取分数
        int paddingScore = SaveManager.PlayerSave.chartScores[(int)ChartID.L1_5].scoreEZ.padding;
        Debug.Log($" - 关卡1-1 简单难度 - \n判定分：{paddingScore}\n连击分：{SaveManager.PlayerSave.chartScores[(int)ChartID.L1_5].scoreEZ.combo}");
        SaveManager.Close();

        //关于已获得干员数据读取，与添加干员
        SaveManager.Open();
        //查找是否已有某个干员
        SaveManager.PlayerSave.opreators.Exists(item => item.id == OpreatorID.Amiya);
        //添加新获得干员
        // SaveManager.PlayerSave.opreators.Add(new Opreator { id = OpreatorID.Spot });
        //获取特定一个干员数据
        //SaveManager.PlayerSave.opreators[(short)OpreatorID.Amiya]
        //获取特定一个干员的皮肤索引，约定皮肤索引从0开始，一个索引对应一套皮肤
        Debug.Log(SaveManager.PlayerSave.opreators[(short)OpreatorID.Amiya].skinIndex);
        //遍历所有已拥有干员
        for (int i = 0; i < SaveManager.PlayerSave.opreators.Count; i++)
        {
            //SaveManager.PlayerSave.opreators[i]
        }
        //其实就是 List<T> 的操作，不想派生自定义 List<T> 来重写方法了

        //最后
        //存档使用了序列化成JSON字符串保存的形式，因为JSON本身的特性，有较多重复字符占用空间，于是又使用了Gzip对字符串进行压缩
        //存档保存路径位于用户文件夹 AppData\LocalLow\Cookie Game Bakery\Arknights Rhythm 下
    }
}