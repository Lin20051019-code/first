using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    // ── 必须在 Inspector 里拖拽赋值的 UI 组件 ──
    [Header("姓名输入")]
    public TMP_InputField nameInputField;       // 拖入你的「姓名输入框」

    [Header("性别按钮")]
    public Button btnMale;
    public Button btnFemale;

    [Header("确认按钮")]
    public Button confirmButton;                // 拖入你的「确认/开始」按钮

    // ── 视觉颜色（可调整） ──
    public Color selectedColor = Color.white;
    public Color unselectedColor = new Color(0.6f, 0.6f, 0.6f, 1f);

    // 内部状态
    private string playerName = "";
    private int gender = 0;         // 0 = 男，1 = 女
    private Button selectedGenderBtn;

    void Start()
    {
        if (nameInputField == null || btnMale == null || btnFemale == null || confirmButton == null)
        {
            Debug.LogError("有 UI 组件没拖拽！请检查 Inspector");
            return;
        }

        // 监听输入变化 → 实时更新 playerName 并控制确认按钮是否可点
        nameInputField.onValueChanged.AddListener(OnNameChanged);

        // 性别按钮点击事件
        btnMale.onClick.AddListener(() => SelectGender(btnMale, 0));
        btnFemale.onClick.AddListener(() => SelectGender(btnFemale, 1));

        // 确认按钮
        confirmButton.onClick.AddListener(OnConfirm);

        // 默认选中「男」
        SelectGender(btnMale, 0);

        // 一开始让输入框获得焦点（弹出输入法）
        nameInputField.ActivateInputField();
    }

    private void OnNameChanged(string newText)
    {
        playerName = newText.Trim();
        // 名字不为空白时才允许确认
        confirmButton.interactable = !string.IsNullOrWhiteSpace(playerName);
    }

    private void SelectGender(Button clickedBtn, int newGender)
    {
        if (selectedGenderBtn == clickedBtn) return;

        // 恢复上一个
        if (selectedGenderBtn != null)
        {
            SetButtonState(selectedGenderBtn, false);
        }

        // 高亮当前
        SetButtonState(clickedBtn, true);

        selectedGenderBtn = clickedBtn;
        gender = newGender;

        Debug.Log($"性别变更为：{(gender == 0 ? "男" : "女")}");
    }

    private void SetButtonState(Button btn, bool isSelected)
    {
        btn.interactable = isSelected;

        var img = btn.GetComponent<Image>();
        if (img != null)
        {
            img.color = isSelected ? selectedColor : unselectedColor;
        }

        var text = btn.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.color = isSelected ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1f);
        }
    }

    private void OnConfirm()
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            Debug.LogWarning("名字不能为空！");
            return;
        }

        Debug.Log($"=== 角色创建成功 ===\n名字：{playerName}\n性别：{(gender == 0 ? "男" : "女")}");

        // 这里放你后续的逻辑，例如：
        // PlayerPrefs.SetString("PlayerName", playerName);
        // PlayerPrefs.SetInt("PlayerGender", gender);
        // SceneManager.LoadScene("下一场景");
    }
}