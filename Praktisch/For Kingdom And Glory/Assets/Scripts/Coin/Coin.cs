using UnityEngine;

public class Coin : MonoBehaviour
{
    #region private Variables
    private Rigidbody2D m_Rigid;
    private AudioSource m_Source;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            m_Source.Play();
        }
    }
}
