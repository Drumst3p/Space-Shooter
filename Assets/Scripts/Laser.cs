using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;
    private bool _isEnemylaser = false;

    void Update()
    {
    if(_isEnemylaser == false)
            {
            MoveUp();
            }
        else
            {
            MoveDown();
            }
    }

    void MoveUp()
        {
        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

        if (gameObject.transform.position.y > 15)
            {
            if(transform.parent != null)
                {
                Destroy(transform.parent.gameObject);
                }

            Destroy(this.gameObject);
            }
        }

    void MoveDown()
        {
        transform.Translate(Vector3.down * projectileSpeed * Time.deltaTime);

        if (gameObject.transform.position.y < -15)
            {
            if (transform.parent != null)
                {
                Destroy(transform.parent.gameObject);
                }

            Destroy(this.gameObject);
            }
        }

    public void EnemyLaser()
        {
        _isEnemylaser = true;
        }
    }
