using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Grabable
{
    public override void Use(Vector2 _targetPosition)
    {
        var results = Physics2D.RaycastAll(_targetPosition, Vector2.down);
        foreach (var result in results)
        {
            var plant = result.collider.GetComponent<Plant>();
            if (plant != null)
            {
                plant.Work();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
