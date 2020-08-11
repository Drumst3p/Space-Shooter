using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

        [SerializeField]
        private float _powerupSpeed;

        [SerializeField]
        private int _powerupID;
        
        
        private AudioSource _powerupSound;

    void Start()
        {
        _powerupSound = GameObject.Find("Powerup").GetComponent<AudioSource>();
        if(_powerupSound == null)
            {
            Debug.Log("Powerup pickup sound not found");
            }
        }

    void Update()
            {
            transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);

            if (transform.position.y < -15)
                {
                Destroy(this.gameObject);
                }
            }

        private void OnTriggerEnter2D(Collider2D obj)
            {
            if (obj.gameObject.tag == "Player")
                {
                _powerupSound.PlayOneShot(_powerupSound.clip);
                Player player = obj.GetComponent<Player>();
                if (player != null)
                    {
                switch (_powerupID)
                    {
                    case 0:
                        player.StartCoroutine("TripleShotEnabled");
                        Destroy(this.gameObject);
                        break;
                    case 1:
                        player.StartCoroutine("SpeedEnabled");
                        Destroy(this.gameObject);
                        break;
                    case 2:
                        player.StartCoroutine("ShieldsEnabled");
                        Destroy(this.gameObject);
                        break;
                    default:
                        Debug.Log("Powerup not found");
                        break;
                    }
                    }
                }
            }
}
    
