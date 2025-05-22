using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletSprite;
    public float minRotate;
    public float maxRotate;
    public int numOfBullets;
    public bool isRandom;

    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;

    float[] rotations;

    void Start() {
        timer = cooldown;
        rotations = new float[numOfBullets];
        if (!isRandom) {
            DistributedRotations();
        }
    }

    void Update() {
        if (GameController.gameIsOver) return;
        if (timer <= 0) {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }

    public float[] RandomRotations() {
        for (int i = 0; i < numOfBullets; i++ ) {
            rotations[i] = Random.Range(minRotate, maxRotate);
        }
        return rotations;
    }

    public float[] DistributedRotations() {
        for (int i = 0; i < numOfBullets; i++) {
            var fraction = (float)i / ((float)numOfBullets -1);
            var difference = maxRotate - minRotate;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + minRotate;
        }
        foreach (var r in rotations) print(r);
        return rotations;
    }

    public GameObject[] SpawnBullets() {
        if (isRandom) {
            RandomRotations();
        }
        GameObject[] spawnedBullets = new GameObject[numOfBullets];
        for (int i = 0; i < numOfBullets; i++) {
            spawnedBullets[i] = Instantiate(bulletSprite, transform.position, Quaternion.identity);
            var b = spawnedBullets[i].GetComponent<BossBullet>();
            b.rotation = rotations[i];
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;
        }
        return spawnedBullets;
    }

}
