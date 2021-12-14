using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Loot{
    public _Item item;
    [Range(1,999)]
    public int Amount;
}

public class stats : MonoBehaviour
{
    // Start is called before the first frame update
    public float MAX_HEALTH = 10;
    public float IFRAME_TIME = 1;

    public float currentIFrame;
    public float currentHealth;

    public float deathInvokeTimer;
    public bool isDead;
    public bool destroyOnDeath;

    public healthbar_script healthBar;
    private bool healthBarIsOn = false;

    public GameObject gibPrefab;

    private SpriteRenderer spriteRenderer;

    public GameObject droppedItem;
    public Loot[] loot;
    void Awake()
    {
        currentHealth = MAX_HEALTH;
        isDead = false;

        healthBar = GetComponentInChildren<healthbar_script>();
        if(healthBar != null){
            healthBar.SetMaxHealth(currentHealth);
            healthBar.transform.gameObject.SetActive(false);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0){
            //death
            Invoke("Death", deathInvokeTimer);
        }
        if(currentIFrame >= 0){
            currentIFrame -= Time.deltaTime;
        }else{
            spriteRenderer.color = Color.white;
        }

        if(currentHealth < MAX_HEALTH && !healthBarIsOn){
            //activate healthbar
            healthBar.transform.gameObject.SetActive(true);
            healthBarIsOn = true;
        }

        if(currentHealth == MAX_HEALTH && healthBarIsOn){
            //deactivate healthbar
            healthBar.transform.gameObject.SetActive(false);
            healthBarIsOn = false;
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

        if(spriteRenderer != null){
            spriteRenderer.color = Color.red;
        }

    }

    public void IFrame(){
        currentIFrame = IFRAME_TIME;
    }
    

    public virtual void Death(){
        if(gibPrefab != null && !isDead){
            Instantiate(gibPrefab, this.transform.position, Quaternion.identity);
            if(loot.Length != 0){
                for(int i = 0; i < loot.Length; i++){
                    GameObject lootPrefab = Instantiate(droppedItem, this.transform.position, Quaternion.identity);
                    lootPrefab.GetComponent<pickup_script>().SetupItem(loot[i].item, loot[i].Amount);
                }
                
            }
            isDead = true;
        }
        if(destroyOnDeath){
            Destroy(this.gameObject);
        }
        
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
