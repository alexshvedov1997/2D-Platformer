using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private int lives = 5;
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
          if(value < 5)  lives = value;
            _livesBar.Refresh();
        }
    }
    LivesBar _livesBar;
    [SerializeField]
    private float jumpForce = 15.0f;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRendered;
    private bool isGrounded = false;
    Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRendered = GetComponentInChildren<SpriteRenderer>();
       // _spriteRendered = GetComponent<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
        _livesBar = FindObjectOfType<LivesBar>();
    }
    CharacterState State
    {
        get { return (CharacterState)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value); }
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    // Update is called once per frame
    void Update()
    {
      if(isGrounded)  State = CharacterState.Idle;
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump") & isGrounded == true) Jump();
    }
    private void Run()
    {
        Vector3 direction = transform.right*Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed);
        // _spriteRendered.flipX = direction.x < 0.0f ;
        // direction.x = direction.x < 0 ? direction.x * (-1.0f) : Mathf.Abs(direction.x);
        if (direction.x < 0)
        {
            _spriteRendered.flipX = true;
        }
        else
        {
            _spriteRendered.flipX = false;
        }
    if(isGrounded)   State = CharacterState.Run;
    }
    public void Jump()
    {
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
       

    }
    private void Shoot()
    {
        Vector3 position = transform.position;
     position.y += 0.8f;
       Bullet newBullet =  Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (_spriteRendered.flipX ? -1.0f : 1.0f);
    }

    private void CheckGround()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = coliders.Length > 1 ? isGrounded = true : isGrounded = false ;
        if(!isGrounded) State = CharacterState.Jump;
    }

    public override void ReciveDamage()
    {
        Lives--;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(transform.up * 8.0f,ForceMode2D.Impulse);
        Debug.Log(lives);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
      {
            ReciveDamage();

      }  
    }

}
enum CharacterState
{
    Idle,
    Jump,
    Run
}
