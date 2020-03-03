using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Player : Character
{
    public Rigidbody rigid;
    float mouseSensitivity;
    public float moveSpeed =0.2f;
    public float jumpForce = 3000f;
    float currentDashDistance = 0f;
    public Vector3 oldDirection;
    bool jumping = false;
    public Transform parentTransform;
    float cameraSensitivity = 2f;
    Vector3 lastPos;
    float distToGround;
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
            healthBar.value = healthPoints;
            if (healthPoints <= 0)
            {
                audioSource.clip = deathSound;
                audioSource.Play();
                GameManager.handlePlayerDeath();
            }
            else
            {
                if (oldHealthPoints > healthPoints)
                {
                    audioSource.clip = takeDamageSound;
                    audioSource.Play();
                }
                if (oldHealthPoints < healthPoints)
                {
                    if (healthPoints > playerSheet.health)
                    {
                        healthPoints = playerSheet.health;
                    }
                }



            }
        }
    }
    public PlayerSheet playerSheet;
    bool cooldownFinished = true;
    public Slider healthBar;
    public Transform arrowTransform;
    GameObject playerCamera;
    public MeshRenderer arrowRenderer;
    int ammo;
    public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            int oldAmmo = ammo;
            ammo = value;
            ammoText.text = ammo.ToString();


        }
    }
    public Text ammoText;
    int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }
    public Text scoreText;
    public int enemiesKilled;



    new void Start(){
        base.Start();
        GameObject theParent = transform.parent.gameObject;
        parentTransform = theParent.transform;
        rigid = gameObject.GetComponentInParent(typeof(Rigidbody)) as Rigidbody;
        mouseSensitivity = cameraSensitivity;
        lastPos = transform.position;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        healthPoints = playerSheet.health;
        moveSpeed = playerSheet.speed;
        playerCamera = GameObject.Find("Main Camera");
        team = Character.Team.Player;
        ammo = playerSheet.startingAmmo; //to stop sound
        Ammo = playerSheet.startingAmmo;
        Cursor.lockState = CursorLockMode.Locked;
        enemiesKilled = 0;
    }

    void FixedUpdate()
    {

       

        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity * 2, 0)));

        if (rigid.velocity.y == 0) jumping = false; else jumping = true;


        CheckWalk();
        if (IsGrounded())
            CheckJump();

        if (Input.GetMouseButtonDown(0))
        {

            if (cooldownFinished)
            {
                ShootArrow();
                


            }
        }
    }

    IEnumerator ArrowCooldown()
    {
        
        yield return new WaitForSeconds(playerSheet.attackCooldown);

        while (Ammo <= 0)
        {
            yield return new WaitForSeconds(0.1f);
        }
        cooldownFinished = true;
        arrowRenderer.enabled = true;
    }

    void ShootArrow()
    {
        Ammo--;
        cooldownFinished = false;
        arrowRenderer.enabled = false;
        GameObject arrow = PoolManager.GetObject("Arrow");
        arrow.transform.position = arrowTransform.position;
        arrow.transform.forward = playerCamera.transform.forward;
        arrow.SetActive(true);
        arrow.GetComponent<Arrow>().AfterEnable(Character.Team.Player,playerSheet.damage);
        StartCoroutine(ArrowCooldown());
    }

    bool IsGrounded(){
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }  


    void CheckWalk(){
        rigid.MovePosition(parentTransform.position + (parentTransform.forward * Input.GetAxis("Vertical") * moveSpeed) + (parentTransform.right * Input.GetAxis("Horizontal") * moveSpeed));
    }

    void CheckJump(){
        if (Input.GetButtonDown("Jump")){
            rigid.AddForce(parentTransform.up * jumpForce);
        }

    }


}
