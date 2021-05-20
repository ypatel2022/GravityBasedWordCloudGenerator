using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudWord : MonoBehaviour
{
    // components
    Rigidbody2D rb;
    ContentSizeFitter contentSizeFitter;
    RectTransform rectTransform;
    BoxCollider2D boxCollider2D;

    // the center
    private GameObject center;

    // rotation of the word
    float rotationAngle = 0f;

    [Header("Gravity")]
    // Distance where gravity works
    [Range(0.0f, 1000.0f)]
    public float maxGravDist = 150.0f;

    // Gravity force
    [Range(0.0f, 1000.0f)]
    public float maxGravity = 150.0f;


    [Header("Time to wait until words freeze.")]
    public float WaitToFreezeSeconds = 4f;

    void Start()
    {
        // get components to store
        rb = GetComponent<Rigidbody2D>();
        contentSizeFitter = GetComponent<ContentSizeFitter>();
        rectTransform = GetComponent<RectTransform>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        // to set the size of the rigid body
        StartCoroutine(SetRBSize());
        // SetRigidbodySize();

        // find and store center point
        center = GameObject.Find("Center");

        // will freeze position of the word after some time
        StartCoroutine(FreezePosition());
    }



    void Update()
    {
        // Distance to the center
        float dist = Vector3.Distance(center.transform.position, transform.position);

        // move word towards the center
        Vector3 v = center.transform.position - transform.position;
        rb.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);

        // set rotation of word
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    // to set the size of the rigid body
    IEnumerator SetRBSize()
    {
        // coroutine because normal function wont work 
        yield return null;

        // disable because you cant access size with it on
        contentSizeFitter.enabled = false;

        // set the size
        boxCollider2D.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);

        // add offset so that the collider is in the center of the word
        boxCollider2D.offset = new Vector2(GetComponent<RectTransform>().rect.width / 2f, GetComponent<RectTransform>().rect.height / 2f);

        // chance to make word vertical if it's height is smaller than 20
        if (boxCollider2D.size.y < 30f)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                rotationAngle = 90f;
            }
        }

        // reenable componenet
        contentSizeFitter.enabled = true;
    }

    // wait to freeze position
    IEnumerator FreezePosition()
    {
        yield return new WaitForSeconds(WaitToFreezeSeconds);

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
