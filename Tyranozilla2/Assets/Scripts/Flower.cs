using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Grabable
{

    public override void Use(Vector2 _targetPosition)
    {
        var objects = Physics2D.OverlapPointAll(_targetPosition);
        foreach(var obj in objects)
        {
            if(obj.GetComponent<Shop>() != null)
            {
                Destroy(gameObject);
            }
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

}
