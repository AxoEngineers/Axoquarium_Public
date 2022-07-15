using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookIntoCamera : MonoBehaviour
{

    Camera _worldCam;
    [SerializeField] GameObject target;

    private void Update()
    {
        Vector3 followPosition = target.transform.position;
        ChaseCharacter(followPosition);
    }


    private void ChaseCharacter(Vector3 target)
    {
        Vector3 p1 = Camera.main.WorldToScreenPoint(target);
        p1.z = 100;
        Vector3 p2 = Camera.main.ScreenToWorldPoint(p1);
        transform.position = p2;
    }
}
