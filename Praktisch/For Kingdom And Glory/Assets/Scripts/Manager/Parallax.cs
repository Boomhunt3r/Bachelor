using UnityEngine;

public class Parallax : MonoBehaviour
{
    #region Serializefield
    [SerializeField]
    private float m_ParallaxEffectMultiplier;
    #endregion

    #region private Variables
    private Transform m_CameraTransform;
    private Sprite m_Sprite;
    private Texture2D m_Texture;
    private float m_TextureUnitSizeX;
    private float m_OffestPositionX;
    private Vector3 m_LastCameraPos;
    private Vector3 m_DeltaMovement;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_CameraTransform = Camera.main.transform;
        m_LastCameraPos = m_CameraTransform.position;
        m_Sprite = GetComponent<SpriteRenderer>().sprite;
        m_Texture = m_Sprite.texture;
        m_TextureUnitSizeX = m_Texture.width / m_Sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        m_DeltaMovement = m_CameraTransform.position - m_LastCameraPos;
        transform.position += m_DeltaMovement * m_ParallaxEffectMultiplier;
        m_LastCameraPos = m_CameraTransform.position;

        if(Mathf.Abs(m_CameraTransform.position.x - transform.position.x) >= m_TextureUnitSizeX)
        {
            m_OffestPositionX = (m_CameraTransform.position.x - transform.position.x) % m_TextureUnitSizeX;
            transform.position = new Vector3(m_CameraTransform.position.x + m_OffestPositionX, transform.position.y);
        }
    }
}
