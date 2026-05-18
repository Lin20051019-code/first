using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 游戏主控制器（单例）
/// 负责：介绍框显示 → 选球阶段 → 游戏开始
/// </summary>
public class GameManager : MonoBehaviour
{
    // ═══════════════════════════════════════
    // 单例
    // ═══════════════════════════════════════
    public static GameManager Instance { get; private set; }

    // ═══════════════════════════════════════
    // Inspector 引用
    // ═══════════════════════════════════════
    [Header("UI 引用")]
    public GameObject introPanel;       // 介绍面板
    public Button confirmButton;    // 确定按钮
    public Button startButton;      // 开始按钮
    public GameObject startButtonObj;   // 开始按钮的 GameObject（用于显示/隐藏）

    [Header("小球设置")]
    public GameObject ballPrefab;       // 蓝色小球预制体
    public int gridSize = 5;    // 5×5 方阵
    public float spacing = 1.2f; // 小球间距

    // ═══════════════════════════════════════
    // 内部状态
    // ═══════════════════════════════════════
    public bool IsSelectingPhase { get; private set; } = false;

    private BallSelector _selectedBall = null;  // 当前选中的小球

    // ═══════════════════════════════════════
    // 生命周期
    // ═══════════════════════════════════════
    void Awake()
    {
        // 单例初始化
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        // 初始状态：显示介绍框，隐藏开始按钮
        introPanel.SetActive(true);
        startButtonObj.SetActive(false);

        // 绑定按钮事件
        confirmButton.onClick.AddListener(OnConfirmClicked);
        startButton.onClick.AddListener(OnStartClicked);

        // 生成小球（先隐藏）
        SpawnBalls();
        SetBallsVisible(false);
    }

    // ═══════════════════════════════════════
    // 生成25个小球
    // ═══════════════════════════════════════
    void SpawnBalls()
    {
        // 计算方阵左下角起点，让方阵居中在 (-x, -y)
        float startX = -(gridSize - 1) * spacing / 2f;
        float startY = -(gridSize - 1) * spacing / 2f;

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                Vector3 pos = new Vector3(
                    startX + col * spacing,
                    startY + row * spacing,
                    0f
                );
                GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);
                ball.name = $"Ball_{row}_{col}";
            }
        }
    }

    void SetBallsVisible(bool visible)
    {
        // 找到所有小球并设置可见性
        BallSelector[] balls = FindObjectsByType<BallSelector>(FindObjectsSortMode.None);
        foreach (var b in balls)
            b.gameObject.SetActive(visible);
    }

    // ═══════════════════════════════════════
    // 按钮事件
    // ═══════════════════════════════════════

    /// <summary>点击"确定"后：关闭介绍框，显示小球，进入选球阶段</summary>
    void OnConfirmClicked()
    {
        introPanel.SetActive(false);
        SetBallsVisible(true);
        IsSelectingPhase = true;

        Debug.Log("请点击一个蓝色小球选择你的角色！");
    }

    /// <summary>被 BallSelector 调用，记录玩家选择的球</summary>
    public void SelectPlayerBall(BallSelector ball)
    {
        // 取消上一个选中状态
        if (_selectedBall != null)
            _selectedBall.SetSelected(false);

        // 设置新选中
        _selectedBall = ball;
        _selectedBall.SetSelected(true);

        // 显示开始按钮
        startButtonObj.SetActive(true);

        Debug.Log($"选中了：{ball.gameObject.name}");
    }

    /// <summary>点击"开始"后：游戏正式开始</summary>
    void OnStartClicked()
    {
        if (_selectedBall == null)
        {
            Debug.LogWarning("请先选择一个小球！");
            return;
        }

        IsSelectingPhase = false;
        startButtonObj.SetActive(false);

        Debug.Log($"游戏开始！玩家球：{_selectedBall.gameObject.name}");
        // TODO：下一步在这里启动教官AI和计时逻辑
    }
}
