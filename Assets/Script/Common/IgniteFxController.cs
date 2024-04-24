using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteFxController : MonoBehaviour
{
    
    public void SetUpIgnite(Transform _parent, float _igniteDuration)
    {
        transform.parent = _parent;
        Invoke(nameof(DestroyIgniteFX), _igniteDuration);
    }

    private void DestroyIgniteFX()
    {
        Destroy(gameObject);
    }
}
