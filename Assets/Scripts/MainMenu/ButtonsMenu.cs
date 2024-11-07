using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ExitButton()
    {
        Application.Quit();
    }
}
