using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantOrc : MonoBehaviour
{
    [SerializeField]
    private float mXMin, mXMax, mYMax, mYMin;
    /*[SerializeField]
    private Transform mBoomerangPos;*/

    private float mSpeed = StringHelper.ENEMY_GIANT_RUNNING_SPEED;
    public float mHP = StringHelper.ENEMY_GIANT_HP; //5

    private Rigidbody2D mRB;
    private SpriteRenderer mSpriteRenderer;
    private Animator mAnimator;

    public int nextMove;

    private bool isShooting = false;
    private bool isDead = false;
    private float mGunFireRate = 0;

    private int direction = 1;


    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        InvokeRepeating("IsShooting", 3.0f, 10.0f);
    }

    void FixedUpdate()
    {
        mAnimator.SetBool("isShooting", isShooting);
        mAnimator.SetBool("isDead", isDead);

        mGunFireRate -= Time.deltaTime;

        
        if (!isDead && mGunFireRate < 0)
        {
            mGunFireRate = 10;
            isShooting = true;
        }
        mRB.velocity = new Vector2(direction, 0) * mSpeed;

        if (transform.position.x >= 8.4f)
        {
            direction = -1;
            mSpriteRenderer.flipX = true;
        }
        else if (transform.position.x <= -8.4f)
        {
            direction = 1;
            mSpriteRenderer.flipX = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            mHP--;
            //Debug.Log(mHP);

            if (mHP == 0)
            {
                IsDead();
            }
        }
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.GetDamage(1);
        }
    }

    void IsShooting()
    {
        isShooting = true;
    }

    void IsDead()
    {
        isDead = true;

        GetComponent<Collider2D>().enabled = false;
        mRB.bodyType = RigidbodyType2D.Static;
        Invoke(nameof(Deactivate), 1f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
