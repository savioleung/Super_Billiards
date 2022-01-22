using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balls : MonoBehaviour
{
    Transform hole;
    bool holed = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "hole")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            hole = other.gameObject.transform;
            GoToHole();
            holed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "table")
        {
            if (!holed) { this.gameObject.transform.position = new Vector3(0, 0, 100); Debug.Log("ball out of table"); }
        }
    }
    private void Update()
    {
        
        if (holed)
        {
            Vector2 v2 = this.transform.localScale;
            v2 *= 0.9f;
            this.transform.localScale = v2; 
            transform.position = Vector3.MoveTowards(transform.position, hole.transform.position, 0.05f);
        }
    }
    void GoToHole()
    {
        Destroy(this.gameObject, 0.5f);
        this.GetComponent<Collider2D>().isTrigger = true;
    }
}
