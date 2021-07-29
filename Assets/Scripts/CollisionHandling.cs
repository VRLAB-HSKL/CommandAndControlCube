using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandling : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material markedMaterial;

    private void OnTriggerEnter(Collider other)
    {
        GameObject camera = GameObject.Find("ViveCameraRig");

        CCCHandling script =camera.GetComponent<CCCHandling>();

        if (script.lastCollidedObject.tag != "Plane")
        {
            script.lastCollidedObject.GetComponent<MeshRenderer>().material = defaultMaterial;
            script.lastCollidedObject = other.gameObject;
            other.gameObject.GetComponent<MeshRenderer>().material = markedMaterial;
        }
    }

}
