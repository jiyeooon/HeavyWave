using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private float mSpeed = StringHelper.GUN_SPEED;
    private Rigidbody2D mRB;
    private float mDirection;

    void Awake()
    {
        mRB = GetComponent<Rigidbody2D>();
        float horizontal = Input.GetAxis("Horizontal");
        mRB.velocity = new Vector2(mDirection, 0) * mSpeed * -horizontal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
        }
    }

    public void Shot(Vector2 pos, float rot)
    {
        mRB.position = pos;
        mRB.rotation = rot;

        mDirection = rot > 0f ? -1f : 1f;
        mRB.velocity = new Vector2(mDirection, 0) * mSpeed;
    }
}
