using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Grabable
{
    [SerializeField]
    private int m_uses = 3;

    [SerializeField]
    private Sprite m_emptySprite = null;

    [SerializeField]
    private GameObject m_splashPrefab = null;

    [SerializeField]
    private AudioClip m_pourClip;
    [SerializeField]
    private AudioClip m_emptyClip;

    public override void Use(Vector2 _targetPosition)
    {
        if(m_uses <= 0)
        {
            FindObjectOfType<GameMaster>().PlayAudio(m_emptyClip, 1.0f, gameObject);
            return;
        }

        FindObjectOfType<GameMaster>().PlayAudio(m_pourClip, 1.0f, gameObject);

        var splash = Instantiate(m_splashPrefab);
        splash.transform.position = _targetPosition;

        --m_uses;
        var results = Physics2D.CircleCastAll(_targetPosition, 0.25f, Vector2.down);
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
