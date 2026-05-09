using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private float deadZone = 10f;

    [SerializeField]
    float speed = 0.10f;

    private Rigidbody rb;
    private Vector3 direction;
    float damp = 0.10f;
    private bool canMove = true;
    public GameObject LevTool;
    private float speddLimit = 150f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        var cursorPos = Input.mousePosition;
        var x = Screen.width / 2;
        var y = Screen.height / 2;

        var dirToCursor = new Vector2(cursorPos.x - x, cursorPos.y - y);
        if (dirToCursor.x > speddLimit)
        {
            dirToCursor.x = speddLimit;
        }
        if (dirToCursor.x < -speddLimit)
        {
            dirToCursor.x = -speddLimit;
        }
        if (dirToCursor.y > speddLimit)
        {
            dirToCursor.y = speddLimit;
        }
        if (dirToCursor.y < -speddLimit)
        {
            dirToCursor.y = -speddLimit;
        }
        direction = Vector2.zero;
        if(Vector2.Distance(dirToCursor, Vector2.zero)>deadZone && canMove)
        {
            direction = new Vector3(dirToCursor.x, 0, dirToCursor.y) * -1;
        }
        else
        {
            direction = Vector2.zero;
        }
        rb.velocity = Vector3.Lerp(rb.velocity, direction * speed, damp);

        transform.rotation = Quaternion.Euler(rb.velocity.normalized * -0.5f);
        if (Input.GetMouseButton(0))
        {
            LevTool.GetComponent<CapsuleCollider>().enabled = true;
            if (LevTool.transform.localScale.x < 1.5)
            {
                LevTool.transform.localScale = new Vector3(LevTool.transform.localScale.x + 0.1f, LevTool.transform.localScale.y, LevTool.transform.localScale.z + 0.1f);
            }
        }
        else
        {
            LevTool.GetComponent<CapsuleCollider>().enabled = false;
            LevTool.transform.localScale = new Vector3(0, LevTool.transform.localScale.y, 0);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            direction = new Vector3(other.transform.position.x, 0, other.transform.position.z) * -1;
            rb.velocity = Vector3.Lerp(rb.velocity, direction * speed * 20, damp);
            canMove = false;
            StartCoroutine(WaitForMove());
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, direction * speed, damp); 
        }
    }
    private IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }
}
