using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Grabable
{
    private bool m_atShop = false;

    public override void Use()
    {
        if(m_atShop)
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
