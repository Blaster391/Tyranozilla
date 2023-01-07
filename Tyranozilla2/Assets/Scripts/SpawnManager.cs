using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemyPrefab = null;

    [SerializeField]
    private float m_spawnRange = 20.0f;

    [SerializeField]
    private float m_startingTimer = 10.0f;
    [SerializeField]
    private float m_endingTimer = 0.5f;
    [SerializeField]
    private float m_rampTime = 600.0f;

    private float m_timeSinceLastSpawn = 0.0f;
    private float m_liveTime = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_timeSinceLastSpawn += Time.deltaTime;
        m_liveTime += Time.deltaTime;
        float spawnTime = Mathf.Lerp(m_startingTimer, m_endingTimer, m_liveTime / m_rampTime);

        if(m_timeSinceLastSpawn > spawnTime)
        {
            var enemy = Instantiate(m_enemyPrefab);
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-m_spawnRange, m_spawnRange);

            enemy.transform.position = spawnPosition;

            m_timeSinceLastSpawn = 0.0f;
        }

    }
}
