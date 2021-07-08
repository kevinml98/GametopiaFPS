using UnityEngine;

public class GunShot : MonoBehaviour {

    // Atributos del disparo.
    private bool hasShot;
    public Transform cameraTransform;
    private float shootDistance = 17f;
    private RaycastHit shootInfo;
    public LayerMask shootMask;

    // Partículas del disparo y golpeo.
    public ParticleSystem shootParticles;
    public GameObject hitEffect;

    // Impacto del disparo.
    private Rigidbody hitRigidbody;
    private float impactForce = 170f;
    public GameObject destroyEffect;

    /**
     * Update is called once per frame.
     */
    private void Update() {
        ReadInputs();
        if (hasShot) {
            Shoot();
        }
    }

    /**
     * Se leen los Inputs horizontales y verticales del usuario.
     */
    private void ReadInputs() {
        hasShot = Input.GetButtonDown("Fire1");
    }

    /**
     * Implementación del comportamiento del disparo.
     */
    private void Shoot() {
        // Se muestra la partícula de disparo.
        shootParticles.Play();

        // Si golpea a algún objeto se aplica el efecto en el punto golpeado.
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out shootInfo, shootDistance, shootMask)) {
            Instantiate(hitEffect, shootInfo.point, Quaternion.LookRotation(shootInfo.normal), shootInfo.transform);

            // Si el objeto golpeado tiene Rigidbody se le aplica una fuerza en sentido negativo al vector normal del impacto.
            hitRigidbody = shootInfo.collider.GetComponent<Rigidbody>();
            if (hitRigidbody != null) {
                hitRigidbody.AddForce(-shootInfo.normal * impactForce);
            }

            // Si el objeto golpeado es "Barrel" lo hacemos explotar.
            if (shootInfo.collider.CompareTag("Barrel")) {
                Instantiate(destroyEffect, shootInfo.point, Quaternion.LookRotation(shootInfo.normal));
                Destroy(shootInfo.collider.gameObject);
                LevelManager.instance.barrelCounter++;
            }
        }
    }

}
