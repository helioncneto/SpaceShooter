using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private GameObject _restart;

    private GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager== null)
        {
            Debug.LogError("GameManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText(int value)
    {
        _scoreText.text = "Score: " + value.ToString();
    }

    public void UpdateLives(int lives)
    {
        _livesImage.sprite = _livesSprites[lives];
    }

    public void CallGameOver()
    {
        _gameOver.SetActive(true);
        _restart.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine("FlickerGameOver");
    }

    public void RemoveGameOver()
    {
        _gameOver.SetActive(false);
        _gameOver.SetActive(false);
        StopCoroutine("FlickerGameOver");
    }

    IEnumerator FlickerGameOver()
    {
        Text gameoverText = _gameOver.GetComponent<Text>();
        while (true)
        {
            gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);
            gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
        }

    }

}
