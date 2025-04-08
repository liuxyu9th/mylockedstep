using System.Collections;
using System.Collections.Generic;
using Synchronize.Game.Lockstep.Ecsr.Components.Common;
using TrueSync;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    protected void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
