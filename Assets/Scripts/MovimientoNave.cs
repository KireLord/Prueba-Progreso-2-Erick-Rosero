using UnityEngine;

public class MovimientoNave : MonoBehaviour
{
    [Header("Ship parameters")]
    [SerializeField] private float shipAcceleration = 10f;
    [SerializeField] private float shipMaxVelocity = 10f;
    [SerializeField] private float shipRotationSpeed = 180f;
    [SerializeField] private float collisionSpeedReduction = 0.5f;
    [SerializeField] private float dampingFactor = 0.98f; // Nuevo parámetro de amortiguación
    [SerializeField] private bool freezeRotationOnCollision = true; // Nueva variable para congelar la rotación

    private Rigidbody2D shipRigidbody;
    private bool isAlive = true;
    private bool isAccelerating = false;
    private bool isDecelerating = false;

    private void Start()
    {
        // Obtén una referencia al RigidBody2D adjunto.
        shipRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAlive)
        {
            HandleShipAcceleration();
            HandleShipDeceleration();
            HandleShipRotation();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            // Ajusta la fuerza aplicada según la dirección (adelante o atrás)
            float force = 0f;

            if (isAccelerating)
            {
                force = shipAcceleration;
            }
            else if (isDecelerating)
            {
                force = -shipAcceleration;
            }

            // Añade fuerza a la nave
            shipRigidbody.AddForce(force * transform.up);

            // Limita la velocidad
            shipRigidbody.velocity = Vector2.ClampMagnitude(shipRigidbody.velocity, shipMaxVelocity);

            // Aplica el factor de amortiguación para reducir gradualmente la velocidad
            shipRigidbody.velocity *= dampingFactor;

            // Si se ha activado freezeRotationOnCollision y hay colisión, congela la rotación
            if (freezeRotationOnCollision && isCollidingWithWall())
            {
                shipRigidbody.freezeRotation = true;
            }
        }
    }

    private void HandleShipAcceleration()
    {
        isAccelerating = Input.GetKey(KeyCode.UpArrow);
    }

    private void HandleShipDeceleration()
    {
        isDecelerating = Input.GetKey(KeyCode.DownArrow);
    }

    private void HandleShipRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
        }
    }

    // Manejo de colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ParedInvisible")
        {
            Debug.Log("Colisión con la pared invisible detectada");
            // Reducir la velocidad del carro al colisionar con la pared invisible.
            shipRigidbody.velocity *= collisionSpeedReduction;

            // Si se ha activado freezeRotationOnCollision, congela la rotación
            if (freezeRotationOnCollision)
            {
                shipRigidbody.freezeRotation = true;
            }

            // Puedes agregar código adicional que se ejecutará cuando haya una colisión con la pared invisible.
            // Por ejemplo, puedes reproducir un sonido, mostrar un efecto visual, etc.
        }
    }

    // Verifica si la nave está colisionando con la pared
    private bool isCollidingWithWall()
    {
        // Implementa la lógica específica para verificar la colisión con la pared si es necesario
        // Puedes usar raycasts, colliders, o cualquier otro método adecuado
        // Devuelve true si hay una colisión, de lo contrario, devuelve false
        return false; // Cambia esto con la lógica de colisión adecuada
    }
    private bool IsObjectInside(Transform target)
    {
        // Verifica si el objeto MetaInvisible está completamente dentro de la nave
        Collider2D shipCollider = GetComponent<Collider2D>();
        Collider2D metaCollider = target.GetComponent<Collider2D>();

        if (shipCollider != null && metaCollider != null)
        {
            // Obtiene los límites de los colliders
            Bounds shipBounds = shipCollider.bounds;
            Bounds metaBounds = metaCollider.bounds;

            // Verifica si los límites de la nave contienen completamente los límites de MetaInvisible
            return shipBounds.Contains(metaBounds.min) && shipBounds.Contains(metaBounds.max);
        }

        return false;
    }

}
