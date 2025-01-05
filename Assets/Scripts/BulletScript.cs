using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
