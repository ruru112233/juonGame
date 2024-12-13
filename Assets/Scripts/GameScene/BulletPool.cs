using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject pooledObject;
    public GameObject stickPooledObject;
    public GameObject enemyPooledObject;

    //private int pooledAmount = 20;
    //private int enemyPooledAmount = 20;

    public List<GameObject> pooledObjects;
    public List<GameObject> stickObjects;
    public List<GameObject> enemyPooledObjects;

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
        //CreateObjectPoole(pooledObject, pooledAmount, ref pooledObjects);
        //CreateObjectPoole(stickPooledObject, pooledAmount, ref stickObjects); // �X�e�B�b�N�̐���

        // �G�l�~�[�̒e�̐���
        //CreateObjectPoole(enemyPooledObject, enemyPooledAmount, ref enemyPooledObjects);
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
    public GameObject GetPooledObject(GameManager.InstanceObjType instanceObjType)
    {
        List<GameObject> poolObjList = CreateObjList(instanceObjType);
        for (int i = 0; i < poolObjList.Count; i++)
        {
            if (!poolObjList[i].activeInHierarchy)
            {
                return poolObjList[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(CreateObj(instanceObjType));

        if (obj)
        {
            obj.SetActive(false);
            poolObjList.Add(obj);
        }
        
        return obj;
    }

    // �v���C���[�̒e�擾(�X�e�B�b�N)
    //public GameObject GetSticksObject()
    //{
    //    for (int i = 0; i < stickObjects.Count; i++)
    //    {
    //        if (!stickObjects[i].activeInHierarchy)
    //        {
    //            return stickObjects[i];
    //        }
    //    }

    //    GameObject obj = (GameObject)Instantiate(stickPooledObject);
    //    obj.SetActive(false);
    //    stickObjects.Add(obj);

    //    return obj;
    //}

    // �G�l�~�[�̒e�擾
    //public GameObject GetEnemyPooledObject()
    //{
    //    for (int i = 0; i < enemyPooledObjects.Count; i++)
    //    {
    //        if (!enemyPooledObjects[i].activeInHierarchy)
    //        {
    //            return enemyPooledObjects[i];
    //        }
    //    }

    //    GameObject obj = (GameObject)Instantiate(enemyPooledObject);
    //    obj.SetActive(false);
    //    enemyPooledObjects.Add(obj);

    //    return obj;
    //}

    private GameObject CreateObj(GameManager.InstanceObjType instanceObjType)
    {
        switch (instanceObjType)
        {
            case GameManager.InstanceObjType.PICK_BULLET:
                return pooledObject;
            case GameManager.InstanceObjType.STICK_BULLET:
                return stickPooledObject;
            case GameManager.InstanceObjType.ENEMY_BULLET:
                return enemyPooledObject;
            case GameManager.InstanceObjType.JIMI_ITEM:

                break;
            case GameManager.InstanceObjType.JOHN_ITEM:

                break;
            case GameManager.InstanceObjType.THUNDER_ITEM:

                break;
            case GameManager.InstanceObjType.AT_UP_ITEM:

                break;
            case GameManager.InstanceObjType.SP_UP_ITEM:

                break;
            default:
                break;
        }

        return null;
    }

    private List<GameObject> CreateObjList(GameManager.InstanceObjType instanceObjType)
    {
        switch (instanceObjType)
        {
            case GameManager.InstanceObjType.PICK_BULLET:
                return pooledObjects;
            case GameManager.InstanceObjType.STICK_BULLET:
                return stickObjects;
            case GameManager.InstanceObjType.ENEMY_BULLET:
                return enemyPooledObjects;
            case GameManager.InstanceObjType.JIMI_ITEM:

                break;
            case GameManager.InstanceObjType.JOHN_ITEM:

                break;
            case GameManager.InstanceObjType.THUNDER_ITEM:

                break;
            case GameManager.InstanceObjType.AT_UP_ITEM:

                break;
            case GameManager.InstanceObjType.SP_UP_ITEM:

                break;
            default:
                break;
        }

        return new List<GameObject>();
    }

}
