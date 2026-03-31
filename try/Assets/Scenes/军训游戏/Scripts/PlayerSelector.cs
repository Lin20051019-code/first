using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public static GameObject currentPlayer = null;
    public Color normalColor = new Color(0.2f, 0.6f, 1f);
    public Color selectedColor = new Color(1f, 0.92f, 0.3f);

    private SpriteRenderer spriteRenderer;

    void Start()        // 改用 Start() 而不是 Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError(gameObject.name + " 上找不到 SpriteRenderer 组件！");
            // 尝试延迟查找
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        else
        {
            spriteRenderer.color = normalColor;   // 初始化颜色
            Debug.Log(gameObject.name + " SpriteRenderer 已获取并初始化");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("=== 点击了 " + gameObject.name);
        SelectThisSoldier();
    }

    public void SelectThisSoldier()
    {
        // 恢复之前的选中
        if (currentPlayer != null && currentPlayer != gameObject)
        {
            var prev = currentPlayer.GetComponent<PlayerSelector>();
            if (prev != null) prev.Deselect();
        }

        currentPlayer = gameObject;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = selectedColor;
            Debug.Log("✅ 颜色修改成功！ " + gameObject.name + " 已变为选中状态");
        }
        else
        {
            Debug.LogError("仍然无法获取 SpriteRenderer");
        }
    }

    public void Deselect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor;
        }
    }
}