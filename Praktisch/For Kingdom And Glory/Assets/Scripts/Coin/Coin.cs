using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region private Variables
    private Rigidbody2D m_Rigid;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Sound, Tag for Ground
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
