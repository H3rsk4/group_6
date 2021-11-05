using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_self : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy",1f);
    }

    private void Destroy(){
        Destroy(this.gameObject);
    }
}
