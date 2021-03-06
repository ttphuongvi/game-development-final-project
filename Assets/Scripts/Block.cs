using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    public float maxHeath = 100;
    [ShowOnly]
    public float health;
    public Sprite[] spriteArray;
    
    //Audio 
    public AudioClip[] audioClipCollision, audioClipDamge, audioClipDestroy;
    [HideInInspector]
    public AudioSource audioSource;

    public GameObject[] shapeDetroy;
    public float distanceDetroyShape;
    [HideInInspector]
    public GameObject gameManager;
    [HideInInspector]
    public int indexLevel;
    public void changeSprite(int index)
    {
        GetComponent<SpriteRenderer>().sprite = spriteArray[index];
    }

    public void ChangeAudioClipCollisionRandom() {
        audioSource.clip = audioClipCollision[Random.Range(0, audioClipCollision.Length)];
    }

    public void ChangeAudioClipDamgeRandom() {
        audioSource.clip = audioClipDamge[Random.Range(0, audioClipDamge.Length)];
        // Debug.Log(audioClipDamge.Length);
    }

    public void ChangeAudioClipDestroyRandom() {
        audioSource.clip = audioClipDestroy[Random.Range(0, audioClipDestroy.Length)];
    }

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // Tính damage, detroy nếu hết máu
        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 100;

        // Add Score
        if (gameManager)
            gameManager.GetComponent<IngameMenu>().currentScore += (int)damage;

        float oldHealth = health;
        health -= damage;
        if (health <= 0) {
            ChangeAudioClipDestroyRandom();
            audioSource.Play();
            if (gameManager)
                gameManager.GetComponent<IngameMenu>().currentScore += 500;
            GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            for (int index = 0; index <= 20; index++) {
                // Instance random shapeDestroy
                GameObject newShape = Instantiate(shapeDetroy[Random.Range(0, shapeDetroy.Length)], transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject, 2f);
        }
        // Debug.Log(damage);

        // Check máu chuyển đổi Sprite

        if (health <= maxHeath * 0.25f && oldHealth > maxHeath * 0.25f) {
            ChangeAudioClipDamgeRandom();
            audioSource.Play();
            changeSprite(3);
        }
        else if (health <= maxHeath * 0.5f && oldHealth > maxHeath * 0.5f) {
            ChangeAudioClipDamgeRandom();
            audioSource.Play();
            changeSprite(2);
        }
        else if (health <= maxHeath * 0.75f && oldHealth > maxHeath * 0.75f) {
            ChangeAudioClipDamgeRandom();
            audioSource.Play();
            changeSprite(1);
        }
        else {
            ChangeAudioClipCollisionRandom();
            audioSource.Play();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHeath;
        audioSource = gameObject.AddComponent<AudioSource>();

        // Get Game Manager
        gameManager = GameObject.Find("UIIngame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 RandomVector(Vector3 myVector, Vector3 min, Vector3 max)
     {
        return myVector + new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
     }
}
