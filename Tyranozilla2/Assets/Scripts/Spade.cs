using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spade : Grabable
{
    [SerializeField]
    private GameObject m_flowerPrefab = null;

    public override void Use(Vector2 _targetPosition)
    {
        var results = Physics2D.RaycastAll(transform.position, Vector2.down);
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
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
