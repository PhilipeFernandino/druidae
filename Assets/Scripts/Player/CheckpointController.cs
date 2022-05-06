using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public Transform firstCheckpoint;
    private Vector2 lastCheckpoint;

    private void Awake() {
        lastCheckpoint = firstCheckpoint.position;    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Checkpoint")) {
            lastCheckpoint = other.transform.position;
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Hazard")) {
            BackToCheckpoint();
        }
    }

    private void BackToCheckpoint() {
        transform.position = lastCheckpoint;
    }
}
