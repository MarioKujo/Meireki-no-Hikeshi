using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]//Para poder modificar valores en el editor de unity
    public static float moveSpeed = 3f;//velocidad de movimiento
    [SerializeField]
    public static float jumpSpeed = 8.5f;//velocidad de salto
    public Transform groundLevel;//Para recibir la posici�n de los pies del jugador
    [SerializeField]
    private LayerMask groundLayer;//M�scara de capa para solamente hacerle caso a los elementos de esa capa en espec�fico
    [SerializeField]
    float distance;//De qu� tama�o hacer el rayo
    [SerializeField]
    LayerMask whatIsLadder;//M�scara de capa para revisar si est� en contacto con una escalera
    [SerializeField]
    float gravityScale;
    [SerializeField]
    float fallGravity;
    [HideInInspector]
    public bool isGrounded = true;//Booleano que revisa si est� tocando el suelo
    public static Animator animator;//Para animar
    [HideInInspector]
    public float x;//Se recibe el movimiento en el eje horizontal
    [HideInInspector]
    public float y;//Se recibe el movimiento en el eje vertical
    private bool isClimbing;//Booleano para revisar si est� escalando
    private Rigidbody2D rb;//Para cambiar la velocidad, el movimiento, el salto, etc.
    public static bool facingRight;
    public bool tirandoAgua;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();//inicializa el valor
        // animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (rb.velocity.y < 0 && !isGrounded)
        {
            rb.gravityScale = fallGravity;
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
        IsGrounded();//revisa si est� en el suelo
        if (!tirandoAgua)
        {
            Move();//Funci�n para moverse
            Jump();//Funci�n para saltar
            Climb();//Funci�n para escalar
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void EndAnimation()
    {
        if (WaterTank.Capacity != 0)
        {
            WaterTank.Capacity--;
        }
        animator.ResetTrigger("Throw");
        WaterTank.isPlaying = false;
        tirandoAgua = false;
    }
    public void Water()
    {
        tirandoAgua = true;
    }
    private void Climb()
    {
        //Se lanza un rayo desde los pies hacia arriba la distancia indicada en el programa con la m�scara de capa indicada
        RaycastHit2D hitInfo = Physics2D.Raycast(groundLevel.position, Vector2.up, distance, whatIsLadder);
        if (hitInfo.collider != null)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isClimbing = true;//Si est� en contacto y pulsa el bot�n, se activa el booleano
            }
        }
        else
        {
            if (x < 0 || x > 0)
            {
                isClimbing = false;//Si no, si detecta que se mueve hacia la derecha o hacia la izquierda, lo convierte en falso
            }
        }
        if (isClimbing && hitInfo.collider != null && WaterTank.Capacity != 3)//Si est� escalando, el rayo detecta algo y la capacidad de agua no es 3
        {
            y = Input.GetAxisRaw("Vertical");//El eje y recibe el input vertical
            rb.velocity = new Vector2(rb.velocity.x, y * moveSpeed);//Se le aplica la velocidad
            rb.gravityScale = 0;//Se elimina la gravedad
        }
        else
        {
            if (animator.GetBool("isFalling") == false)
            {
                rb.gravityScale = gravityScale;//Si no, la gravedad vuelve a ser como antes
            }
        }
    }

    public void Move()
    {
        x = Input.GetAxis("Horizontal");//Se recibe el movimiento en el eje horizontal
        animator.SetFloat("Move", Mathf.Abs(x));//El animator recibe el eje x absoluto y lo aplica a su propio float de movimiento
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);//se aplica la velocidad en el eje x multiplicandolo por
                                                                //la velocidad de movimiento que le hemos puesto nosotros
        if (x > 0)
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0, 180, 0);//Si se mueve hacia la derecha se gira
        }
        if (x < 0)
        {
            facingRight = false;
            transform.eulerAngles = Vector3.zero;//Si se mueve hacia la izquierda se queda igual
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && Time.timeScale == 1)//Si recibe la tecla, est� en el suelo y no est� pausado, salta
        {
            if (WaterTank.Capacity != 3)
            {
                animator.SetTrigger("Jump");
            }
            ClearForces();//funci�n para borrar cualquier fuerza ejercida en el personaje anteriormente (b�sicamente para prevenir que salte m�s bajo una vez cae al suelo)
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);//A�ade una fuerza al rigidbody de impulso en el eje 'y' de jumpSpeed
        }
    }
    private void IsGrounded()
    {
        //Lanza un rayo al suelo y detecta si colisiona con el suelo o no.
        RaycastHit2D hit = Physics2D.Raycast(groundLevel.transform.position, Vector2.down, 0.2f, groundLayer);
        Debug.DrawRay(groundLevel.transform.position, Vector2.down * 0.2f, Color.red);//para ver el rayo en el editor de unity
        isGrounded = false;//Lo inicializa como falso
        if (hit)
        {
            if (hit.collider.CompareTag("Floor"))
            {
                isGrounded = true;//Si colisiona, revisa la etiqueta del elemento; y si el elemento tiene esa etiqueta,
                                  //devuelve verdadero
            }
        }
    }
    private void ClearForces()
    {
        rb.velocity = Vector2.zero;//reinicia todas las fuerzas ejercidas en el personaje
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
