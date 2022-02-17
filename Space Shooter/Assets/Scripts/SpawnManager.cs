using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private float _spawnTime = 5.0f;

    private Vector3 _spawnEnemyPosition = new Vector3(0, 7.5f,0);
    private Vector3 _spawnPowerupPosition = new Vector3(0, 7.5f, 0);
    private bool _stopSpawn = false;

    void Start()
    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine("SpawnPowerupRoutine");
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawn)
        {
            if (_enemy != null)
            {
                Instantiate(_enemy, _spawnEnemyPosition, Quaternion.identity, transform);
            }
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawn)
        {
            if(_powerups != null)
            {
                _spawnPowerupPosition.x = Random.Range(-9.2f, 9.2f);
                int randomPowerup = Random.Range(0, _powerups.Length);

                if(_powerups[randomPowerup] != null)
                {
                    Instantiate(_powerups[randomPowerup], _spawnPowerupPosition, Quaternion.identity, transform);
                }
                else
                {
                    Debug.Log("Powerup does not exist!");
                }
                
            }
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));

        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
    }
}
