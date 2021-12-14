using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_self : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifespan;
    void Start()
    {
        Invoke("Destroy",lifespan);
        
    }

    private void Destroy(){
        Destroy(this.gameObject);
    }
}
