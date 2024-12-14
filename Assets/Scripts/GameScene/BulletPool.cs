using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject pooledObject;
    public GameObject stickPooledObject;
    public GameObject jimiItem;
    public GameObject johnItem;
    public GameObject thunderItem;
    public GameObject atUpItem;
    public GameObject spUpItem;

    private int pooledAmount = 10;
    private int itemAmount = 20;

    private List<GameObject> pooledObjects;
    private List<GameObject> stickObjects;
    private List<GameObject> jimiObjects;
    private List<GameObject> johnObjects;
    private List<GameObject> thunderObjects;
    private List<GameObject> atUpObjects;
    private List<GameObject> spUpObjects;

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
        CreateObjectPoole(pooledObject, pooledAmount, ref pooledObjects);
        CreateObjectPoole(stickPooledObject, pooledAmount, ref stickObjects); // スティックの生成
        CreateObjectPoole(jimiItem, itemAmount, ref jimiObjects); 
        CreateObjectPoole(johnItem, itemAmount, ref johnObjects); 
        CreateObjectPoole(thunderItem, itemAmount, ref thunderObjects); 
        CreateObjectPoole(atUpItem, itemAmount, ref atUpObjects); 
        CreateObjectPoole(spUpItem, itemAmount, ref spUpObjects);
    }

    // オブジェクトプールの初期作成
    private void CreateObjectPoole(GameObject prefab, int amount, ref List<GameObject> pooledObjs)
    {
        pooledObjs = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjs.Add(obj);
        }
    }

    // プレイヤーの弾取得
    public GameObject GetPooledObject(EnumData.InstanceObjType instanceObjType)
    {
        List<GameObject> poolObjList = CreateObjList(instanceObjType);
        for (int i = 0; i < poolObjList.Count; i++)
        {
            if (poolObjList[i] && !poolObjList[i].activeInHierarchy)
            {
                return poolObjList[i];
            }
        }

        GameObject obj = Instantiate(CreateObj(instanceObjType));

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
                return jimiItem;
            case EnumData.InstanceObjType.JOHN_ITEM:
                return johnItem;
            case EnumData.InstanceObjType.THUNDER_ITEM:
                return thunderItem;
            case EnumData.InstanceObjType.AT_UP_ITEM:
                return atUpItem;
            case EnumData.InstanceObjType.SP_UP_ITEM:
                return spUpItem;
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
                return jimiObjects;
            case EnumData.InstanceObjType.JOHN_ITEM:
                return johnObjects;
            case EnumData.InstanceObjType.THUNDER_ITEM:
                return thunderObjects;
            case EnumData.InstanceObjType.AT_UP_ITEM:
                return atUpObjects;
            case EnumData.InstanceObjType.SP_UP_ITEM:
                return spUpObjects;
            default:
                break;
        }

        return new List<GameObject>();
    }
}
