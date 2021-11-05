using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public const float MAX_HEALTH = 10;
    public float currentHealth;
    void Start()
    {
        currentHealth = MAX_HEALTH;

    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0){
            //death
            Invoke("Death", .5f);
        }
    }

    public void Damage(int damage){
        currentHealth -= damage;
    }

    public virtual void Death(){
        Destroy(this.gameObject);
    }
}
