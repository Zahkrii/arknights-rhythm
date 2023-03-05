using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public DialogUIManager dialogUIManager;

    [Header("剧情文件")]
    public TextAsset dialogDataFile;
    private string[] rows;
    private string[] cells;
    private int target;

    [Header("对话UI")]
    public GameObject dialogCanvas;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI dialogName;
    public Image dialogImgLeft;
    public Image dialogImgMid;
    public Image dialogImgRight;
    public Image dialogContainer;

    [Header("对话显示")]
    public float wordAppearInterval;
    public float imageFadeTime;
    public float imageDesaltTime;

    [Header("背景音乐")]
    public AudioSource bgm;

    [Header("背景图片")]
    public Image background;
    public GameObject clickBtn;

    [Header("历史记录")]
    public GameObject playBackPariPrefab;
    public GameObject playBackContent;
    public GameObject playBack;

    [Header("选择")]
    public GameObject optionPrefab;
    public Transform optionParent;
    public GameObject optionMask;

    [Header("跳过")]
    public Image skipPanel;
    public TextMeshProUGUI skipTitle;
    public TextMeshProUGUI skipContent;
    public TextMeshProUGUI skipTint;

    [Header("开始结束黑屏")]
    public Image blackMask;
    public GameObject warmTint;

    // 暂存数据
    private string dialogImgLeftName;
    private string dialogImgMidName;
    private string dialogImgRightName;
    // 淡化
    private bool dialogImgLeftDesalt;
    private bool dialogImgMidDesalt;
    private bool dialogImgRightDesalt;
    // 所选对话
    private string chosenText;

    // 能否加载下一段
    private bool canLoadNext;

    private bool exchange;
    private int hideLocation;

    private string curBgm;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        dialogImgLeftName = "-1";
        dialogImgMidName = "0";
        dialogImgRightName = "1";

        curBgm = "";

        warmTint.SetActive(false);

        blackMask.gameObject.SetActive(true);
        StartCoroutine(FadeBlackMask(false));

        ReadText();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        // LoadNextDialog();
        // CheckExchangeSprite();
    }

    private IEnumerator FadeBlackMask(bool fadeIn)
    {
        float speed = fadeIn ? (1f - 0f) / imageFadeTime : (0f - 1f) / imageFadeTime;
        blackMask.color = fadeIn ? new Color(0f, 0f, 0f, 0f) : new Color(0f, 0f, 0f, 1f);
        float timer = 0f;

        while (timer <= imageFadeTime)
        {
            blackMask.color = new Color(0f, 0f, 0f,
                fadeIn ? Mathf.Min(1, blackMask.color.a + speed * Time.deltaTime) : Mathf.Max(0, blackMask.color.a + speed * Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
        }

        if (!fadeIn) blackMask.gameObject.SetActive(false);
        else
        {
            // TODO: 场景跳转
        }
    }

    public void OnClickSkipBtn()
    {
        skipPanel.transform.parent.gameObject.SetActive(true);
        StartCoroutine(FadeSkipPanel(true));
    }

    public void SkipConfirm(bool close)
    {
        if (close)
        {
            StartCoroutine(FadeSkipPanel(false));
        }
        else
        {
            // 结束播放
            blackMask.gameObject.SetActive(true);
            StartCoroutine(FadeBlackMask(true));
        }
    }

    private IEnumerator FadeSkipPanel(bool fadeIn)
    {
        float speed = fadeIn ? (1f - 0f) / imageDesaltTime : (0f - 1f) / imageDesaltTime;
        float timer = 0f;

        skipPanel.color = new Color(1f, 1f, 1f, fadeIn ? 0f : 1f);
        skipTitle.color = new Color(1f, 1f, 1f, fadeIn ? 0f : 1f);
        skipContent.color = new Color(1f, 1f, 1f, fadeIn ? 0f : 1f);
        skipTint.color = new Color(1f, 1f, 1f, fadeIn ? 0f : 1f);

        while (timer <= imageDesaltTime)
        {
            skipPanel.color = new Color(1f, 1f, 1f,
            fadeIn ? Mathf.Min(1f, skipPanel.color.a + speed * Time.deltaTime) : Mathf.Max(0f, skipPanel.color.a + speed * Time.deltaTime)
            );

            skipTitle.color = new Color(1f, 1f, 1f,
            fadeIn ? Mathf.Min(1f, skipTitle.color.a + speed * Time.deltaTime) : Mathf.Max(0f, skipTitle.color.a + speed * Time.deltaTime)
            );

            skipContent.color = new Color(1f, 1f, 1f,
            fadeIn ? Mathf.Min(1f, skipContent.color.a + speed * Time.deltaTime) : Mathf.Max(0f, skipContent.color.a + speed * Time.deltaTime)
            );

            skipTint.color = new Color(1f, 1f, 1f,
            fadeIn ? Mathf.Min(1f, skipTint.color.a + speed * Time.deltaTime) : Mathf.Max(0f, skipTint.color.a + speed * Time.deltaTime)
            );

            timer += Time.deltaTime;
            yield return null;
        }

        if (!fadeIn) skipPanel.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 从csv文件中读取对话
    /// </summary>
    private void ReadText()
    {
        rows = dialogDataFile.text.Split('\n');
        ShowDialogRow(rows[target]);
    }

    private void ShowDialogRow(string row)
    {
        cells = row.Split(',');
        // 下一句话的编号
        target = int.Parse(cells[8]);
        // 更新BGM
        UpdateBGM(cells[7]);

        // 不是结束标志
        if (int.Parse(cells[1]) == 1)
        {
            // 更新UI
            UpdateText(cells[2], cells[5], int.Parse(cells[9]), cells[3], cells[10]);
            if (cells[3] != "")
                UpdateImage(cells[4], int.Parse(cells[3]));
            else
                UpdateDesaltImage();

            UpdatePlayBack(cells[2], cells[5]);
        }
        else if (int.Parse(cells[1]) == 2)
        {
            optionMask.SetActive(true);
            StartCoroutine(OptionMask(true));
        }
        else if (int.Parse(cells[1]) == 3)
        {
            // 结束播放
            blackMask.gameObject.SetActive(true);
            StartCoroutine(FadeBlackMask(true));
        }
        else if (int.Parse(cells[1]) == 4)
        {
            // 更新UI
            UpdateText(cells[2], cells[5], int.Parse(cells[9]), cells[3], cells[10]);
            HideBackground();
        }
        // 图片淡出
        else if (int.Parse(cells[1]) == 5)
        {
            if (cells[5] != "")
            {
                string[] contents = cells[5].Split("*");
                skipTitle.text = contents[0];
                skipContent.text = contents[1];
                // 如果是1-1行动前
                if (contents[0] == "1-1   行动前")
                    StartCoroutine(PlayWarmTint());
            }
            UpdateBackground(cells[6]);
        }
    }

    private IEnumerator PlayWarmTint()
    {
        warmTint.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        blackMask.gameObject.SetActive(true);

        yield return StartCoroutine(FadeBlackMask(true));

        warmTint.SetActive(false);

        yield return StartCoroutine(FadeBlackMask(false));
    }

    private void GenerateOption(int _index)
    {
        string[] optionCells = rows[_index].Split(',');

        if (int.Parse(optionCells[1]) == 2)
        {
            GameObject option = Instantiate(optionPrefab, optionParent.position, Quaternion.identity, optionParent);
            // 绑定事件
            option.GetComponentInChildren<TextMeshProUGUI>().text = optionCells[5];
            option.GetComponent<Button>().onClick.AddListener(() => OnOptionClick(int.Parse(optionCells[8]), optionCells[5]));
            GenerateOption(_index + 1);
        }
    }

    private void OnOptionClick(int _id, string content)
    {
        target = _id;
        chosenText = content;
        UpdatePlayBack("你", chosenText);
        StartCoroutine(OptionMask(false));
    }

    private IEnumerator OptionMask(bool fadeIn)
    {
        float speed = fadeIn ? (0.5f - 0f) / imageDesaltTime : (0f - 0.5f) / imageDesaltTime;
        optionMask.GetComponent<Image>().color = fadeIn ? new Color(0f, 0f, 0f, 0f) : new Color(0f, 0f, 0f, 0.5f);

        float timer = 0f;

        while (timer <= imageDesaltTime)
        {
            optionMask.GetComponent<Image>().color = new Color(0f, 0f, 0f,
                fadeIn ? Mathf.Min(0.5f, optionMask.GetComponent<Image>().color.a + speed * Time.deltaTime) : Mathf.Max(0f, optionMask.GetComponent<Image>().color.a + speed * Time.deltaTime)
            );
            timer += Time.deltaTime;
            yield return null;
        }

        if (fadeIn)
        {
            optionParent.parent.parent.gameObject.SetActive(true);
            // 跳出选项
            GenerateOption(int.Parse(cells[0]));
        }
        else
        {
            for (int i = optionParent.childCount - 1; i >= 0; i--)
            {
                Destroy(optionParent.GetChild(i).gameObject);
            }
            optionParent.parent.parent.gameObject.SetActive(false);
            optionMask.SetActive(false);
            ShowDialogRow(rows[target]);
        }
    }

    /// <summary>
    /// 更新对话框里的内容
    /// </summary>
    /// <param name="name"></param>
    /// <param name="text"></param>
    private void UpdateText(string name, string text, int spriteHide, string location, string hideLocation)
    {
        dialogName.text = name;
        // 内容逐字出现
        StartCoroutine(ShowDialogTextWordByWord(text, spriteHide, location, hideLocation));
    }

    /// <summary>
    /// 对话内容逐字出现
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private IEnumerator ShowDialogTextWordByWord(string text, int spriteHide, string location, string _hideLocation)
    {
        // 预处理text,将*PlayerName*替换为玩家名字 SaveManager.PlayerSave.playerName
        string newText = "";
        if (text.Contains("*"))
        {
            string[] texts = text.Split("*");
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i] == "PlayerName")
                {
                    // texts[i] = SaveManager.PlayerSave.playerName;
                    texts[i] = SaveManager.PlayerSave.playerID;
                }
                newText += texts[i];
            }
        }
        else newText = text;

        Stack<char> chars = new Stack<char>();
        for (int i = text.Length - 1; i >= 0; i--)
        {
            chars.Push(text[i]);
        }
        string showText = "";
        while (chars.Count != 0)
        {
            // // 如果没有完全显示就点击屏幕 则直接显示所有内容
            // if (Input.GetMouseButtonDown(0))
            // {
            //     chars.Clear();
            //     showText = text;
            // }
            // // 逐字显示
            // else
            // {
            char result;
            chars.TryPop(out result);
            showText += result;
            // }
            dialogText.text = showText;

            yield return new WaitForSeconds(wordAppearInterval);
        }

        if (spriteHide == 1)
        {
            // 淡出图片
            if (_hideLocation != "")
            {
                hideLocation = int.Parse(_hideLocation);
                exchange = true;
            }
        }
        clickBtn.SetActive(true);
        // 文字显示完成后可以加载下一段对话
        canLoadNext = true;
    }

    /// <summary>
    /// 更新立绘
    /// </summary>
    /// <param name="imageName">图片文件名</param>
    /// <param name="location">图片位置</param>
    private void UpdateImage(string imageName, int location)
    {
        // 获取对应位置的Image组件
        Image targetImage = location == -1 ? dialogImgLeft : (location == 0 ? dialogImgMid : dialogImgRight);
        string usedImageName = location == -1 ? dialogImgLeftName : (location == 0 ? dialogImgMidName : dialogImgRightName);

        // 此位置第一次读入图片或者更换了图片
        if (targetImage.sprite == null || usedImageName != imageName)
        {
            // 更新立绘记录
            dialogImgLeftName = usedImageName == dialogImgLeftName ? imageName : dialogImgLeftName;
            dialogImgMidName = usedImageName == dialogImgMidName ? imageName : dialogImgMidName;
            dialogImgRightName = usedImageName == dialogImgRightName ? imageName : dialogImgRightName;
            // 获取该位置立绘并且更换
            Sprite targetSprite = Resources.Load<Sprite>("Dialog/人物立绘/" + imageName);

            if (targetImage.sprite == null)
            {
                targetImage.sprite = targetSprite;
                // 该位置立绘淡入
                StartCoroutine(Fade(targetImage, true));
            }
            else if (usedImageName != imageName)
            {
                // 直接切换立绘
                StartCoroutine(ChangeSprite(targetImage, targetSprite));
            }
        }

        if (targetImage == dialogImgLeft)
        {
            if (targetImage.sprite != null && usedImageName == imageName && dialogImgLeftDesalt)
                StartCoroutine(Desalt(targetImage, false));
            if (dialogImgRight.sprite != null && !dialogImgRightDesalt)
                StartCoroutine(Desalt(dialogImgRight, true));
        }
        else if (targetImage == dialogImgRight)
        {
            if (targetImage.sprite != null && usedImageName == imageName && dialogImgRightDesalt)
                StartCoroutine(Desalt(targetImage, false));
            if (dialogImgLeft.sprite != null && !dialogImgLeftDesalt)
                StartCoroutine(Desalt(dialogImgLeft, true));
        }
        else if (targetImage == dialogImgMid)
        {
            if (targetImage.sprite != null && usedImageName == imageName && dialogImgMidDesalt)
                StartCoroutine(Desalt(targetImage, false));
        }
    }

    private void UpdateDesaltImage()
    {
        if (!dialogImgLeftDesalt && dialogImgLeft.sprite != null)
            StartCoroutine(Desalt(dialogImgLeft, true));
        if (!dialogImgMidDesalt && dialogImgMid.sprite != null)
            StartCoroutine(Desalt(dialogImgMid, true));
        if (!dialogImgRightDesalt && dialogImgRight.sprite != null)
            StartCoroutine(Desalt(dialogImgRight, true));
    }

    /// <summary>
    /// 切换BGM
    /// </summary>
    /// <param name="bgmName">bgm文件名</param>
    private void UpdateBGM(string bgmName)
    {
        // 如果bgmName不为空
        if (bgmName != "")
        {
            // 获取BGM文件
            AudioClip targetAudioClip = Resources.Load<AudioClip>("Dialog/剧情BGM/" + bgmName);

            if (curBgm == "")
            {
                // 先暂停BGM
                bgm.Stop();
                // 更换文件
                bgm.clip = targetAudioClip;
                // 播放BGM
                bgm.Play();
            }
            else
            {
                if (bgmName != curBgm)
                {
                    StartCoroutine(ChangeBgm(targetAudioClip));
                }
            }
            curBgm = bgmName;
        }
    }

    private IEnumerator ChangeBgm(AudioClip targetClip)
    {
        float curVolume = bgm.volume;
        float speed = (0 - curVolume) / imageFadeTime;
        float timer = 0f;

        while (timer <= imageFadeTime)
        {
            bgm.volume = Mathf.Max(0f, bgm.volume + speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        bgm.clip = targetClip;
        timer = 0f;
        bgm.Play();

        while (timer <= imageFadeTime)
        {
            bgm.volume = Mathf.Min(curVolume, bgm.volume - speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void HideBackground()
    {
        // 先淡出还在场的立绘
        // 然后淡出对话框
        // 后淡出背景图片
        List<Image> images = new List<Image>();
        images.Add(dialogImgLeft);
        images.Add(dialogImgMid);
        images.Add(dialogImgRight);
        dialogText.text = "";
        dialogName.text = "";
        StartCoroutine(BGFade(images, dialogContainer, background, false));
    }

    private void UpdateBackground(string bgName)
    {
        Sprite targetBg = Resources.Load<Sprite>("Dialog/剧情对话背景/" + bgName);
        background.sprite = targetBg;

        List<Image> images = new List<Image>();
        images.Add(dialogImgLeft);
        images.Add(dialogImgMid);
        images.Add(dialogImgRight);
        dialogText.text = "";
        dialogName.text = "";
        StartCoroutine(BGFade(images, dialogContainer, background, true));
    }

    /// <summary>
    /// 加载下一段对话
    /// </summary>
    public void LoadNextDialog()
    {
        // 如果可以加载下一段对话
        if (canLoadNext && !dialogUIManager.isOpen)
        {
            if (exchange)
            {
                StartCoroutine(ExchangeSprite());
                exchange = false;
            }
            else
            {
                // 如果点击屏幕
                // if (Input.GetMouseButtonDown(0))
                // {
                canLoadNext = false;
                ShowDialogRow(rows[target]);
                // }
            }
        }
    }

    private IEnumerator ExchangeSprite()
    {
        switch (hideLocation)
        {
            case -1:
                yield return Fade(dialogImgLeft, false);
                dialogImgLeft.sprite = null;
                dialogImgLeftDesalt = false;
                break;
            case 0:
                yield return Fade(dialogImgMid, false);
                dialogImgMid.sprite = null;
                dialogImgMidDesalt = false;
                break;
            case 1:
                yield return Fade(dialogImgRight, false);
                dialogImgRight.sprite = null;
                dialogImgRightDesalt = false;
                break;
            case 2:
                StartCoroutine(Fade(dialogImgLeft, false));
                StartCoroutine(Fade(dialogImgRight, false));
                yield return new WaitForSeconds(imageFadeTime);
                dialogImgRight.sprite = null;
                dialogImgLeft.sprite = null;
                dialogImgLeftDesalt = false;
                dialogImgRightDesalt = false;
                break;
            default:
                break;
        }
        hideLocation = 9999;
        canLoadNext = false;
        ShowDialogRow(rows[target]);
    }

    /// <summary>
    /// 更新历史记录
    /// </summary>
    private void UpdatePlayBack(string name, string text)
    {
        GameObject playBackPair = Instantiate(playBackPariPrefab, playBackContent.transform.position, Quaternion.identity, playBackContent.transform);
        // 生成对话
        playBackPair.transform.Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().text = name;
        playBackPair.transform.Find("Dialog Speaker").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0f);
        playBackPair.transform.Find("Dialog Content").GetComponent<TextMeshProUGUI>().text = text;
        playBackPair.transform.Find("Dialog Content").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0f);
    }

    /// <summary>
    /// 控制图片淡入淡出
    /// </summary>
    /// <param name="fadeIn">true-淡入 false-淡出</param>
    /// <returns></returns>
    private IEnumerator Fade(Image image, bool fadeIn)
    {
        float speed = fadeIn ? (1f - 0f) / imageFadeTime : (0f - 1f) / imageFadeTime;
        image.color = fadeIn ? new Color(1f, 1f, 1f, 0f) : new Color(image.color.r, image.color.g, image.color.b, 1f);
        float timer = 0f;

        while (timer <= imageFadeTime)
        {
            image.color = new Color(fadeIn ? 1f : image.color.r, fadeIn ? 1f : image.color.g, fadeIn ? 1f : image.color.b,
                fadeIn ? Mathf.Min(1, image.color.a + speed * Time.deltaTime) : Mathf.Max(0, image.color.a + speed * Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// 控制背景淡入淡出
    /// </summary>
    /// <param name="fadeIn">true-淡入 false-淡出</param>
    /// <returns></returns>
    private IEnumerator BGFade(List<Image> character, Image dialog, Image bg, bool fadeIn)
    {
        clickBtn.SetActive(false);
        if (!fadeIn)
        {
            if (character.Count != 0)
            {
                foreach (var image in character)
                {
                    if (image.sprite != null)
                        StartCoroutine(Fade(image, false));
                }
                yield return new WaitForSeconds(imageFadeTime);
            }

            yield return Fade(dialog, false);

            yield return Fade(bg, false);
            ShowDialogRow(rows[target]);
        }
        else
        {
            if (character.Count != 0)
            {
                // 透明化立绘
                foreach (var image in character)
                {
                    image.sprite = null;
                    image.color = new Color(1f, 1f, 1f, 0f);
                }
            }

            yield return Fade(bg, true);

            yield return Fade(dialog, true);
            clickBtn.SetActive(true);
            // 文字显示完成后可以加载下一段对话
            canLoadNext = true;
        }
    }

    /// <summary>
    /// 控制图片淡入淡出
    /// </summary>
    /// <param name="fadeIn">true-淡化 false-恢复</param>
    /// <returns></returns>
    private IEnumerator Desalt(Image image, bool desaltIn)
    {
        float speed = desaltIn ? (0.5f - 1f) / imageDesaltTime : (1f - 0.5f) / imageDesaltTime;
        image.color = desaltIn ? new Color(image.color.r, image.color.g, image.color.b, image.color.a) : new Color(0.5f, 0.5f, 0.5f, image.color.a);
        float timer = 0f;

        while (timer <= imageDesaltTime)
        {
            image.color = new Color(
                desaltIn ? Mathf.Max(0.5f, image.color.r + speed * Time.deltaTime) : Mathf.Min(1, image.color.r + speed * Time.deltaTime),
                desaltIn ? Mathf.Max(0.5f, image.color.g + speed * Time.deltaTime) : Mathf.Min(1, image.color.g + speed * Time.deltaTime),
                desaltIn ? Mathf.Max(0.5f, image.color.b + speed * Time.deltaTime) : Mathf.Min(1, image.color.b + speed * Time.deltaTime),
                1f);
            timer += Time.deltaTime;
            yield return null;
        }

        if (desaltIn)
        {
            if (image == dialogImgLeft) dialogImgLeftDesalt = true;
            else if (image == dialogImgMid) dialogImgMidDesalt = true;
            else if (image == dialogImgRight) dialogImgRightDesalt = true;
        }
        else
        {
            if (image == dialogImgLeft) dialogImgLeftDesalt = false;
            else if (image == dialogImgMid) dialogImgMidDesalt = false;
            else if (image == dialogImgRight) dialogImgRightDesalt = false;
        }
    }

    /// <summary>
    /// 控制图片淡入淡出
    /// </summary>
    /// <param name="fadeIn">true-淡化 false-恢复</param>
    /// <returns></returns>
    private IEnumerator ChangeSprite(Image image, Sprite targetSprite)
    {
        float speed = (0.2f - 1f) / (imageDesaltTime * 4f);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        float timer = 0f;

        while (timer <= 4 * imageDesaltTime)
        {
            if (timer >= 0 && timer <= 2 * imageDesaltTime)
            {
                // image.color = new Color(1f, 1f, 1f,
                //     Mathf.Max(0.5f, image.color.a + speed * Time.deltaTime));

                image.color = new Color(
                    Mathf.Max(0.2f, image.color.r + speed * Time.deltaTime),
                    Mathf.Max(0.2f, image.color.g + speed * Time.deltaTime),
                    Mathf.Max(0.2f, image.color.b + speed * Time.deltaTime),
                    1f);
            }
            else if (timer > 2 * imageDesaltTime && timer <= 4 * imageDesaltTime)
            {
                image.sprite = targetSprite;
                // image.color = new Color(1f, 1f, 1f,
                //     Mathf.Min(1f, image.color.a - speed * Time.deltaTime));
                image.color = new Color(
                    Mathf.Min(1f, image.color.r - speed * Time.deltaTime),
                    Mathf.Min(1f, image.color.g - speed * Time.deltaTime),
                    Mathf.Min(1f, image.color.b - speed * Time.deltaTime),
                    1f);
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
