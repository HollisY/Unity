using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;  //单色蛇身移动用

public class SnakeHead : MonoBehaviour
{
    public float velocity = 0.35f;
    public int step;
    public List<Transform> lstBody = new List<Transform>();
    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];
    public GameObject explosionEffect;
    public AudioClip eatClip;
    public AudioClip dieClip;

    private int x;
    private int y;
    private Vector3 headPos;
    private Transform canvas;
    private bool isDead;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;

        if (PlayerPrefs.GetString("SnakeHead", "sh01") == "sh01")
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("SnakeHead", "sh01"));
            bodySprites[0] = Resources.Load<Sprite>("sb0101");
            bodySprites[1] = Resources.Load<Sprite>("sb0102");
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("SnakeHead", "sh02"));
            bodySprites[0] = Resources.Load<Sprite>("sb0201");
            bodySprites[1] = Resources.Load<Sprite>("sb0202");
        }
    }

    private void Start()
    {
        InvokeRepeating("Move", 0, velocity);
        x = 0;
        y = step;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && MainUI.Instance.isPause == false && isDead == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity - 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && MainUI.Instance.isPause == false && isDead == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }
        if (Input.GetKey(KeyCode.W) && (y != -step) && MainUI.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;
            y = step;
        }
        if (Input.GetKey(KeyCode.A) && (x != step) && MainUI.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }
        if (Input.GetKey(KeyCode.S) && (y != step) && MainUI.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }
        if (Input.GetKey(KeyCode.D) && (x != -step) && MainUI.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }
    }

    void Move()
    {
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);
        if (lstBody.Count > 0)
        {
            //单色蛇身移动方法
            //lstBody.Last().localPosition = headPos;
            //lstBody.Insert(0, lstBody.Last());
            //lstBody.RemoveAt(lstBody.Count - 1);

            //多色蛇身移动方法
            for (int i = lstBody.Count - 2; i >= 0; i--)
            {
                lstBody[i + 1].localPosition = lstBody[i].localPosition;
            }
            lstBody[0].localPosition = headPos;
        }
    }

    private void Grow()
    {
        AudioSource.PlayClipAtPoint(eatClip, Vector3.zero);
        int index = (lstBody.Count % 2 == 0) ? 0 : 1;
        GameObject body = Instantiate(bodyPrefab, new Vector3(1000, 1000, 0), Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[index];
        body.transform.SetParent(canvas, false);
        lstBody.Add(body.transform);
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(dieClip, Vector3.zero);
        CancelInvoke();
        isDead = true;
        Instantiate(explosionEffect);
        PlayerPrefs.SetInt("LastScore", MainUI.Instance.score);
        if (PlayerPrefs.GetInt("BestScore", 0) < MainUI.Instance.score)
        {
            PlayerPrefs.SetInt("BestScore", MainUI.Instance.score);
        }
        StartCoroutine(GameOver(1.5f));
    }

    IEnumerator GameOver(float sec)
    {
        yield return new WaitForSeconds(sec);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")    // == if (collision.gameObject.CompareTag("Food"))      //不常用第2种
        {
            Destroy(collision.gameObject);
            MainUI.Instance.UpdateUI();
            Grow();
            FoodMaker.Instance.MakeFood(Random.Range(0, 100) < 20 ? true : false);
        }
        else if (collision.tag == "Reward")
        {
            Destroy(collision.gameObject);
            MainUI.Instance.UpdateUI(Random.Range(1, 20) * 5);
            Grow();
        }
        else if (collision.tag == "SnakeBody")
        {
            Die();
        }
        else
        {
            if (MainUI.Instance.hasBorder)
            {
                Die();
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                        break;
                    case "Left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "Right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 240, transform.localPosition.y, transform.localPosition.z);
                        break;
                }
            }
        }
    }
}
