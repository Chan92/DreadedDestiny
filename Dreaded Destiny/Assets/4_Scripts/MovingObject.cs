using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    [SerializeField] private float moveSpeedMin = 0.8f, moveSpeedMax = 5f, cooldownMin = 0.2f, cooldownMax = 2f;
    private float moveSpeed;
    private Rigidbody2D rb;


    void Start() {
        rb = transform.GetComponent<Rigidbody2D>();        
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        transform.position = SetRandomPosition();
        StartCoroutine(Movement());
    }

	private void Update() {
        rb.velocity = rb.GetRelativeVector(Vector2.up * moveSpeed);
    }

	IEnumerator Movement() {
        while(true) {
            //TODO: lerp rotation
            rb.rotation = Random.Range(0f, 360f);
            float cooldown = Random.Range(cooldownMin, cooldownMax);
            yield return new WaitForSeconds(cooldown);
        }
    }

   private Vector2 SetRandomPosition() {
        float x = Random.Range(-9.5f, 9.5f);
        float y = Random.Range(-4.5f, 4.5f);
        return new Vector2(x, y);
    }

    public void Dissapear() {
        StopCoroutine(Movement());
        gameObject.SetActive(false);
    }

	private void OnBecameInvisible() {
        Dissapear();
    }

	private void OnDestroy() {
        StopCoroutine(Movement());
	}
}
