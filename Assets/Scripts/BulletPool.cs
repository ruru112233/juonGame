using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject pooledObject;
    public GameObject enemyPooledObject;

    private int pooledAmount = 30;
    private int enemyPooledAmount = 30;

    List<GameObject> pooledObjects;
    List<GameObject> enemyPooledObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの弾の生成
        CreateObjectPoole(ref pooledObjects, pooledObject, pooledAmount);

        // エネミーの弾の生成
        CreateObjectPoole(ref enemyPooledObjects, enemyPooledObject, enemyPooledAmount);
    }

    // オブジェクトプールの初期作成
    private void CreateObjectPoole(ref List<GameObject> pooledObjs, GameObject prefab, int amount)
    {
        pooledObjs = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.SetActive(false);
            pooledObjs.Add(obj);
        }
    }

    // プレイヤーの弾取得
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);

        return null;
    }

    // エネミーの弾取得
    public GameObject GetEnemyPooledObject()
    {
        for (int i = 0; i < enemyPooledObjects.Count; i++)
        {
            if (!enemyPooledObjects[i].activeInHierarchy)
            {
                return enemyPooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(enemyPooledObject);
        obj.SetActive(false);
        enemyPooledObjects.Add(obj);

        return null;
    }
}
