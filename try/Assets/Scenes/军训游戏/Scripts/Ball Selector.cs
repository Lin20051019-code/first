using UnityEngine;

/// <summary>
/// 挂在每个蓝色小球上，处理点击选择逻辑
/// </summary>
public class BallSelector : MonoBehaviour
{
    [Header("外观设置")]
    public Color normalColor = new Color(0.27f, 0.51f, 0.78f); // 蓝色
    public Color selectedColor = new Color(0.18f, 0.75f, 0.18f); // 绿色（选中）
    public Color hoverColor = new Color(0.5f, 0.75f, 1.0f);  // 浅蓝（悬停）

    private SpriteRenderer _sr;
    private bool _isSelected = false;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = normalColor;
    }

    // ── 鼠标悬停 ──────────────────────────────
    void OnMouseEnter()
    {
        if (!GameManager.Instance.IsSelectingPhase) return;
        if (!_isSelected)
            _sr.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (!GameManager.Instance.IsSelectingPhase) return;
        if (!_isSelected)
            _sr.color = normalColor;
    }

    // ── 点击选择 ──────────────────────────────
    void OnMouseDown()
    {
        if (!GameManager.Instance.IsSelectingPhase) return;

        // 通知 GameManager 选中了这个球
        GameManager.Instance.SelectPlayerBall(this);
    }

    /// <summary>由 GameManager 调用，设置选中/取消选中外观</summary>
    public void SetSelected(bool selected)
    {
        _isSelected = selected;
        _sr.color = selected ? selectedColor : normalColor;
    }
}
