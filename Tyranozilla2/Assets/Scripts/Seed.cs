using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Grabable
{
    [SerializeField]
    private Plant m_plantPrefab;

    [SerializeField]
    private float m_seedRange = 1.0f;

    [SerializeField]
    private AudioClip m_plantClip;

    public override void Use(Vector2 _targetPosition)
    {
        var results = Physics2D.RaycastAll(transform.position, Vector2.down);
        foreach (var result in results)
        {
            if (result.collider.tag == "dirt")
            {
                Plant();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "dirt")
        {
            Plant();

        }
    }

    private void Plant()
    {
        var overlaps = Physics2D.OverlapCircleAll(transform.position, m_seedRange);
        foreach (var overlap in overlaps)
        {
            var plant = overlap.GetComponent<Plant>();
            if (plant != null)
            {
                return;
            }
        }

        var myPlant = Instantiate(m_plantPrefab);
        myPlant.transform.position = transform.position + Vector3.up;
        Destroy(gameObject);

        FindObjectOfType<GameMaster>().PlayAudio(m_plantClip, 1.0f, myPlant.gameObject);
    }
}
