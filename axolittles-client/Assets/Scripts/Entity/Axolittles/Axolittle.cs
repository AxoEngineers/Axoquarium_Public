using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Axolittle : MonoBehaviour
{
    private Animator ani;
    //private NavMeshAgent agent;
    public NavMeshAgent agent;

    private Vector3 goal;
    private float collisionTime;
    private Vector3 _cameraRotation;

    private Timestamp walkTimeout = new Timestamp(10.0f);
    private Timestamp walkTs = new Timestamp(3.0f);

    private float nextWaveTime;

    private AquariumManager aquariumManager;
    private PetMode petModeComponent;
    private IEnumerator Rotate ;

    // Start is called before the first frame update
    void Start()
    {
       
        Rotate = RotateToCamera();
        
        
        aquariumManager = FindObjectOfType<AquariumManager>();
        petModeComponent = aquariumManager.GetComponent<PetMode>();
        agent = gameObject.AddComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        //gameObject.AddComponent<Rigidbody>();
        var collider = gameObject.AddComponent<CapsuleCollider>();
        collider.isTrigger = true;
        agent.speed = 0.5f;
        agent.radius = 0.4f;
        agent.stoppingDistance = 0.1f;

        nextWaveTime = Time.time + Random.Range(0f, 60f);
        NavMesh.avoidancePredictionTime = 5f;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!petModeComponent.enabled)
        {
            collisionTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!petModeComponent.enabled && Time.time - collisionTime > 3.0f)
        {
            goal = transform.position + (transform.position - other.transform.position);
            agent.SetDestination(goal);
            collisionTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!petModeComponent.enabled)
        {
            if (Time.time > nextWaveTime)
            {
                nextWaveTime = Time.time + Random.Range(30f, 90f);
                Wave();
            }

            var pos = transform.position;

            if (walkTs.Expired)
            {
                if (goal == Vector3.zero)
                {
                    Vector3 randomPoint = pos + Random.insideUnitSphere * 20.0f;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomPoint, out hit, 3.0f, NavMesh.AllAreas))
                    {
                        goal = hit.position;
                        agent.SetDestination(goal);
                        ani.SetBool("Moving", true);
                        
                    }
                }

                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            goal = Vector3.zero;
                            ani.SetBool("Moving", false);
                           
                        }
                    }
                }
            }
        }
        else if (petModeComponent.enabled)
        {
            ani.SetBool("Moving", false);
           // ani.ResetTrigger("Wave");
           
            
            return;
        }
        
    }

    public void Wave()
    {
        agent.ResetPath();
        StartCoroutine(RotateToCamera());
        ani.SetBool("Moving", false);
        walkTs.Reset(2.0f);

        goal = Vector3.zero;
       
        
        
        ani.SetTrigger("Wave");
        // Debug.Log($"{gameObject.name} is waving!");
        //transform.LookAt(Camera.main.transform.position);
       
        
        
    }

    private IEnumerator RotateToCamera()
    {
        Debug.Log("Started ");
        while (true)
        {
            var targetRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position,Vector3.up);
            transform.rotation =
                Quaternion.Lerp(transform.localRotation, new Quaternion(0,targetRotation.y,0,1), 4f * Time.deltaTime);
            // transform.localRotation.Set( -90f, transform.localRotation.x, 0f,1);

            Debug.Log($"{targetRotation }  :  {transform.rotation}");
            yield return null;
            //  if(transform.rotation.y-targetRotation.y<=0.035f&&transform.rotation.y-targetRotation.y>=-0.035f)
           
            float dot = Vector3.Dot(transform.forward, (ConvertYToZero(Camera.main.transform.position) - ConvertYToZero(transform.position)).normalized);
            if (dot >= 0.99f)
            {
                Debug.Log("Quite facing");
                yield break;
            }
        }
    }

    private Vector3 ConvertYToZero(Vector3 pos)
    {
        return new Vector3(pos.x, 0, pos.z);
    }
}
