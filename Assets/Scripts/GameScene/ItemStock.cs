using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStock : MonoBehaviour
{
    // Itemオブジェクトの格納用
    public GameObject jimiGuiterObj,
                       johnGuiterObj,
                       thunder,
                        powerUpItem;

    public Sprite   eighth, 
                    duplet_quarters,
                    duplet_eighth,
                    thunderImage;


    public GameObject SetItemObj()
    {
        GameObject obj = null;

        int rand = Random.Range(0, 6);

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
            case 4:
                obj = thunder;
                break;
            case 5:
                obj = powerUpItem;
                break;
            default:
                Debug.Log("SetItemObj error");
                break;
        }

        return obj;
    }

}
