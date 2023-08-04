using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singelton<UIManager>
{
    [SerializeField] private GameObject restartButton;

    private void Start()
    {
        RestartButtonState(false);
    }

    public void OnRestartClick()
    {
        DrawManager.Instance.enabled = true;
        GameObject[] allCircleAtCurrentInstance = GameObject.FindGameObjectsWithTag("Circle");
        foreach (GameObject circle in allCircleAtCurrentInstance) { Destroy(circle); }

        Spawner.Instance.SpawnCircles();
        //DrawManager.Instance.current.ResetLine();
        RestartButtonState(false);
    }

    public void RestartButtonState(bool state)
    {
        restartButton.SetActive(state);
    }
}
