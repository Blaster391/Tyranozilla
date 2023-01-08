using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spade : Grabable
{
    [SerializeField]
    private GameObject m_flowerPrefab = null;
    [SerializeField]
    private AudioClip m_swingClip;
    public override void Use(Vector2 _targetPosition)
    {
        FindObjectOfType<GameMaster>().PlayAudio(m_swingClip, 1.0f, gameObject);

        var results = Physics2D.CircleCastAll(_targetPosition, 0.25f, Vector2.down);
        foreach (var result in results)
        {
            var plant = result.collider.GetComponent<Plant>();
            if (plant != null && plant.ReadyForHarvest())
            {
                var flower = Instantiate(m_flowerPrefab);
                flower.transform.position = plant.gameObject.transform.position;
                Destroy(plant.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
