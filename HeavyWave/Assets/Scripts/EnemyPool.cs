using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    public GameObject mEnemy;
    [SerializeField]
    public GameObject mBoomerangEnemy;
    [SerializeField]
    public GameObject GiantEnemy;

    public int enemyNum = 5;
    public int mBoomerangNum = 2;
    public static int recentEnemyNum = 0;

    /*[SerializeField]
    private float mSpawntime;
    private float mCurrenttime;*/

    void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        InvokeRepeating(nameof(Enemy), 3f, 5f);
        //InvokeRepeating(nameof(Boss), 10f, 15f);

        //SpawnRaw(mBoomerangEnemy, new Vector3(7.7f, -4.3f, 0), Quaternion.identity);
        SpawnRaw(mBoomerangEnemy, new Vector3(6.6f, -0.6f, 0), Quaternion.identity);
    }

    private void SpawnRaw(GameObject gameObject, Vector3 pos, Quaternion rot)
    {
        Instantiate(gameObject, pos, rot);
        recentEnemyNum++;
    }

    private void Enemy()
    {
        SpawnRaw(mEnemy, new Vector3(Random.Range(-8.4f, 8.4f), -4.4f, 0), Quaternion.identity);
    }

    private void Boss()
    {
        SpawnRaw(GiantEnemy, new Vector3(Random.Range(-8.4f, 8.4f), -4.4f, 0), Quaternion.identity);
    }
}
