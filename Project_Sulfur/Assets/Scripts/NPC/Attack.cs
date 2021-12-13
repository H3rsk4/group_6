using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    float distance;
    public int damage = 5;

    private float hitCooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(player.playerT.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.playerT.position, transform.position);
    }
    private void FixedUpdate()
    {
        if(distance < 1f){

            player.playerT.GetComponent<stats>().Damage(damage, 10f, player.playerT.position - transform.position);


        }
    }
}
