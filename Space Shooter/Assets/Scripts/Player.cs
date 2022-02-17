using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    [SerializeField]
    private GameObject _tripleShotLaser;
    private GameObject _shield;
    private bool _isTripleShotActive;
    private bool _isShieldActive;
    private float _multiplier = 2;

    private UIManager _uimanager;


    private SpawnManager _spawnManager;

    void Awake()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _shield = transform.GetChild(0).gameObject;
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Start()
    {
        // Put the player in the center of the screen (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        if(_uimanager == null)
        {
            Debug.LogError("UI Manager is null");
        }
    }


    void Update()
    {
        CalculateMovement();

        if (Input.GetButtonDown("Fire1") && Time.time > _canFire)
        {
            Fire();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // O if abaixo foi substituido pelo Mathf.Clamp
        /*
        if (transform.position.y < -3.79f && verticalInput < 0)
        {
            verticalInput = 0;
        }
        else if (transform.position.y >= 0 && verticalInput > 0)
        {
            verticalInput = 0;
        }
        */

        if (transform.position.x < -11.4f)
        {
            transform.position = new Vector3(11.4f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 11.4f)
        {
            transform.position = new Vector3(-11.4f, transform.position.y, transform.position.z);
        }

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movement.normalized * _speed * Time.deltaTime);
        //O metodo Clamp limita o valor de y entre -3.79 ate 0
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.79f, 0), transform.position.z);

    }

    void Fire()
    {
        
        if(_laserPrefab != null)
        {
            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive)
            {
                if(_tripleShotLaser != null)
                {
                    Vector3 laserOffset = new Vector3(0.2f, 0, 0);
                    Instantiate(_tripleShotLaser, transform.position + laserOffset, Quaternion.identity);
                }
                
            }
            else
            {
                if(_laserPrefab != null)
                {
                    Vector3 laserOffset = new Vector3(0, 1.05f, 0);
                    Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
                }
                
            }
            
        }
    }

    public void Damage()
    {
        if (!_isShieldActive)
        {
            _lives -= 1;
            _uimanager.UpdateLives(_lives);

            if (_lives <= 0)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(gameObject);
                _uimanager.CallGameOver();
            }
        } else
        {
            _shield.SetActive(false);
            _isShieldActive = false;
        }
        
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine("TripleShotPowerDown");
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speed *= _multiplier;
        StartCoroutine("SpeedBoostPowerDown");
    }

    public void ShieldActive()
    {
        _shield.SetActive(true);
        _isShieldActive = true;
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _multiplier;
    }

    public void AddScore(int num)
    {
        _score += num;
        if(_uimanager != null)
        {
            _uimanager.UpdateScoreText(_score);
        }
        
    }
}
