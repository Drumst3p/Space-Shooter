using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _descendSpeed;
    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private GameObject _explosionAnimation;
    [SerializeField]
    private SpawnManager _spawnManager;
    private AudioSource _explosionSound;
    
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _explosionSound = GameObject.Find("Explosion").GetComponent<AudioSource>();

        if (_spawnManager == null)
            {
            Debug.Log("Spawn Manager not found in the Asteroid script");
            }
        if(_explosionSound == null)
            {
            Debug.Log("Explosion sound not found");
            }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _descendSpeed * Time.deltaTime,Space.World);

        transform.Rotate(new Vector3(0,0,_rotateSpeed) *  Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D obj)
        {
        if (obj.gameObject.tag == ("Laser"))
            {
            Destroy(obj.gameObject);
            _descendSpeed /= 2f;
            _explosionSound.PlayOneShot(_explosionSound.clip);
            Destroy(this.gameObject.GetComponent<Collider2D>());
            Instantiate(_explosionAnimation, this.transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.17f);
            }
        }
    }
