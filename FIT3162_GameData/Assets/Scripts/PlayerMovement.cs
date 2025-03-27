using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 1.0f;

  private void Update()
  {
    
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
        this.transform.position += Vector3.left * this.speed * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
        this.transform.position += Vector3.right * this.speed * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
        this.transform.position += Vector3.up * this.speed * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
        this.transform.position += Vector3.down * this.speed * Time.deltaTime;
    }
  }
}
