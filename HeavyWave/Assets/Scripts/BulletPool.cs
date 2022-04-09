using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private Bullet mOrigin;
    private List<Bullet> mPool;


    private void Start()
    {
        mPool = new List<Bullet>();
    }

    public Bullet GetFromPool()
    {
        for(int i = 0; i < mPool.Count; i++)
        {
            if(!mPool[i].gameObject.activeInHierarchy)
            {
                mPool[i].gameObject.SetActive(true);
                return mPool[i];
            }
        }

        Bullet newObj = Instantiate(mOrigin);
        mPool.Add(newObj);
        return newObj;
    }
}
