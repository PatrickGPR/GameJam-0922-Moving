using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenHandler : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float fadeOutTime;

    private Animator animator;
    private int onGameEndId;
    
    void Start()
    {
        GameManager.Instance.OnTimeIsUp += OnTimeIsUp;
        animator = GetComponent<Animator>();
        onGameEndId = Animator.StringToHash("OnGameEnd");
    }

    private void OnTimeIsUp()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float waitingTime = waitTime;
        float fadeOutTime = this.fadeOutTime;
        
        yield return new WaitForSecondsRealtime(waitingTime);
        
        animator.SetTrigger(onGameEndId);

        yield return new WaitForSecondsRealtime(fadeOutTime);

        SceneManager.LoadSceneAsync(1);
    }
}
