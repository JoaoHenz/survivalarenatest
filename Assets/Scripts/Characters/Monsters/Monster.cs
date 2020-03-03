using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character {

    public MonsterSheet monsterSheet;
    protected Animator animator;
    Rigidbody rb;
    public NavMeshAgent agent;
    int healthPoints;
    public override int HealthPoints
    {
        get
        {
            return healthPoints;
        }
        set
        {
            int oldHealthPoints = healthPoints;
            healthPoints = value;
            if (healthPoints <= 0)
            {
                audioSource.clip = deathSound;
                audioSource.Play();
                animator.SetBool("hasDied", true);
            }
            else
            {
                if (oldHealthPoints > healthPoints)
                {
                    audioSource.clip = takeDamageSound;
                    audioSource.Play();
                    animator.SetBool("wasHit", true);
                }
                
            }
        }
    }
    Vector3 previousFramePosition;

    protected new void Start () {
        base.Start();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = monsterSheet.speed;
        healthPoints = monsterSheet.health;
        previousFramePosition = transform.position;
        team = Character.Team.Monsters;
        ChangeColor();

    }

    protected virtual void ChangeColor() { }

    public void WalkTowards(Vector3 pos)
    {
        agent.destination = new Vector3 (pos.x,transform.position.y,pos.z);
        float movementPerFrame = Vector3.Distance(previousFramePosition, transform.position);
        float velocity = movementPerFrame / Time.deltaTime;
        previousFramePosition = transform.position;
        animator.SetFloat("velocity", velocity);

    }

    public bool InRange()
    {
        if (Vector3.Distance(transform.position,GameManager.player.transform.position) < monsterSheet.range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HandleDeath()
    {
        giveScoreToPlayer();
        spawnKits();
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

   
    public void giveScoreToPlayer()
    {
        GameManager.player.enemiesKilled += 1; 
        GameManager.player.Score += monsterSheet.scoreForKill;
    }
    
    void spawnKits()
    {

        float roll;
        roll = Random.Range(0, 100);
        if (roll < GameManager.sheet.spawnRateAmmoKit)
        {
            GameObject kit = PoolManager.GetObject("AmmoKit");
            kit.transform.position = transform.position;
            kit.transform.forward = transform.forward;
            kit.SetActive(true);
            kit.GetComponent<Kit>().AfterEnable();
        }
        else
        {
            roll = Random.Range(0, 100);
            if (roll < GameManager.sheet.spawnRateMedKit)
            {
                GameObject kit = PoolManager.GetObject("MedKit");
                kit.transform.position = transform.position;
                kit.transform.forward = transform.forward;
                kit.SetActive(true);
                kit.GetComponent<Kit>().AfterEnable();
            }
        }
    }


}
