using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUiRotateScript : MonoBehaviour
{
    private void LateUpdate()
    {
        // HPバーのUIは常にカメラの方を向くようにする。
        transform.rotation = Camera.main.transform.rotation;
    }
}
