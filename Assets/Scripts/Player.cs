using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horBounds = 21f;
    [SerializeField]
    private float _speed;
    private float _speedMultiplier = 2.5f;
    private float _fireRate;
    private float _nextFire = -1f;
    private float _powerupWaitTime = 5f;
    [SerializeField]
    private int _playerLives = 10;

    [SerializeField]
    private int _score;
    private UIManager _uiManager;


    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _playerShieldOverlay;
    [SerializeField]
    private GameObject[] _playerHurtAnimation;
    private AudioSource _laserShot;
    private AudioSource _explosionSound;


    private bool isTripleShotEnabled = false;
    private bool isSpeedEnabled = false;
    private bool isShieldsEnabled = false;

    private Vector3 projectileOffset;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        projectileOffset = new Vector3(0, -1.2f, 0);
        transform.position = new Vector3(0, -7, 0);
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _laserShot = GameObject.Find("Firing_Laser").GetComponent<AudioSource>();
        _explosionSound = GameObject.Find("Explosion").GetComponent<AudioSource>();

        if (_spawnManager == null)
            {
            Debug.Log("Spawn Manager not found");
            }
        
        if(_uiManager == null)
            {
            Debug.Log("Ui Manager is not found");
            }
        
        if(_laserShot == null)
            {
            Debug.Log("Audio source laser shot not found");
            }
        if(_explosionSound == null)
            {
            Debug.Log("Explosion sound not found");
            }
    }


    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
            {
                Shooting();
            }
    }

    void Movement()
        {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 playerControl = new Vector3(hor, vert, 0);     
        transform.Translate( playerControl * _speed * Time.deltaTime);
        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -7f, 12f), transform.position.z);

        if (transform.position.x > _horBounds)
            {
            transform.position = new Vector3(_horBounds, this.transform.position.y, 0);
            }
        else if (transform.position.x < -_horBounds)
            {
            transform.position = new Vector3(-_horBounds, this.transform.position.y, 0);
            }
        }

    void Shooting()
        { 
            _nextFire = Time.time + _fireRate;
        if (isTripleShotEnabled == true)
            {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);

            }
        else
            {
            Instantiate(laserPrefab, this.transform.position - projectileOffset, Quaternion.identity);
            }
        _laserShot.PlayOneShot(_laserShot.clip);
        //play the audio clip for firing
        }

    public void Damage()
        {
        if (isShieldsEnabled == false)
            {
            _playerLives--;
            _uiManager.UpdateLives(_playerLives);
            }
        if(_playerLives <= 7)
            {
            _playerHurtAnimation[0].gameObject.SetActive(true);
            }
        if(_playerLives <= 3)
            {
            _playerHurtAnimation[1].gameObject.SetActive(true);
            }
         if (_playerLives <= 0)
            {
            if (_spawnManager != null)
                {
                Debug.Log("Player has Died!");
                _explosionSound.PlayOneShot(_explosionSound.clip);
                _spawnManager.PlayerHasDied();
                }
            Destroy(this.gameObject);
            }
        }

    public void AddScore(int points)
        {
        // script communication
        // sending the added value of score to the UI manager to update
        _score += points;
        if (_uiManager != null)
            {
            _uiManager.UpdateScore(_score);
            }
        }

    public IEnumerator TripleShotEnabled()
        {
        isTripleShotEnabled = true;
        yield return new WaitForSeconds(_powerupWaitTime);
        isTripleShotEnabled = false;
        }

    public IEnumerator SpeedEnabled()
        {
        isSpeedEnabled = true;
        _speed *= _speedMultiplier;
        yield return new WaitForSeconds(_powerupWaitTime);
        _speed /= _speedMultiplier;
        isSpeedEnabled = false;
        }

    public IEnumerator ShieldsEnabled()
        {
        isShieldsEnabled = true;
        _playerShieldOverlay.SetActive(true);
        yield return new WaitForSeconds(_powerupWaitTime);
        isShieldsEnabled = false;
        _playerShieldOverlay.SetActive(false);
        }

    private void OnTriggerEnter2D(Collider2D obj)
        {
        if(obj.gameObject.tag == "Enemy_Laser")
            {
            Damage();
            Destroy(obj.gameObject);
            }
        }
    }
