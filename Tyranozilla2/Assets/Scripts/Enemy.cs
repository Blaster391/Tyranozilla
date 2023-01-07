using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Grabable
{
    private Player m_player = null;

    public override void Use(Vector2 _targetPosition)
    {

    }

    protected override void Start()
    {
        base.Start();

        m_player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
