using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeedMultiplier = 30f; // Incremento de velocidad durante el dash
    public float dashDuration = 3f; // Duración del dash en segundos
    private float originalSpeed, dashSpeed; // Para almacenar la velocidad original
    private PlayerMovement playerMovement;
    public ParticleSystem dashParticles;
    public float shrinkDuration = 1f; // Duración de la reducción del tamaño
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dash"))
        {
            originalSpeed = playerMovement.forwardSpeed;
            dashSpeed = (originalSpeed * dashSpeedMultiplier);
            other.gameObject.SetActive(false);
            StartCoroutine(DashCoroutine());
            StartCoroutine(ShrinkAndDestroy(other.gameObject));
        }
    }

    IEnumerator DashCoroutine()
    {
        Debug.Log("dash");
        playerMovement.isBall = true;

        // Aumentar la velocidad
        playerMovement.forwardSpeed += dashSpeed;
        dashParticles.Play();
        // Esperar durante la duración del dash
        yield return new WaitForSeconds(dashDuration);

        // Reducir gradualmente la velocidad de nuevo a la original
        float slowDownDuration = 3f; // Duración para desacelerar
        float elapsedTime = 0f;
        while (elapsedTime < slowDownDuration)
        {
            playerMovement.forwardSpeed = Mathf.Lerp(playerMovement.forwardSpeed, originalSpeed, elapsedTime / slowDownDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la velocidad sea exactamente la original al final
        playerMovement.forwardSpeed = originalSpeed;
    }
    IEnumerator ShrinkAndDestroy(GameObject dashObject)
    {
        Vector3 originalScale = dashObject.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            dashObject.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que el objeto esté completamente reducido
        dashObject.transform.localScale = Vector3.zero;
        Destroy(dashObject);
    }
}
