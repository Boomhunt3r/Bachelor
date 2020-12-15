using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    #region SerializeField
    [Header("Background Colors")]
    [SerializeField]
    private Color m_Morning = new Color(0.7607843f, 0.827451f, 1f, 0);
    [SerializeField]
    private Color m_Noon = new Color(0.5568628f, 0.6196079f, 0.7176471f, 0);
    [SerializeField]
    private Color m_Evening = new Color(0.2745098f, 0.2f, 0.4901961f, 0);
    [SerializeField]
    private Color m_Night = new Color(0.0f, 0.04313726f, 0.1333333f, 0);
    [SerializeField]
    private Color m_Midnight = new Color(0.0f, 0.0f, 0.0f, 0);
    [Header("Background Music")]
    [SerializeField]
    private AudioSource m_Background;
    [SerializeField]
    private AudioClip[] m_BackgroundMusic;
    #endregion

    #region private Variables
    private Camera m_Camera;
    private float m_Timer = 0.0f;
    private float m_MusicTimer = 0.0f;
    private float m_MusicTime;
    private int m_CurrentClip = 0;
    private bool m_Day = true;
    private bool m_Dark = false;
    private bool m_MidNight = false;
    private bool m_Between = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentClip = Random.Range(0, m_BackgroundMusic.Length);

        m_MusicTime = m_BackgroundMusic[m_CurrentClip].length;

        m_Background.clip = m_BackgroundMusic[m_CurrentClip];

        m_Background.Play();

        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
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

        #region BackgroundColor logic
        if (m_Camera.backgroundColor == m_Noon && m_Day && !m_Dark && !m_Between && !m_MidNight)
        {
            m_Timer = 0.0f;
            m_Between = true;
            m_Day = false;
        }

        if (m_Camera.backgroundColor == m_Evening && m_Between && !m_Day && !m_Dark && !m_MidNight)
        {
            m_Timer = 0.0f;
            m_Dark = true;
        }

        if (m_Camera.backgroundColor == m_Night && m_Dark && m_Between && !m_Day && !m_MidNight)
        {
            m_Timer = 0.0f;
            m_Between = false;
            m_Dark = false;
            m_MidNight = true;
        }

        if(m_Camera.backgroundColor == m_Midnight && m_MidNight && !m_Dark && !m_Day && !m_Between)
        {
            m_Timer = 0.0f;
            m_MidNight = false;
        }

        if (m_Camera.backgroundColor == m_Morning && !m_Day && !m_Dark && !m_Between)
        {
            m_Timer = 0.0f;
            m_Day = true;
        }

        if (m_Day && !m_Dark)
            m_Camera.backgroundColor = Color.Lerp(m_Morning, m_Noon, m_Timer / 60);

        if (!m_Day && !m_Dark && m_Between)
        {
            m_Camera.backgroundColor = Color.Lerp(m_Noon, m_Evening, m_Timer / 40);
        }

        if (m_Dark && !m_Day)
        {
            m_Camera.backgroundColor = Color.Lerp(m_Evening, m_Night, m_Timer / 20);
        }

        if(m_MidNight && !m_Dark && !m_Day && !m_Between)
        {
            m_Camera.backgroundColor = Color.Lerp(m_Night, m_Midnight, m_Timer / 80);
        }

        if (!m_Dark && !m_Day && !m_Between && !m_MidNight)
        {
            m_Camera.backgroundColor = Color.Lerp(m_Midnight, m_Morning, m_Timer / 40);
        }
        #endregion
    }
}
