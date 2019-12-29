using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    public Color Color
    {
        set
        {
            _sprite.color = value;
        }
    }
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    [SerializeField]
    private float speed = 7.0f;
    private Vector3 _direction;
    public Vector3 Direction{
        set
        {
            _direction = value;
        }
    }

    private SpriteRenderer _sprite;
    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
            
    }
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if(unit && unit.gameObject != parent)
        {
            unit.ReciveDamage();
            Destroy(gameObject);
        }
    }
}
