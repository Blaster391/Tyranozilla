using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Grabable
{
    [SerializeField]
    private Plant m_plantPrefab;

    public override void Use()
    {
        
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "dirt")
        {
            var myPlant = Instantiate(m_plantPrefab);
            myPlant.transform.position = transform.position + Vector3.up;
            Destroy(gameObject);

        }
    }
}
