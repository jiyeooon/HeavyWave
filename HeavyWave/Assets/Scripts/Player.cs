using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private BulletPool mBulletPool;
    [SerializeField]
    private Transform mBulletPos;
    [SerializeField]
    private float mXMin, mXMax, mYMax, mYMin;
    [SerializeField]
    Button btnRetry, btnHome, btnExit;
    [SerializeField]
    private GameObject mGameOverPanel;
    private float mSpeed = StringHelper.PLAYER_BASIC_RUNNING_SPEED;
    private float mHp = StringHelper.PLAYER_BAGIC_HP;
    private float mJumpSpeed = StringHelper.PLAYER_JUMPING_SPEED;
    private Rigidbody2D mRB;
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    private float mGunFireRate = 0;
    private bool isRightShoot = true;
    private bool IsGround = true; 
    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        btnRetry.onClick.AddListener(reLoadScene);
        btnHome.onClick.AddListener(gotoMainScene);
        btnExit.onClick.AddListener(quitGame);
    }
    void Update()
    {
        Debug.Log("Is Ground : " + IsGround);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        mRB.velocity = new Vector2(horizontal, 0) * mSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, mXMin, mXMax)
            , Mathf.Clamp(transform.position.y, mYMin,mYMax));
        
        bool isWalking = !Mathf.Approximately(horizontal,0);
        bool isJumping = !Mathf.Approximately(0, vertical);
        mAnimator.SetBool("isWalking", isWalking);
        mAnimator.SetBool("isJumping", isJumping);
        if (Input.GetButtonUp("Horizontal"))
        { 
            mRB.velocity = new Vector2(0.5f * mRB.velocity.normalized.x, mRB.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround)
            {
                mRB.AddForce(Vector2.up * mJumpSpeed, ForceMode2D.Impulse);
                mAnimator.SetBool("isJumping", true);
            }
            else
                return;
        }
        mGunFireRate -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && mGunFireRate < 0)
        {
            var newBullet = mBulletPool.GetFromPool();
            newBullet.Shot(mBulletPos.position, GetDirection());
            mGunFireRate = StringHelper.GUN_FIRE_RATE;
            bool isShooting = true;
            mAnimator.SetBool("isShooting", isShooting);
        }
        if(mHp <= 0)
        {
            Time.timeScale = 0;
            mGameOverPanel.SetActive(true);
        }
        if(horizontal > 0)
        {
            mSpriteRenderer.flipX = false;
            isRightShoot = true;
        }
        else if (horizontal < 0)
        {
            mSpriteRenderer.flipX = true;
            isRightShoot = false;
        }else if(horizontal == 0)
        {
            if(isRightShoot == true)
            {
                mSpriteRenderer.flipX = false;
            }
            else if (isRightShoot == false)
            {
                mSpriteRenderer.flipX = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Monster"))
        {
            GetDamage(StringHelper.MONSTER_DAMAGE);
        }
        if (collision.CompareTag("Ground"))
        {
            IsGround = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGround = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            IsGround = false;
        }

    }
    private float GetDirection()
    {
        if(isRightShoot == true)
        {
            return 0f;
        }
        else 
        {
            return 180f;
        }
    }

    public void GetDamage(int pDamage)
    {
        mHp -= pDamage;
    }
    private void reLoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
    private void gotoMainScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
    private void quitGame()
    {
        Application.Quit();
    }
}
