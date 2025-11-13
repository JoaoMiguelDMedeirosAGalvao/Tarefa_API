using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public class script : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public int vida = 5;
    public int items = 0;
    public float posY;
    public float posX;
    public float posZ;
    public GameObject texto;
    public TextMeshProUGUI textoItems;
    public TextMeshProUGUI textoPosY;
    public TextMeshProUGUI textoPosZ;
    public TextMeshProUGUI textoPosX;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoSave;
    public GameApiSersce API;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical);

        posX = transform.position.x;
        posZ = transform.position.z;
        posY = transform.position.y;

        if(Input.GetKeyDown(KeyCode.Escape) && texto)
        {
            texto.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && texto == true)
        {
            texto.SetActive(false);
        }

        if (vida <= 0)
        {
            Destroy(gameObject);
        }

        if(textoItems)
        {
          textoItems.text =  items.ToString();
        }
        if(textoPosX)
        {
            textoPosX.text = posX.ToString();
        }
        if(textoPosY)
        {
            textoPosY.text = posY.ToString();
        }
        if(textoPosZ)
        {
            textoPosZ.text = posZ.ToString();
        }
        if(textoVida)
        {
            textoVida.text = vida.ToString();
        }
    }
    private void FixedUpdate()
    {
        Vector3 movePosition = (speed * Time.fixedDeltaTime * moveDirection.normalized) + rb.position;
        rb.MovePosition(movePosition);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "item")
        {
            items++;
            Destroy(collision.gameObject);
            SavePlayer();
        }
        if (collision.gameObject.tag == "cacto")
        {
            vida--;
        }
    }

    public void SavePlayer()
    {
        if (API == null)
        {
            API = new GameApiSersce();
        }
        var jogador = new script
        {
            vida = this.vida,
            items = this.items,
            posX = this.posX,
            posY = this.posY,
            posZ = this.posZ
        };
     
        if (textoSave)
        {
            textoSave.text = "Jogo Salvo!";
        }
    }
}
