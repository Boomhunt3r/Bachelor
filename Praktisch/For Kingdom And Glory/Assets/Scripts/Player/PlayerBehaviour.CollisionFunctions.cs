using UnityEngine;

public partial class PlayerBehaviour : MonoBehaviour
{
    #region Collision Function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            if (GameManager.Instance.SpawnedVagrants.Count == 0)
            {
                Destroy(collision.gameObject);
                VagrantManager.Instance.RemoveCoinsForAll(collision.gameObject);
                Inventory.Instance.Coins++;
                return;
            }
            else
            {
                for (int i = 0; i < GameManager.Instance.SpawnedVagrants.Count; i++)
                {
                    GameManager.Instance.SpawnedVagrants[i].GetComponent<VagrantBehaviour>().RemoveCoin(collision.gameObject);
                }
                Destroy(collision.gameObject);
                Inventory.Instance.Coins++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            m_WallObj = collision.gameObject;
            WallManager.Instance.WallObj = collision.gameObject;
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Archery"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Build"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Smithy"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Town"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);

        }
        if (collision.CompareTag("VagrantSpawner"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            m_WallObj = null;
            WallManager.Instance.WallObj = null;
        }
        if (collision.CompareTag("Cont"))
        {
            TutorialManager.Instance.DeactivateFirstTut();
        }
    }
    #endregion
}