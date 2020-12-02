using UnityEngine;

public partial class PlayerBehaviour : MonoBehaviour
{
    #region private functions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Direction">Direction Player is looking</param>
    private void Shoot(Vector2 _Direction)
    {
        GameObject Target = null;
        GameObject Arrow = null;

        float XDistance = 0.0f;
        float YDistance = 0.0f;
        float ThrowAngle;
        float TotalVelo;
        float XVelo;
        float YVelo;

        Target = GetClosestTarget(GameObject.FindGameObjectsWithTag("Enemy"), GameObject.FindGameObjectsWithTag("Rabbit"));

        Debug.Log(Vector2.Distance(Target.transform.position, transform.position));

        if (Target != null && Vector2.Distance(Target.transform.position, transform.position) <= 10.0f)
        {
            switch (Inventory.Instance.Bow)
            {
                case EPlayerUpgrade.WOOD:
                    XDistance = Random.Range(Target.transform.position.x - m_ThrowPoint.transform.position.x, _Direction.x * 5.0f);

                    YDistance = Random.Range(Target.transform.position.y - m_ThrowPoint.position.y, 5.0f);
                    break;
                case EPlayerUpgrade.STONE:
                    XDistance = Random.Range(Target.transform.position.x - m_ThrowPoint.transform.position.x, _Direction.x * 2.5f);

                    YDistance = Random.Range(Target.transform.position.y - m_ThrowPoint.position.y, 2.5f);
                    break;
                case EPlayerUpgrade.IRON:
                    XDistance = Target.transform.position.x - m_ThrowPoint.transform.position.x;
                    YDistance = Target.transform.position.y - m_ThrowPoint.transform.position.y;
                    break;
                default:
                    break;
            }

            ThrowAngle = Mathf.Atan((YDistance + 4.905f) / XDistance);

            TotalVelo = XDistance / Mathf.Cos(ThrowAngle);

            XVelo = TotalVelo * Mathf.Cos(ThrowAngle);
            YVelo = TotalVelo * Mathf.Sin(ThrowAngle);

            Arrow = Instantiate(m_Arrow, m_ThrowPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(XVelo, YVelo);

            EffectSource.Play();

            m_Timer = 0.0f;

            return;
        }

        XDistance = Random.Range(_Direction.x * 2.5f, _Direction.x * 5.0f);

        YDistance = Random.Range(transform.position.y + 10.0f - m_ThrowPoint.position.y, 5.0f);

        ThrowAngle = Mathf.Atan((YDistance + 4.905f) / XDistance);

        TotalVelo = XDistance / Mathf.Cos(ThrowAngle);

        XVelo = TotalVelo * Mathf.Cos(ThrowAngle);
        YVelo = TotalVelo * Mathf.Sin(ThrowAngle);

        Arrow = Instantiate(m_Arrow, m_ThrowPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(XVelo, YVelo);

        EffectSource.Play();

        m_Timer = 0.0f;
    }

    /// <summary>
    /// Closest Target Function
    /// </summary>
    /// <param name="_Enemies">All Enemies</param>
    /// <param name="_Rabbits">All Rabbits</param>
    /// <returns>Closest Target</returns>
    private GameObject GetClosestTarget(GameObject[] _Enemies, GameObject[] _Rabbits)
    {
        GameObject TargetE = null;
        GameObject TargetR = null;
        GameObject TargetO = null;

        float MinDist = Mathf.Infinity;
        float EDist = 0.0f;
        float RDist = 0.0f;
        float Dist = 0.0f;

        if (_Enemies != null)
        {
            for (int i = 0; i < _Enemies.Length; i++)
            {
                Dist = Vector2.Distance(_Enemies[i].transform.position, transform.position);

                if (Dist < MinDist)
                {
                    TargetE = _Enemies[i];
                    MinDist = Dist;
                    EDist = Dist;
                }
            }
        }

        MinDist = Mathf.Infinity;
        Dist = 0.0f;

        if (_Rabbits != null)
        {
            for (int i = 0; i < _Rabbits.Length; i++)
            {
                Dist = Vector2.Distance(_Rabbits[i].transform.position, transform.position);

                if (Dist < MinDist)
                {
                    TargetR = _Rabbits[i];
                    MinDist = Dist;
                    RDist = Dist;
                }
            }
        }

        if (EDist <= RDist || RDist == 0.0f)
        {
            TargetO = TargetE;
        }

        if (EDist == 0.0f)
        {
            TargetO = TargetR;
        }

        return TargetO;
    }

    private void PayRepair(EPlayerUpgrade _Tier, ECraftingType _Type)
    {
        switch (_Tier)
        {
            case EPlayerUpgrade.STONE:
                switch (_Type)
                {
                    case ECraftingType.HELMET:
                        break;
                    case ECraftingType.PLATE:
                        break;
                    case ECraftingType.BOOTS:
                        break;
                }
                break;
            case EPlayerUpgrade.IRON:
                switch (_Type)
                {
                    case ECraftingType.HELMET:
                        break;
                    case ECraftingType.PLATE:
                        break;
                    case ECraftingType.BOOTS:
                        break;
                }
                break;
            default:
                break;
        }       
    }
    #endregion
}