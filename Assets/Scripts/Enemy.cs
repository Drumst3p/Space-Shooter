using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _descendSpeed;

    private int _scoreToAdd = 10;

    private float _destructionWaitTime = 2.5f;

    private float _fireWaitTime;
    private float _canFire;

    [SerializeField]
    private float _enemyLives = 0;

    private Vector3 _newResetPosition;

    private Player _player;

    private Animator _enemyAnimator;

    private AudioSource _explosionSound;

    private AudioSource _laserSound;

    [SerializeField]
    private GameObject _enemyLaser;

    void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explosionSound = GameObject.Find("Explosion").GetComponent<AudioSource>();
        _laserSound = GameObject.Find("Firing_Laser").GetComponent<AudioSource>();

        if(_player == null)
            {
            Debug.Log("Player script handle not found");
            }

        if(_enemyAnimator == null)
            {
            Debug.Log("Enemy Animator Component not found");
            }

        if(_explosionSound == null)
            {
            Debug.Log("Explosion Sound not found");
            }
        if(_laserSound == null)
            {
            Debug.Log("Laser Sound not found");
            }

        StartCoroutine("EnemyShooting");
    }

    void Update()
    {
        EnemyMovement();
        EnemyShooting();
    }

    void EnemyMovement()
        {
        transform.Translate(Vector3.down * _descendSpeed * Time.deltaTime);

        if (transform.position.y <= -12)
            {
            _newResetPosition = new Vector3(Random.Range(-21, 21), 12, 0);
            transform.position = _newResetPosition;
            }
        }

    private void OnTriggerEnter2D(Collider2D obj)
        {
        if(obj.gameObject.tag == "Laser")
            {
            Destroy(obj.gameObject);
            EnemyDamage();
            }
        else if (obj.gameObject.tag == "Player")
            {
            if (_player != null)
                {
                _player.Damage();
                }
            _explosionSound.PlayOneShot(_explosionSound.clip);
            Destroy(this.gameObject.GetComponent<Collider2D>());
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _descendSpeed /= 2f;
            Destroy(this.gameObject, _destructionWaitTime);
            }
        }

    public void EnemyDamage()
        {
        _enemyLives--;

        if(_enemyLives <= 0)
            {
            if (_player != null)
            {
            _player.AddScore(_scoreToAdd);
            }
            _explosionSound.PlayOneShot(_explosionSound.clip);
            Destroy(this.gameObject.GetComponent<Collider2D>());
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _canFire = 100;
            _descendSpeed /= 2f;
            Destroy(this.gameObject, _destructionWaitTime);
            }
        }

    public void EnemyShooting()
        {
        if (Time.time > _canFire)
            {
            _fireWaitTime = Random.Range(3f, 5f);
            _canFire = Time.time + _fireWaitTime;
            _laserSound.PlayOneShot(_laserSound.clip);
            GameObject _laser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            _laser.GetComponent<Laser>().EnemyLaser();
            }
        }
    }
