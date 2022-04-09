using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float mXMin, mXMax, mYMax, mYMin;

    private float mSpeed = StringHelper.ENEMY_BASIC_RUNNING_SPEED;
    public float mHP = StringHelper.ENEMY_BAGIC_HP;
    public float changeTime;

    private Rigidbody2D mRB;
    private SpriteRenderer mSpriteRenderer;
    private Animator mAnimator;

    private bool isShooting = false;
    private bool isDead = false;

    private float timer;
    private int direction = 1;

    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        timer = changeTime;
    }

    void FixedUpdate()
    {
        mAnimator.SetBool("isShooting", isShooting);
        mAnimator.SetBool("isDead", isDead);

        mRB.velocity = new Vector2(direction, 0) * mSpeed;

        changeTime = Random.Range(1f, 2f);
        

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            direction = -direction;
            timer = changeTime;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, mXMin, mXMax)
            , Mathf.Clamp(transform.position.y, mYMin, mYMax));

        if(direction == 1)
        {
            mSpriteRenderer.flipX = false;
        }
        else if(direction == -1)
        {
            mSpriteRenderer.flipX = true;
        }

        if (transform.position.x >= 8.4f)
        {
            direction = -1;
            timer = changeTime;
        }
        else if (transform.position.x <= -8.4f)
        {
            direction = 1;
            timer = changeTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            isDead = true;
            mHP--;

            GetComponent<Collider2D>().enabled = false;
            mRB.bodyType = RigidbodyType2D.Static;
            Invoke(nameof(Deactivate), 1f);

        }
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.GetDamage(1);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDead)
            return;

        if (other.CompareTag("Player"))
        {
            timer = 0;
            IsShooting();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isShooting = false;
        timer = changeTime;

        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 position = mRB.position;

            if (playerPos.x - position.x > 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;

            }
        }
    }

    void IsShooting()
    {
        isShooting = true;
        direction = 0;
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
