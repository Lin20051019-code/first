using UnityEngine;

public class InstructorPatrol : MonoBehaviour
{
    [Header("巡逻设置")]
    public float moveSpeed = 3f;                    // 移动速度
    public float patrolRadius = 8f;                 // 巡逻半径（围绕方阵）

    private Vector2 centerPoint;                    // 方阵中心
    private Vector2 targetPosition;
    private float timer = 0f;

    void Start()
    {
        // 以学生方阵为中心
        centerPoint = Vector2.zero;   // 如果你的方阵不在 (0,0)，后面可以调整

        ChooseNewTargetPosition();
    }

    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.IsGameStarted())
            return;   // 游戏还没开始，教官不动

        // 原来的移动代码保持不变
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, targetPosition) < 0.5f)
        {
            timer += Time.deltaTime;
            if (timer > 1.2f)
            {
                ChooseNewTargetPosition();
                timer = 0f;
            }
        }
    }

    void ChooseNewTargetPosition()
    {
        // 在方阵四周随机选择一个点（模拟围绕巡逻）
        float angle = Random.Range(0f, 360f);
        float distance = patrolRadius + Random.Range(-1f, 2f);   // 稍微有点随机距离

        targetPosition = centerPoint + new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
            Mathf.Sin(angle * Mathf.Deg2Rad) * distance
        );
    }

    // 可视化巡逻范围（编辑器中显示圆）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPoint, patrolRadius);
    }
}