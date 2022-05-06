using UnityEngine;

public class RaycastRenderer : MonoBehaviour {
    
    public Transform spawnPoint; 
    public LayerMask raycastMask;
    public float lineRendererDistance;

    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() {
        if (Input.GetButton("Fire1")) {
            CastRay();
        } else lineRenderer.enabled = false;
    }

    private void CastRay() {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, spawnPoint.position);
        
        Vector2 mouseInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - spawnPoint.position;
        RaycastHit2D hit = Physics2D.Raycast(spawnPoint.position, mouseInWorldPos, 100f, raycastMask);
        
        if (hit) lineRenderer.SetPosition(1, hit.point);
        else lineRenderer.SetPosition(1, mouseInWorldPos * lineRendererDistance);

        if (hit) {
            IWandable iw = hit.collider.gameObject.GetComponent<IWandable>();
            if (iw != null) {
                iw.Wand();
            }
        }
    }

}
