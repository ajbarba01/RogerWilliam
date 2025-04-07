using UnityEngine;

public class SalamiSpinner : MonoBehaviour
{
    public float damageAmount = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //PlayerHealth2D playerHealth = collision.GetComponent<PlayerHealth2D>();
           // if (playerHealth != null)
           // {
             //   playerHealth.TakeDamage(damageAmount);
            //}
        }
    }[Header("Spinner Settings")]
    public Transform spinner;              // The child GameObject that spins
    public float rotationSpeed = 200f;     // Rotation speed of the spinner
    public float orbitRadius = 1.5f;       // How far from the enemy it spins
    public float orbitSpeed = 2f;          // Orbit speed around the enemy

    private float angle;

    void Update()
    {
        if (spinner == null) return;

        // Orbit calculation (2D - x and y only)
        angle += orbitSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * orbitRadius;
        float y = Mathf.Sin(angle) * orbitRadius;

        spinner.localPosition = new Vector3(x, y, 0f);

        // Spin the object around its own axis
        spinner.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}