using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public Button startButton;

    [Header("游戏状态")]
    public bool gameStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 初始时隐藏开始按钮？或者保持显示
        if (startButton != null)
            startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void OnStartButtonClicked()
    {
        if (PlayerSelector.currentPlayer == null)
        {
            Debug.LogWarning("请先选择一个士兵作为自己！");
            return;
        }

        gameStarted = true;
        startButton.gameObject.SetActive(false);   // 点击后隐藏按钮

        Debug.Log("游戏开始！教官即将开始巡逻");
    }

    // 提供给其他脚本查询是否可以开始巡逻
    public bool IsGameStarted()
    {
        return gameStarted;
    }
}