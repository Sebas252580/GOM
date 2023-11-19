using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //public Transform bulletspawnpoint;
    //public GameObject balitaPrefab;
    public static float balitaSpeed = 10f;
    public float lifespan = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(vamoAMori());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    GameObject _bala = Instantiate(balitaPrefab, bulletspawnpoint.position, bulletspawnpoint.rotation);
        //    _bala.GetComponent<Rigidbody2D>().velocity = bulletspawnpoint.up * balitaSpeed;
        //}
    }

    public static float GetSpeed()
    {
        return balitaSpeed;
    }

    IEnumerator vamoAMori()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
