using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject pooledObject;
    public GameObject stickPooledObject;
    public GameObject enemyPooledObject;

    private int pooledAmount = 20;
    private int enemyPooledAmount = 20;

    List<GameObject> pooledObjects;
    List<GameObject> stickObjects;
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
        // �v���C���[�̒e�̐���
        CreateObjectPoole(pooledObject, pooledAmount, ref pooledObjects);
        CreateObjectPoole(stickPooledObject, pooledAmount, ref stickObjects); // �X�e�B�b�N�̐���

        // �G�l�~�[�̒e�̐���
        CreateObjectPoole(enemyPooledObject, enemyPooledAmount, ref enemyPooledObjects);
    }

    // �I�u�W�F�N�g�v�[���̏����쐬
    private void CreateObjectPoole(GameObject prefab, int amount, ref List<GameObject> pooledObjs)
    {
        pooledObjs = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.SetActive(false);
            pooledObjs.Add(obj);
        }
    }

    // �v���C���[�̒e�擾
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

        return obj;
    }

    // �v���C���[�̒e�擾(�X�e�B�b�N)
    public GameObject GetSticksObject()
    {
        for (int i = 0; i < stickObjects.Count; i++)
        {
            if (!stickObjects[i].activeInHierarchy)
            {
                return stickObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(stickPooledObject);
        obj.SetActive(false);
        stickObjects.Add(obj);

        return obj;
    }

    // �G�l�~�[�̒e�擾
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

        return obj;
    }
}
