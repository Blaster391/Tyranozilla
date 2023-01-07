using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private bool m_watered = false;
    private bool m_worked = false;

    [SerializeField]
    private SpriteRenderer m_level1;
    [SerializeField]
    private SpriteRenderer m_level2;
    [SerializeField]
    private SpriteRenderer m_level3;

    [SerializeField]
    private List<Sprite> m_level1Sprite;
    [SerializeField]
    private List<Sprite> m_level2Sprite;
    [SerializeField]
    private List<Sprite> m_level3Sprite;

    private int m_currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_level1.sprite = m_level1Sprite[Random.Range(0, m_level1Sprite.Count - 1)];
        m_level2.sprite = m_level2Sprite[Random.Range(0, m_level1Sprite.Count - 1)];
        m_level3.sprite = m_level3Sprite[Random.Range(0, m_level1Sprite.Count - 1)];

        m_level1.gameObject.SetActive(true);
        m_level2.gameObject.SetActive(false);
        m_level3.gameObject.SetActive(false);

        var results = Physics2D.RaycastAll(transform.position, Vector2.down);
        foreach(var result in results)
        {
            if(result.collider.tag == "dirt")
            {
                transform.position = result.point;
                break;
            }
        }
    }

    public bool ReadyForHarvest()
    {
        return m_watered && m_worked;
    }

    public void Water()
    {
        m_watered = true;
    }

    public void Work()
    {
        m_worked = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentLevel = 0;
        if(m_watered)
        {
            ++m_currentLevel;
        }
        if(m_worked)
        {
            ++m_currentLevel;
        }

        m_level1.gameObject.SetActive(m_currentLevel == 0);
        m_level2.gameObject.SetActive(m_currentLevel == 1);
        m_level3.gameObject.SetActive(m_currentLevel == 2);
    }
}
