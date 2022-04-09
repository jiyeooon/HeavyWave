using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangEnemy : MonoBehaviour
{
    [SerializeField]
    public BoomerangPool mBoomerangPool;
    [SerializeField]
    private Transform mBoomerangPos;
    [SerializeField]
    private float mXMin, mXMax, mYMax, mYMin;
    

    public float mHP = StringHelper.ENEMY_BAGIC_HP;

    private Rigidbody2D mRB;
    private Animator mAnimator;

    private bool isShooting = false;
    private bool isDead = false;
    private float mGunFireRate = 0;


    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        mAnimator.SetBool("isDead", isDead);

        mGunFireRate -= Time.deltaTime;

        if (!isDead && mGunFireRate < 0)
        {
            var newBoomerang = mBoomerangPool.GetFromPool();
            newBoomerang.Shot(mBoomerangPos.position, 180f);
            mGunFireRate = 3;
            isShooting = true;
            mAnimator.SetTrigger("IsShooting");
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

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
