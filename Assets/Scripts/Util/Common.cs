using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    // Itemオブジェクトのドロップする数
    public static void ScatterItem(Transform transform)
    {
        int dropItemCount = 5;

        for (int i = 0; i < dropItemCount; i++)
        {
            GameObject obj = GameManager.instance.itemStock.SetItemObj();
            if (obj)
            {
                Vector3 randomItemPos = RandomPosition(transform.position);
                obj.transform.position = randomItemPos;
                obj.transform.rotation = Quaternion.Euler(0, 0, 45);
                obj.SetActive(true);
            }
        }

        Vector3 RandomPosition(Vector3 targetPos)
        {
            Vector3 pos = targetPos;

            pos.x = Random.Range(targetPos.x - 0.5f, targetPos.x + 0.5f);
            pos.y = Random.Range(targetPos.y - 0.5f, targetPos.y + 0.5f);

            return pos;
        }
    }
}
