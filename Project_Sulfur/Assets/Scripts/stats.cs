using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    // Start is called before the first frame update
    public float MAX_HEALTH = 10;
    public float IFRAME_TIME = 1;

    public float currentIFrame;
    public float currentHealth;

    public bool isDead;

    public healthbar_script healthBar;
    void Awake()
    {
        currentHealth = MAX_HEALTH;
        isDead = false;

        healthBar = GetComponentInChildren<healthbar_script>();
        if(healthBar != null){
            healthBar.SetMaxHealth(currentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0){
            //death
            Invoke("Death", .5f);
        }
        if(currentIFrame >= 0){
            currentIFrame -= Time.deltaTime;
        }
    }

    public void Damage(int damage, float knockbackPower, Vector3 direction){
        if(currentIFrame <= 0 && !isDead){
            currentHealth -= damage;
            Knockback(knockbackPower, direction);
            IFrame();

            if(healthBar != null){
                healthBar.SetHealth(currentHealth);
            }
        }

    }

    public void IFrame(){
        currentIFrame = IFRAME_TIME;
    }
    

    public virtual void Death(){
        isDead = true;
        Destroy(this.gameObject);
    }

    public void Knockback(float power, Vector3 direction){
        if(currentIFrame <= 0){
            if(transform.GetComponent<Rigidbody2D>() != null){
                transform.GetComponent<Rigidbody2D>().AddForce(direction * power, ForceMode2D.Impulse);
            }
        }
        
    }

    public void UpdateStats(){
        if(healthBar != null){
            healthBar.SetHealth(currentHealth);
        }
    }
}
