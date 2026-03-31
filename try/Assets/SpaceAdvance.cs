using UnityEngine;
using Yarn.Unity;

public class SceneEffects : MonoBehaviour
{
    public GameObject sceneBackground;

    void Awake()  // 用 Awake 确保早于 DialogueRunner 初始化
    {
        var runner = FindObjectOfType<DialogueRunner>();
        if (runner != null)
        {
            // 手动注册命令，强制让 Yarn 认识 playscene
            runner.AddCommandHandler("playscene", PlaySceneFadeIn);
        }
    }

    public void PlaySceneFadeIn()
    {
        if (sceneBackground != null)
        {
            sceneBackground.SetActive(true);
            Animator animator = sceneBackground.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("BlinkFadeIn", -1, 0f);
            }
        }
    }
}