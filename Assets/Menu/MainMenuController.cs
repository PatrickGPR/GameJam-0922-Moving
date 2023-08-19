using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private float quitTime;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject instructionPanel;

    private int quitGameId;
    private int showOptionsId;
    private int backToMenuId;

    private void Awake()
    {
        quitGameId = Animator.StringToHash("QuitGame");
        showOptionsId = Animator.StringToHash("ShowOptions");
        backToMenuId = Animator.StringToHash("BackToMenu");
    }

    #region ButtonCallbacks

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenOptions()
    {
        animator.SetTrigger(showOptionsId);
    }

    public void BackToMenu()
    {
        animator.SetTrigger(backToMenuId);
    }
    
    public void QuitGame()
    {
        animator.SetTrigger(quitGameId);
        
        StartCoroutine(nameof(QuitGameCoroutine));
    }

    public void ShowInstructions()
    {
        instructionPanel.SetActive(!instructionPanel.activeSelf);
    }

    #endregion

    

    public IEnumerator QuitGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(quitTime);
        
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}