using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundManager : MonoBehaviour
{
    [Header("Render")]
    [SerializeField]
    private SpriteRenderer m_Render;
    [Header("Background Colors")]
    [SerializeField]
    private Color m_Morning = new Color(0.7607843f, 0.827451f, 1f, 0.4f);
    [SerializeField]
    private Color m_Noon = new Color(0.5568628f, 0.6196079f, 0.7176471f, 0.4f);
    [SerializeField]
    private Color m_Evening = new Color(0.2745098f, 0.2f, 0.4901961f, 0.4f);
    [SerializeField]
    private Color m_Night = new Color(0.0f, 0.04313726f, 0.1333333f, 0.4f);
    [SerializeField]
    private Color m_Midnight = new Color(0.0f, 0.0f, 0.0f, 0.4f);

    private float m_Timer = 0.0f;
    private bool m_Day = true;
    private bool m_Dark = false;
    private bool m_MidNight = false;
    private bool m_Between = false;

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        #region BackgroundColor logic
        if (m_Render.color == m_Noon && m_Day && !m_Dark && !m_Between && !m_MidNight)
        {
            m_Timer = 0.0f;
            m_Between = true;
            m_Day = false;
        }

        if (m_Render.color == m_Evening && m_Between && !m_Day && !m_Dark && !m_MidNight)
        {
            m_Timer = 0.0f;
            m_Dark = true;
        }

        if (m_Render.color == m_Night && m_Dark && m_Between && !m_Day && !m_MidNight)
        {
            m_Timer = 0.0f;
            m_Between = false;
            m_Dark = false;
            m_MidNight = true;
        }

        if (m_Render.color == m_Midnight && m_MidNight && !m_Dark && !m_Day && !m_Between)
        {
            m_Timer = 0.0f;
            m_MidNight = false;
        }

        if (m_Render.color == m_Morning && !m_Day && !m_Dark && !m_Between)
        {
            m_Timer = 0.0f;
            m_Day = true;
        }

        if (m_Day && !m_Dark)
            m_Render.color = Color.Lerp(m_Morning, m_Noon, m_Timer / 30);

        if (!m_Day && !m_Dark && m_Between)
        {
            m_Render.color = Color.Lerp(m_Noon, m_Evening, m_Timer / 70);
        }

        if (m_Dark && !m_Day)
        {
            m_Render.color = Color.Lerp(m_Evening, m_Night, m_Timer / 20);
        }

        if (m_MidNight && !m_Dark && !m_Day && !m_Between)
        {
            m_Render.color = Color.Lerp(m_Night, m_Midnight, m_Timer / 80);
        }

        if (!m_Dark && !m_Day && !m_Between && !m_MidNight)
        {
           m_Render.color = Color.Lerp(m_Midnight, m_Morning, m_Timer / 40);
        }
        #endregion
    }
}
