using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUiRotateScript : MonoBehaviour
{
    private void LateUpdate()
    {
        // HP�o�[��UI�͏�ɃJ�����̕��������悤�ɂ���B
        transform.rotation = Camera.main.transform.rotation;
    }
}
