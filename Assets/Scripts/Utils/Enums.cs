/// <summary>
/// 曲目难度
/// </summary>
public enum Difficulty
{
    Easy = 0,
    Normal,
    Hard,
    Insane
}

/// <summary>
/// 曲目ID，后续增加请在这里增加ID
/// </summary>
public enum ChartID
{
    L1_1 = 0,
    L1_2,
    L1_3,
    L1_4,
    L1_5,
    L1_6,
    SE_1,
    SE_2,
    SE_3,
    SE_4,
    SE_5,
    EP_1
}

/// <summary>
/// 干员ID，后续增加请在这里增加ID
/// </summary>
public enum OpreatorID
{
    Amiya = 0,
    Logos,
    Centaurea,
    Mountain,
    Talulah,
    OceanCat,
    NearlTheRadiantKnight,
    Rosmontis,
    FrostNova,
    Spot,
}

/// <summary>
/// 干员8个语音的类型
/// </summary>
public enum CharacterAudioType
{
    Login,
    ChatFirst,
    ChatSecond,
    ChatThird,
    GameFourStar,
    GameThreeStar,
    GameTwoStar,
    GameOneStar,
}