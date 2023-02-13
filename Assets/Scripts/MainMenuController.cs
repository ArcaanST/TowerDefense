using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject stagesMenu, mainMenu;

    public void SetStageToPlay(string stage)
    {
        SceneManager.LoadScene(stage);
    }

    public void OnPlayClicked()
    {
        stagesMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        stagesMenu.SetActive(false);
    }

}