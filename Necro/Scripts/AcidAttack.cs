using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidAttack : MonoBehaviour {

    public GameObject AcidPrefab;

    [SerializeField]
    private float m_power = 0.0f;
    [SerializeField]
    private Vector2 m_powerDir = Vector2.zero;

    void Start ()
    {
        acidAttack();
    }

    void acidAttack()
    {
        while (true)
        {
            GameObject acid = Instantiate(AcidPrefab, transform.position, Quaternion.identity) as GameObject;

            Rigidbody2D acidRb = acid.GetComponent<Rigidbody2D>();

            acidRb.AddForce(m_powerDir.normalized * m_power);

            Destroy(acid, 5.0f);
        }
    }

    void Update()
    {

    }

}
