using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStock : MonoBehaviour
{
    // Itemオブジェクトの格納用
    public GameObject jimiGuiterObj,
                       johnGuiterObj,
                       thunder,
                        atUpItem,
                        spUpItem;

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
                obj = jimiGuiterObj;
                break;
            case 1:
            case 2:
                obj = johnGuiterObj;
                break;
            case 3:
                if (GameManager.instance.player.Speed >= SP_MAX_PT)
                {
                    obj = jimiGuiterObj;
                }
                else
                {
                    obj = spUpItem;
                }
                break;
            case 4:
                obj = thunder;
                break;
            case 5:
                if (GameManager.instance.player.Speed >= AT_MAX_PT)
                {
                    obj = jimiGuiterObj;
                }
                else
                {
                    obj = atUpItem;
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
