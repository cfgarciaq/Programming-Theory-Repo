using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent = null;
    private Collider farmBounds = null;

    private Vector3 m_Center, m_Min, m_Max;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        farmBounds = GameObject.Find("FarmBounds").GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Center = farmBounds.bounds.center;
        m_Min = farmBounds.bounds.min;
        m_Max = farmBounds.bounds.max;

        //Debug.Log($"{gameObject.name} : center{m_Center}, min{m_Min}, max{m_Max}");
    }

    public void MoveRandomly(float distance)
    {

        //float rng_x = Random.Range(m_Min.x, m_Max.x);
        //float rng_y = Random.Range(m_Min.y, m_Max.y);
        //float rng_z = Random.Range(m_Min.z, m_Max.z);
        float rng_x = Mathf.Clamp(transform.position.x + Random.Range(-distance,distance), m_Min.x, m_Max.x);
        float rng_z = Mathf.Clamp(transform.position.z + Random.Range(-distance, distance), m_Min.z, m_Max.z);

        Vector3 rngCoord = new Vector3(rng_x, transform.position.y, rng_z);
        
        if (Physics.Raycast(rngCoord, Vector3.down, out RaycastHit hitInfo, 10))
        {
            agent.SetDestination(hitInfo.point);
        }
        
    }
}
