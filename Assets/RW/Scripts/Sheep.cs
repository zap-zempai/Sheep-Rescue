using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; 
    public float gotHayDestroyDelay; 
    private bool hitByHay;
    private bool isDrop = false;
    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody;
    private SheepSpawner sheepSpawner;
    public float heartOffset; 
    public GameObject heartPrefab;
    public int points;
    public int life;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        life--;

        SoundManager.Instance.PlaySheepHitClip();

        if (life <= 0)
        {
            sheepSpawner.RemoveSheepFromList(gameObject);

            hitByHay = true;
            runSpeed = 0;

            Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
            TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
            tweenScale.targetScale = 0;
            tweenScale.timeToReachTarget = gotHayDestroyDelay;

            Destroy(gameObject, gotHayDestroyDelay);

            GameStateManager.Instance.SavedSheep(points);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Hay") && !hitByHay) 
        {
            Destroy(other.gameObject); 
            HitByHay(); 
        }
        else if (other.CompareTag("DropSheep") && !isDrop)
        {
            Drop();
        }

    }

    private void Drop()
    {
        SoundManager.Instance.PlaySheepDroppedClip();

        GameStateManager.Instance.DroppedSheep(damage);
        sheepSpawner.RemoveSheepFromList(gameObject);

        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        isDrop = true;
        Destroy(gameObject, dropDestroyDelay);
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}
