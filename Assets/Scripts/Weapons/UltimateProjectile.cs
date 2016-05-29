using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UltimateProjectile : MonoBehaviour, IDamageSender
{
    public float speed;

    public float impactDmg = 35f;
    public float splashDmg = 10f;

    public float m_fExplRadius = 5.0f;
    public float m_fExplPower = 1000.0f;

    Rigidbody body;
    private List<Rigidbody> players;

    // Used for information that is send to damage reciever 
    // about the sender and the weapon used.
    GameObject m_sourceWeapon;
    private float m_fCurrentDelayTimer;
    public float m_fDelayTimer = 50;

    public GameObject SourceWeapon
    {
        get { return m_sourceWeapon; }
        set
        {
            m_sourceWeapon = value;
        }
    }

    void OnEnable()
    {
        m_fCurrentDelayTimer = m_fDelayTimer;
    }

    void Start()
    {
        players = new List<Rigidbody>();
        body = GetComponent<Rigidbody>();
        body.velocity *= speed;
        var playerarray = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in playerarray)
        {
            players.Add(player.GetComponent<Rigidbody>());
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        
        // Also expensive
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_fExplRadius);

        DamageInfo impactInfo = new DamageInfo(SourceWeapon, impactDmg);

        // Careful, this is expensive as it uses reflection
        // This, will however only trigger on colliders that also have IDamageRecievers in their hierarchy level, meaning that
        // the players main capsule collider will not be affected
        collision.collider.gameObject.SendMessageUpwards("RecieveDamage", impactInfo, SendMessageOptions.DontRequireReceiver);
        
        DamageInfo splashInfo = new DamageInfo(SourceWeapon, splashDmg);

        foreach (Collider hit in colliders)
        {
            // Also expensive
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.gameObject.SendMessageUpwards("RecieveDamage", splashInfo, SendMessageOptions.DontRequireReceiver);           
                rb.AddExplosionForce(m_fExplPower, explosionPos, m_fExplRadius, 3.0F);
            }
        }

        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if(m_fCurrentDelayTimer-- <= 0)
        foreach (Rigidbody playerBody in players)
        {
            Vector3 direction = transform.position - playerBody.transform.position;

            direction = (direction/direction.sqrMagnitude)*500000;
            playerBody.AddForce(direction);
        }
    }

}
