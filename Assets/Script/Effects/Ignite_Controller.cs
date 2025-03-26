using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite_Controller : MonoBehaviour
{
    public Entity_Behavior targetBehaviour;

    private void Update()
    {
        if (targetBehaviour != null && targetBehaviour.wasDead)
        {
            Destroy(gameObject);
        }
    }
}
