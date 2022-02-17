using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Vector3 _initialPosition = new Vector3(0, 7.51f, 0);
    [SerializeField]
    private int _score = 10;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _collider;

    void Start()
    {
        _initialPosition.x = Random.Range(-9.2f, 9.2f);
        transform.position = _initialPosition;
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("Animator is null");
        }
        _collider = GetComponent<BoxCollider2D>();

        if(_collider == null)
        {
            Debug.LogError("Collider is null");
        }
    }

    void Update()
    {
        Vector3 movement = Vector3.down * _speed * Time.deltaTime;
        transform.Translate(movement);

        if(transform.position.y <= -7.95f)
        {
            _initialPosition.x = Random.Range(-9.2f, 9.2f);
            transform.position = _initialPosition;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerComponent = other.transform.GetComponent<Player>();

            if(playerComponent != null)
            {
                playerComponent.Damage();
            }
            _animator.SetTrigger("onEnemyDeath");
            _collider.enabled = false;
            _speed = 0f;

            Destroy(gameObject, 2.8f);
        }

        if (other.CompareTag("Laser"))
        {
            
            _player.AddScore(_score);
            Destroy(other.gameObject);
            _animator.SetTrigger("onEnemyDeath");
            _collider.enabled = false;
            _speed = 0f;

            Destroy(gameObject, 2.8f);
        }

        

    }
}
