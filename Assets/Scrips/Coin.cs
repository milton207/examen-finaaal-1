using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound; // Aquí arrastraremos el sonido desde el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduce el sonido en la posición exacta de la moneda antes de destruirla
            if (coinSound != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }

            // Suma la moneda al GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin();
            }

            // Destruye la moneda
            Destroy(gameObject);
        }
    }
}