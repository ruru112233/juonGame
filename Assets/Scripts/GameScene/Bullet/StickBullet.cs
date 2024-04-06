using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBullet : PlayerBullet
{
    private Quaternion playerRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        UpdateRotate();
    }

    // é©êgÇâÒì]Ç≥ÇπÇÈ
    private void UpdateRotate()
    {
        transform.Rotate(new Vector3( 0, 0, 360 * Time.deltaTime));

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
