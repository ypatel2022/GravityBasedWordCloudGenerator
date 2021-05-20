using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityBody : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 lookDirection;

    float lookAngle;

    bool freezePosition = false;

    [Header("Gravity")]

    // Distance where gravity works
    [Range(0.0f, 1000.0f)]
    public float maxGravDist = 150.0f;

    // Gravity force
    [Range(0.0f, 1000.0f)]
    public float maxGravity = 150.0f;

    // Your planet
    public GameObject planet;

    Vector2 size = new Vector2();

    public int UpOrSideways = 0;
    public bool setRotation = false;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(SetRBSize());

        planet = GameObject.Find("Gravity Body");

        StartCoroutine(FreezePosition());
    }

    void Update()
    {
        if (!freezePosition)
        {
            if (!setRotation && GetComponent<BoxCollider2D>().size.y < 20f)
            {
                UpOrSideways = Random.Range(0, 2);

                setRotation = true;
            }

            // Distance to the planet
            float dist = Vector3.Distance(planet.transform.position, transform.position);

            // Gravity
            Vector3 v = planet.transform.position - transform.position;
            rb.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);

            // Rotating to the planet
            lookDirection = planet.transform.position - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;


            if (setRotation)
            {
                if (UpOrSideways == 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);//lookAngle);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    IEnumerator SetRBSize()
    {
        yield return new WaitForSeconds(0.01f);

        GetComponent<ContentSizeFitter>().enabled = false;

        size = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);

        GetComponent<BoxCollider2D>().size = size;

        GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<RectTransform>().rect.width / 2f, GetComponent<RectTransform>().rect.height / 2f);

        GetComponent<ContentSizeFitter>().enabled = true;
    }

    public float WaitToFreezeSeconds = 4f;

    IEnumerator FreezePosition()
    {
        yield return new WaitForSeconds(4f);

        // GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // freezePosition = true;
    }
}
