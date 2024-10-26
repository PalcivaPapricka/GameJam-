using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clippy : MonoBehaviour
{
    // Movement attributes
    private Rigidbody2D rb;
    private float dirX;
    private float dirY;
    private float speed;


    

    // Dash attributes
    private float startDashTime = 0.2f;
    private float dashSpeed = 25f;   
    private float currentDashTime;
    private bool canDash = true;

    public bool is_sprinting=false;

    public bool rollcounter;

    public int dmgtaken=0;
    private bool cantakedmg=true;
    private int damage_value;

    private scrip stamina_bar ;
    [SerializeField] GameObject Stamina;
    scrip stm ;

    // Shooting attributes
    [SerializeField] GameObject fireball; // Fireball prefab
    public float speedarr = 15f; // Speed of the fireball
    private Coroutine shootingCoroutine; // Reference to shooting coroutine

    // Coins
    public int coins = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 6f; // Set initial speed
        stm = Stamina.GetComponent<scrip>();
    }

    void Update()
    {
        GetMovement();
        GetRoll();

        // Shooting logic
        if (Input.GetMouseButtonDown(0) && shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(ShootFireball());
        }

        if (Input.GetMouseButtonUp(0) && shootingCoroutine != null )
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    private void GetRoll()
    {

        if (canDash && Input.GetMouseButtonDown(1) && stm.player_stamina > 20f)
        {
            Vector2 direction = new Vector2(dirX, dirY).normalized; // Normalize direction based on input

            if (direction != Vector2.zero) // Check if there is a direction
            {
                StartCoroutine(Dash(direction));
            }
        }
    }

    private IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        currentDashTime = startDashTime; // Reset the dash timer

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime; // Lower the dash timer each frame
            rb.linearVelocity = direction * dashSpeed; // Dash in the direction

            yield return null; // Return to the coroutine this frame
        }

        rb.linearVelocity = Vector2.zero; // Stop dashing
        canDash = true;
        rollcounter = true;
    }

    private void GetMovement()
    {
        // Basic movement
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(dirX * speed, dirY * speed); // Set movement velocity
    }

    private IEnumerator ShootFireball()
    {
        while (true) // Run indefinitely until stopped
        {
            if(canDash!=false)
            {
                Vector3 shootDirection = Input.mousePosition;
                shootDirection.z = 0.0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection - transform.position;

                GameObject arrow_fired = Instantiate(fireball, transform.position, Quaternion.identity);
                arrow_fired.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shootDirection.x, shootDirection.y).normalized * speedarr;

                // Rotate the fireball to face the shooting direction
                float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
                arrow_fired.transform.rotation = Quaternion.Euler(0f, 0f, angle);

                // Destroy the projectile after 5 seconds
                Destroy(arrow_fired, 5f);

                // Wait for 0.5 seconds before shooting again
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0f);  
        }
    }
}
