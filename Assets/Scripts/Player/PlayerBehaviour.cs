using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    #region Variable
        #region private
            [SerializeField] private BoxCollider2D boxCollider2D;
            [SerializeField] private Vector3 moveDelta;
            [SerializeField] private RaycastHit2D hit;
            [SerializeField] private GameObject playerSprite;
            [SerializeField] private GameObject treeSprite;
            [SerializeField] private Animator playerAnim;
            private bool isMoving = false;
            private bool isRunning = false;
            [SerializeField] private bool isDead = false;
        #endregion
        #region public
            public Joystick joystick;
        #endregion
        #region const
            private const float RUN_SPEED = 0.84f;
            private const string ENEMY_TAG = "Enemy";
        #endregion
    #endregion
    
    private void OnValidate()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerAnim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(isDead) return;
        MoveMent();
    }

    private void MoveMent()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        //reset MoveDelta
        moveDelta = new Vector3(x, y, 0);

        isMoving = (moveDelta == Vector3.zero)? false : true;

        // Biến thành cây
        playerSprite.SetActive(isMoving);
        treeSprite.SetActive(!isMoving);

        gameObject.GetComponent<Collider2D>().enabled = isMoving; 

        if(!isMoving) return;

        transform.localScale = (moveDelta.x > 0) ? Vector3.one : new Vector3(-1, 1, 1);

        isRunning = (Mathf.Sqrt(moveDelta.x * moveDelta.x + moveDelta.y * moveDelta.y) >= RUN_SPEED)? true : false;
        
        // Tạo animation run
        playerAnim.SetBool("isRunning", isRunning);

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider != null) return;

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider != null) return;
 
        transform.Translate(moveDelta*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag(ENEMY_TAG)) return;

        isDead = true;
    }
}