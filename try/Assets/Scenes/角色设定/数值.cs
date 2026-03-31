using UnityEngine;
using UnityEngine.UI;
using TMPro;  // 如果用 TextMeshPro，用这个；否则用 using UnityEngine.UI; 并把 TextMeshProUGUI 改成 Text

public class SliderValueDisplay : MonoBehaviour
{
    [SerializeField] private Slider slider;          // 拖入你的 Slider
    [SerializeField] private TextMeshProUGUI valueText;  // 拖入刚才创建的 Text（或 Text）

    [SerializeField] private string format = "F0";   // 显示格式：F0=整数，F1=一位小数，F2=两位等
    // 可选：如果你想加单位，比如 "%" 或 "dB"
    // private string unit = "%"; 然后 text = value.ToString(format) + unit;

    private void Awake()
    {
        if (slider == null) slider = GetComponent<Slider>();
        if (valueText == null) valueText = GetComponentInChildren<TextMeshProUGUI>(); // 自动找子物体Text

        // 初始更新一次
        UpdateValueText(slider.value);

        // 监听变化（拖动时实时更新）
        slider.onValueChanged.AddListener(UpdateValueText);
    }

    private void UpdateValueText(float value)
    {
        valueText.text = value.ToString(format);
    }

    // 可选：销毁时移除监听，避免内存泄漏
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(UpdateValueText);
    }
}