using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AxolittleRotator : MonoBehaviour
{
    private List<Transform> _axolittles = new List<Transform>();

    public void RotateAllAxolittles()
    {
        GetAllChidlren();
        foreach (var child in _axolittles)
        {
            if (!child.gameObject.activeSelf) continue;
            StopCoroutine(RotateToCamera(child));
            StartCoroutine(RotateToCamera(child));
        }
    }

    private void GetAllChidlren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!_axolittles.Contains(transform.GetChild(i)))
                _axolittles.Add(transform.GetChild(i));
        }
    }

    private IEnumerator RotateToCamera(Transform child)
    {
        while (true)
        {
            if (AquariumManager.Mode != GamemodesTypes.Petting) yield break;
            var targetRotation = Quaternion.LookRotation(Camera.main.transform.position - child.position, Vector3.up);
            
            child.localRotation =
                Quaternion.Lerp(child.localRotation, new Quaternion(0,targetRotation.y,0,1), 4f * Time.deltaTime);
            
           
            
            Debug.Log($"{targetRotation}  :  {child.localRotation}  - {child.gameObject.name}");
            yield return null;
            float dot = Vector3.Dot( child.transform.forward, (ConvertYToZero(Camera.main.transform.position) - ConvertYToZero( child.transform.position)).normalized);
            if (dot >= 0.999f)
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