using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovableMonster : Monster
{
    [SerializeField]
    float speed = 2.0f;
    Bullet bullet;
    SpriteRenderer _spriteRender;
    Vector3 _direction;
    protected override void Awake()
    {
        bullet = Resources.Load("Bullet") as Bullet;
        _spriteRender = GetComponentInChildren<SpriteRenderer>();
    }
    protected override void Start()
    {
        _direction = transform.right;
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.gameObject.GetComponent<Unit>();

        if(unit && unit as Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F) ReciveDamage();
            else unit.ReciveDamage();
        }
    }
    protected override void Update()
    {
        Move();
    }

    private void Move()
    {
        // _direction = transform.right;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right*_direction.x, 0.2f);
        if (collider.Length > 0 && collider.All(x => x.GetComponent<Character>())) _direction.x *=(-1.0F);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, speed * Time.deltaTime);


    }



}
