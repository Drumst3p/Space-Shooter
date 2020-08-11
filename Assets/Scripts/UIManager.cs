using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Slider _healthSlider;

    [SerializeField]
    private GameObject _gameOverText;

    [SerializeField]
    private GameObject _restartText;

    private GameManager _gameManager;

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
            {
            Debug.Log("Game Manager not found in Ui manager");
            }
    }

    public void UpdateScore(int playerscore)
        {
        _scoreText.text = "Score: " + playerscore.ToString();
        }

    public void UpdateLives(int current)
        { 
        _healthSlider.value = current;

        if(current <= 0)
            {
            GameOver();
            }
        }

    public void GameOver()
        {
        _gameManager.GameOver();
        _restartText.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        }

    public IEnumerator GameOverFlickerRoutine()
        {
        while (true)
            {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            }
        }
}
