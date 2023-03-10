using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gameOverPanel = null;

    [SerializeField]
    private AudioSource m_musicSource = null;

    [SerializeField]
    private AudioClip m_gameOverClip = null;

    [SerializeField]
    private AudioSource m_audioSourcePrefab = null;

    [SerializeField]
    private Text m_liveScoreText = null;
    [SerializeField]
    private Text m_liveHPText = null;
    [SerializeField]
    private Text m_scoreText = null;
    [SerializeField]
    private Text m_timeText = null;
    [SerializeField]
    private Text m_killsText = null;

    [SerializeField]
    private float m_fadeTime = 1.0f;

    private bool m_gameOver = false;

    private int m_score = 0;
    private float m_time = 0.0f;
    private int m_kills = 0;
    private Player m_player = null;
    private float m_gameOverTime = 0.0f;

    public bool IsGameOver()
    {
        return m_gameOver;
    }

    public void GameOver()
    {
        m_gameOver = true;

        m_gameOverPanel.SetActive(true);

        m_scoreText.gameObject.SetActive(true);
        m_timeText.gameObject.SetActive(true);
        m_killsText.gameObject.SetActive(true);
        m_liveScoreText.gameObject.SetActive(false);
        m_liveHPText.gameObject.SetActive(false);

        m_scoreText.text = $"Earnings - ${m_score}";
        m_timeText.text = $"Time on farm - {m_time:.00}";
        m_killsText.text = $"Bad guys repelled - {m_kills}";

        m_musicSource.Stop();
        m_musicSource.PlayOneShot(m_gameOverClip);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_gameOverPanel.SetActive(false);
        m_liveScoreText.gameObject.SetActive(true);
        m_liveHPText.gameObject.SetActive(true);

        m_player = FindObjectOfType<Player>();

        Image gameOverImage = m_gameOverPanel.GetComponent<Image>();
        var colour = gameOverImage.color;
        colour.a = 0;
        gameOverImage.color = colour;

    }

    public void AddKill()
    {
        m_kills++;
    }

    public void AddScore()
    {
        m_score++;

        m_player.AddHealth();
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_WEBGL
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (!IsGameOver())
        {
            m_liveScoreText.text = $"Earnings - ${m_score}";
            m_liveHPText.text = $"HP - {m_player.GetHealth()}";
            m_time += Time.deltaTime;
        }
        else
        {
            Image gameOverImage = m_gameOverPanel.GetComponent<Image>();
            var colour = gameOverImage.color;
            colour.a = Mathf.Clamp01(m_gameOverTime / m_fadeTime);
            gameOverImage.color = colour;

            m_gameOverTime += Time.deltaTime;
        }
    }

    public void PlayAudio(List<AudioClip> _clips, float _volume, GameObject _trackingObject)
    {
        PlayAudio(_clips[Random.Range(0, _clips.Count)], _volume, _trackingObject);
    }

    public void PlayAudio(AudioClip _clip, float _volume, GameObject _trackingObject)
    {
        AudioSource audioSource = Instantiate(m_audioSourcePrefab);
        audioSource.clip = _clip;
        audioSource.volume = _volume;
        audioSource.Play();

        audioSource.GetComponent<AudioObject>().SetTrackingObject(_trackingObject);
    }
}
