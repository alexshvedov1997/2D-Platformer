using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster
{
    [SerializeField]
    float _rate = 2.0F;
    private Bullet bullet;
    [SerializeField]
    private Color bulletColor = Color.white;
    protected override void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }
    protected override void Start()
    {
        InvokeRepeating("Shoot", _rate, _rate);
    }
    void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
        newBullet.Color = bulletColor;

    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.gameObject.GetComponent<Unit>();

        if (unit && unit as Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F) ReciveDamage();
            else unit.ReciveDamage();
        }
    }

}
