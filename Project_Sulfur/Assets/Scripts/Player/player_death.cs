using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_death : MonoBehaviour
{
    public GameObject joystick;
    public GameObject deathPanel;

    private stats playerStats;

    private float deathTimer = 5;

    private Text respawnText;

    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        deathPanel.SetActive(false);
        if(joystick != null){
            joystick.SetActive(true);
        }
        
        playerStats = GetComponent<stats>();
        respawnText = deathPanel.transform.GetChild(1).GetComponent<Text>();

        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.currentHealth <= 0){
            PlayDeathAnimation();
            playerCollider.enabled = false;
        }else{
            playerCollider.enabled = true;
            playerStats.isDead = false;

        }
    }

    private void PlayDeathAnimation(){
        deathPanel.SetActive(true);
        if(joystick != null){
            joystick.SetActive(false);
        }
        

        deathTimer -= Time.deltaTime;
        respawnText.text = "Respawning in " + (Mathf.Round(deathTimer)).ToString();
        if(deathTimer <= 0){
            Respawn();
        }
    }
    private void Respawn(){
        deathPanel.SetActive(false);
        if(joystick != null){
            joystick.SetActive(true);
        }
        
        transform.position = new Vector3(0,0,0);
        playerStats.currentHealth = playerStats.MAX_HEALTH;
        playerStats.UpdateStats();
        playerStats.IFrame();
        
        deathTimer = 5;
    }
}
