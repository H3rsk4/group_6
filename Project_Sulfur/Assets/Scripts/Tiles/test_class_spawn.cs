using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_class_spawn : MonoBehaviour
{
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class MySpawn {
    GameObject myPrefab;

    public MySpawn(GameObject _myPrefab, Transform parent){
        //myPrefab = Instantiate(_myPrefab, parent);
    }
}
