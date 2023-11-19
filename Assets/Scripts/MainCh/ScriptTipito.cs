using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScriptTipito : MonoBehaviour
{
    public Transform bulletspawnpoint;
    public GameObject balitaPrefab;
    private int numBalas = 999;
    public float elcooldownbala = 1f;
    public float eltiemporestantebala = 0f;

    public float elcooldownespada = 0.1f;
    public float eltiemporestanteespada = 0f;


    [SerializeField] private float speed;
    Animator myAnimator;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myCapsuleCollider;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        direction = new Vector2 (-1, -1);
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Shoot();
        Sword();
        if (eltiemporestantebala>0)
        {
            eltiemporestantebala -= Time.deltaTime;
        }

        if (eltiemporestanteespada > 0)
        {
            eltiemporestanteespada -= Time.deltaTime;
        }

        print(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
    }

    public bool canShoot()
    {
        if (eltiemporestantebala <= 0)
        {
            

            // Iniciar el cooldown
            eltiemporestantebala = elcooldownbala;

            return true;
        }
        else return false;
    }
    public bool canSword()
    {
        if (eltiemporestanteespada <= 0)
        {


            // Iniciar el cooldown
            eltiemporestanteespada = elcooldownespada;

            return true;
        }
        else return false;
    }

    private void Mover()
    {
        float movimientox = Input.GetAxis("Horizontal");
        float movimientoy = Input.GetAxis("Vertical");
        transform.localScale = new Vector2(Mathf.Sign(movimientox), 1);
        transform.Translate(new Vector2 (movimientox*speed*Time.deltaTime,0));
        transform.Translate(new Vector2(0, movimientoy * speed * Time.deltaTime));

        if (movimientoy !=0)
        {
            myAnimator.SetBool("MovY", true);
            if (movimientoy > 0)
            {
                myAnimator.SetBool("DirY", true);
                direction.y = 1;
            }
            else
            {
                myAnimator.SetBool("DirY", false);
                direction.y = -1;
            }
        }
        else
        {
            myAnimator.SetBool("MovY", false);
        }

        if(movimientox !=0)
        {
            myAnimator.SetBool("MovX", true);
        }
        else
        {
            myAnimator.SetBool("MovX", false);

        } 
    }

    private void Shoot()
    {
        
        
        if (Input.GetKeyDown(KeyCode.K) && numBalas>0 && canShoot()==true)
        {
            myAnimator.SetBool("IsAttacking", true);
            GameObject _bala = Instantiate(balitaPrefab, bulletspawnpoint.position, bulletspawnpoint.rotation);

            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")== true || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walkfront") == true)
            {
                _bala.GetComponent<Rigidbody2D>().velocity = Vector2.down * GunScript.GetSpeed();
            }
            else if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walkback") == true)
            {
                _bala.GetComponent<Rigidbody2D>().velocity = Vector2.up * GunScript.GetSpeed();
            }
            else if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walkside") == true)
            {
                _bala.GetComponent<Rigidbody2D>().velocity = Vector2.right* (Input.GetAxis("Horizontal") > 0? 1: -1) * GunScript.GetSpeed();
            }
            numBalas--;
        }
        else
        {
            myAnimator.SetBool("IsAttacking", false);
        }
    }

    private void Sword()
    {
        if (Input.GetKeyDown(KeyCode.J) && canSword() == true)
        {
            myAnimator.SetBool("IsSwording", true);

            //GameObject espadita = Instantiate(balitaPrefab, bulletspawnpoint.position, bulletspawnpoint.rotation);

        }
        else myAnimator.SetBool("IsSwording", false);
    }

    
}
