using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameover;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameover)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver()
    {
        _isGameover = true;
    }
}
