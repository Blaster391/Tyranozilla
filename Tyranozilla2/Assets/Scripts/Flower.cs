using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Grabable
{
    private GameMaster m_gameMaster = null;
    void Start()
    {
        m_gameMaster = FindObjectOfType<GameMaster>();
    }
    public override void Use(Vector2 _targetPosition)
    {
        var objects = Physics2D.OverlapPointAll(_targetPosition);
        foreach(var obj in objects)
        {
            if(obj.GetComponent<Shop>() != null)
            {
                m_gameMaster.AddScore();
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
