using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    [SerializeField] GameObject _firstMenu;
    GameObject _currentMenu;

    private void Start()
    {
        _currentMenu = _firstMenu;
    }

    public void GoTo(GameObject PanelToGo)
    {
        PanelToGo.SetActive(true);
        _currentMenu.SetActive(false);
        _currentMenu = PanelToGo;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
