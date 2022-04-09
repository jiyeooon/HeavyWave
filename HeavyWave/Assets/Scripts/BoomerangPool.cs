using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangPool : MonoBehaviour
{
    [SerializeField]
    private Boomerang mOrigin;
    private List<Boomerang> mPool;

    private void Start()
    {
        mPool = new List<Boomerang>();
    }

    public Boomerang GetFromPool()
    {
        for (int i = 0; i < mPool.Count; i++)
        {

            if (!mPool[i].gameObject.activeInHierarchy)
            {
                mPool[i].gameObject.SetActive(true);
                return mPool[i];
            }
        }

        Boomerang newObj = Instantiate(mOrigin);
        mPool.Add(newObj);
        return newObj;

    }
}
