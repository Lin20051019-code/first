using UnityEngine;

public class SoldierGridGenerator : MonoBehaviour
{
    [Header("方阵设置")]
    public GameObject soldierPrefab;
    public int rows = 5;
    public int columns = 5;

    [Header("外观与间距")]
    [Range(0.5f, 3f)]
    public float soldierSize = 1.0f;

    [Range(1.2f, 3f)]
    public float spacingMultiplier = 1.8f;

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        // 清除旧的士兵
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        float spacing = soldierSize * spacingMultiplier;
        float startX = -(columns - 1) * spacing * 0.5f;
        float startY = (rows - 1) * spacing * 0.5f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 pos = new Vector2(startX + col * spacing, startY - row * spacing);

                GameObject soldier = Instantiate(soldierPrefab, pos, Quaternion.identity);
                soldier.transform.SetParent(transform);
                soldier.name = $"Soldier_{row}_{col}";

                // 设置大小
                soldier.transform.localScale = Vector3.one * soldierSize;

                // 确保有碰撞体（点击用）
                CircleCollider2D circleCollider = soldier.GetComponent<CircleCollider2D>();
                if (circleCollider == null)
                {
                    circleCollider = soldier.AddComponent<CircleCollider2D>();
                }
                circleCollider.isTrigger = false;

                // 添加玩家选择脚本
                if (soldier.GetComponent<PlayerSelector>() == null)
                {
                    soldier.AddComponent<PlayerSelector>();
                }
            }
        }

        Debug.Log($"方阵生成完成！共 {rows * columns} 个士兵，已支持点击选择");
    }
}