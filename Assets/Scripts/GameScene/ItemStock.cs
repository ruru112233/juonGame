using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStock : MonoBehaviour
{
    // Itemオブジェクトの格納用
    public Sprite   eighth, 
                    duplet_quarters,
                    duplet_eighth,
                    thunderImage;

    // パワーアップ関係
    private const float AT_MAX_PT = 3.0f;
    private const float SP_MAX_PT = 6.0f;

    public GameObject SetItemObj()
    {
        GameObject obj = null;

        int rand = RandomItemObj();

        switch (rand)
        {
            case 0:
                obj = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.JIMI_ITEM);
                break;
            case 1:
            case 2:
                obj = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.JOHN_ITEM);
                break;
            case 3:
                if (GameManager.instance.player.Speed >= SP_MAX_PT)
                {
                    obj = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.JIMI_ITEM);
                }
                else
                {
                    obj = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.SP_UP_ITEM);
                }
                break;
            case 4:
                obj = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.THUNDER_ITEM);
                break;
            case 5:
                if (GameManager.instance.player.Speed >= AT_MAX_PT)
                {
                    obj = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.JIMI_ITEM);
                }
                else
                {
                    obj = BulletPool.Instance.GetPooledObject(EnumData.InstanceObjType.AT_UP_ITEM);
                }
                break;
            default:
                Debug.Log("SetItemObj error");
                break;
        }

        return obj;
    }

    private int RandomItemObj()
    {
        switch (GameManager.instance.unlockCounter)
        {
            case 1:
                return Random.Range(0, 3);
            case 2:
                return Random.Range(0, 4);
            case 3:
                return Random.Range(0, 5);
            case 4:
                return Random.Range(0, 6);
            default:
                return 7;
        }
    }
}
