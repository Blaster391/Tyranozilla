using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Grabable
{
    [SerializeField]
    private int m_uses = 3;

    [SerializeField]
    private Sprite m_emptySprite = null;

    public override void Use(Vector2 _targetPosition)
    {
        if(m_uses <= 0)
        {
            return;
        }

        --m_uses;
        var results = Physics2D.RaycastAll(_targetPosition, Vector2.down);
        foreach (var result in results)
        {
            var plant = result.collider.GetComponent<Plant>();
            if (plant != null)
            {
                plant.Water();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_uses <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = m_emptySprite;
        }
    }
}
