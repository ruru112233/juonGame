using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject pooledObject;
    public GameObject stickPooledObject;

    private int pooledAmount = 10;

    private List<GameObject> pooledObjects;
    private List<GameObject> stickObjects;


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
    public GameObject GetPooledObject(EnumData.InstanceObjType instanceObjType)
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

    private GameObject CreateObj(EnumData.InstanceObjType instanceObjType)
    {
        switch (instanceObjType)
        {
            case EnumData.InstanceObjType.PICK_BULLET:
                return pooledObject;
            case EnumData.InstanceObjType.STICK_BULLET:
                return stickPooledObject;
            case EnumData.InstanceObjType.JIMI_ITEM:

                break;
            case EnumData.InstanceObjType.JOHN_ITEM:

                break;
            case EnumData.InstanceObjType.THUNDER_ITEM:

                break;
            case EnumData.InstanceObjType.AT_UP_ITEM:

                break;
            case EnumData.InstanceObjType.SP_UP_ITEM:

                break;
            default:
                break;
        }

        return null;
    }

    private List<GameObject> CreateObjList(EnumData.InstanceObjType instanceObjType)
    {
        switch (instanceObjType)
        {
            case EnumData.InstanceObjType.PICK_BULLET:
                return pooledObjects;
            case EnumData.InstanceObjType.STICK_BULLET:
                return stickObjects;
            case EnumData.InstanceObjType.JIMI_ITEM:

                break;
            case EnumData.InstanceObjType.JOHN_ITEM:

                break;
            case EnumData.InstanceObjType.THUNDER_ITEM:

                break;
            case EnumData.InstanceObjType.AT_UP_ITEM:

                break;
            case EnumData.InstanceObjType.SP_UP_ITEM:

                break;
            default:
                break;
        }

        return new List<GameObject>();
    }

}
