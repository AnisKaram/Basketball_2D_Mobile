using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    int sceneID = 1;

    #region public methods
    public void StartGame()
    {
        Enter();
    }
    public void QuitGame()
    {
        Quit();
    }
    #endregion public methods

    #region private methods
    private void Enter()
    {
        SceneManager.LoadScene(sceneID);
    }
    private void Quit()
    {
        Application.Quit();
    }
    #endregion private methods
}
