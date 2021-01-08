using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField]
    private AudioSource m_Background;
    [SerializeField]
    private AudioClip[] m_BackgroundMusic;

    private float m_MusicTimer = 0.0f;
    private float m_MusicTime;
    private int m_CurrentClip = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentClip = Random.Range(0, m_BackgroundMusic.Length);

        m_MusicTime = m_BackgroundMusic[m_CurrentClip].length;

        m_Background.clip = m_BackgroundMusic[m_CurrentClip];

        m_Background.Play();
    }

    // Update is called once per frame
    void Update()
    {
        m_MusicTimer += Time.deltaTime;

        #region Background Music logic
        if (m_MusicTimer >= m_MusicTime)
        {

            for (int i = 0; i < m_BackgroundMusic.Length; i++)
            {
                if (m_BackgroundMusic[m_CurrentClip] == m_BackgroundMusic[i])
                {
                    m_CurrentClip++;

                    if (m_CurrentClip >= m_BackgroundMusic.Length)
                    {
                        m_CurrentClip = 0;
                    }

                    break;
                }
            }

            m_MusicTime = m_BackgroundMusic[m_CurrentClip].length;

            m_Background.clip = m_BackgroundMusic[m_CurrentClip];

            m_Background.Play();

            m_MusicTimer = 0.0f;
        }
        #endregion
    }
}
