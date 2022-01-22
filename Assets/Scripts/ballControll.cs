using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballControll : MonoBehaviour
{
    private float hitPower = 2;
    private Rigidbody2D rb2d;

    [SerializeField]
    private Transform arrow = null;
    private Vector2 mousePosA;
    private Vector2 mousePosB;

    [SerializeField]
    private PhysicsMaterial2D pm2d = null;
    //skills
    [SerializeField]
    private Toggle power;
    [SerializeField]
    private Toggle bomb;
    [SerializeField]
    private Toggle unholeable;
    [SerializeField]
    private Toggle stickyWall;

    // Start is called before the first frame update
    void Start()
    {
        power.isOn = false;
        bomb.isOn = false;
        unholeable.isOn = false;
        stickyWall.isOn = false;
        rb2d = this.GetComponent<Rigidbody2D>();
        arrow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosA = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 nowPos = Input.mousePosition;
                Vector2 nowAng = (nowPos - mousePosA);
                float dis = Vector2.Distance(mousePosA, nowPos);
                float rotAng = Mathf.Atan2(nowAng.y, nowAng.x) * Mathf.Rad2Deg;
                Quaternion qua = Quaternion.Euler(new Vector3(0, 0, rotAng + 90));
                if (dis > 10)
                {
                    arrow.gameObject.SetActive(true);
                    arrow.localRotation = qua;
                    arrow.localScale = new Vector3(arrow.localScale.x, 0.5f + (dis / 1000), arrow.localScale.z);
                }
                else
                {
                    arrow.gameObject.SetActive(false);
                }
            }
            //Œ‚‚Â
            if (Input.GetMouseButtonUp(0))
            {
                arrow.gameObject.SetActive(false);
                mousePosB = Input.mousePosition;
                Vector2 ang = (mousePosB - mousePosA).normalized * -1;
                float dis = Vector2.Distance(mousePosA, mousePosB);
                if (dis < 100 && dis > 10) { dis = 100; }
                if (dis > 400) { dis = 400; }


                Debug.Log("dis:" + dis);
                if (dis >= 10)
                {
                    onShotSkill();
                    GameManager.gameStart = true;
                    dis *= hitPower;
                    rb2d.AddForce(ang * dis);
                }

            }
        }



    }
    void onShotSkill()
    {
        if (power.isOn)
        {
            rb2d.mass = 10;
            hitPower = 200;
        }
        else
        {

            rb2d.mass = 0.4f;
            hitPower = 2;
        }
        if (stickyWall.isOn)
        {
            pm2d.friction = 100;
        }
        else
        {

            pm2d.friction = 0 ;
        }
        if (bomb.isOn)
        {
            this.transform.localScale = new Vector2(1.2f, 1.2f);
        }
        else
        {

            this.transform.localScale = new Vector2(0.6f, 0.6f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!unholeable.isOn)
        {
            if (other.gameObject.tag == "hole")
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Destroy(this.gameObject);
                GameManager.islose = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {

            

        }
    }
}
