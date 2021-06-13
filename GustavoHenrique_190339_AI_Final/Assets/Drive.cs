using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour
{
    //criação das variaveis
    float speed = 20.0F;
    float rotationSpeed = 120.0F;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Slider healthBar;
    float health = 100.0f;
    public Transform respawn;

    void Update()
    {
        //faz ele usar os inputs setado na vertical
        float translation = Input.GetAxis("Vertical") * speed;
        //faz ele usar os inputs setado na horizontal
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        //faz o calculo pra ele se mover nas verticais com o deltaTime
        translation *= Time.deltaTime;
        //faz o calculo pra ele se mover nas horizontais com o deltaTime
        rotation *= Time.deltaTime;
        //fala o eixo que o translation vai interferir no caso o z
        transform.Translate(0, 0, translation);
        //fala o eixo que o rotation vai interferir no caso o y
        transform.Rotate(0, rotation, 0);
        //ao apertar a barra de espaço atira um projetiol
        if (Input.GetKeyDown("space"))
        {
            //instancia o projetil
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            //da a força de tiro do projetil
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 2000);
        }
        //posiciona a barra de vida de acordo com a camera
        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //seta o valor da vida como a variavel health
        healthBar.value = (int)health;
        //coloca a barra de vida em cima do npc
        healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);
        //caso a visa seja igual ou menor que 0
        if (health <= 0)
        {
            //vida volta a ser 100
            health = 100;
            //coloca o player na posição do respawn
            transform.position = respawn.position;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        //caso colida com um projetil perca vida
    if (col.gameObject.tag == "bullet")
        {
            health -= 10;
        }
    }
    //função de tomar dano
    public void Damege(float damage)
    {
        //caso tome dano subitraia a vida
        health -= damage;
    }
}

