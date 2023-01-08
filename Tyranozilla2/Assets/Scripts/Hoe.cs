using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Grabable
{
    [SerializeField]
    private AudioClip m_swingClip;

    public override void Use(Vector2 _targetPosition)
    {
        FindObjectOfType<GameMaster>().PlayAudio(m_swingClip, 1.0f, gameObject);

        var results = Physics2D.CircleCastAll(_targetPosition, 0.25f, Vector2.down);
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
