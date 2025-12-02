using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;

public class Player : Character, IShootable
{
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform BulletSpawnPoint { get; set; }
    [field: SerializeField] public float BulletSpawnTime { get; set; }
    [field: SerializeField] public float BulletTimer { get; set; }
    [field: SerializeField] public float JumpCount { get; set; }
    [field: SerializeField] public int BulletCount { get; set; }
    [SerializeField] TextMeshProUGUI bulletText, jumpText;
    public float MaxSpeed = 10f;
	bool facingRight = true;
	public float JumpForce = 700.0f;




	void Start ()
	{
        Init(1);
        BulletSpawnTime = 0.5f;
        BulletTimer = 2.0f;
        BulletCount = 3;
        JumpCount = 3;
        UpdateBulletText();
        UpdateJumpText();
    }
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && JumpCount > 0)
		{
			rb.AddForce(new Vector2(0, JumpForce));
			JumpCount -= 1;
            UpdateJumpText();
        }

        BulletTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && BulletTimer <= 0 && BulletCount > 0)
        {
            Shoot();
        }
    }

	void FixedUpdate () 
	{

		float move = Input.GetAxis("Horizontal");

		rb.velocity = new Vector2(move*MaxSpeed, rb.velocity.y);

		if(move > 0 && !facingRight)
			Flip();
		else if( move < 0 && facingRight )
			Flip();
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    public void Shoot()
    {
        GameObject obj = Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        BulletPng bulletPng = obj.GetComponent<BulletPng>();
        bulletPng.Init(1, this);
        BulletTimer = BulletSpawnTime;
        BulletCount -= 1;
        UpdateBulletText();
    }
    public void OnHitWith(Character character)
    {
        if (character is Enemy)
        {
            Debug.Log("You Dead!");
            Destroy(this.gameObject);
        }
    }
    public void OnHitWith(Finish type)
    {
        if (type is Finish)
        {
            Debug.Log("You Win!");
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        OnHitWith(other.gameObject.GetComponent<Character>());
        OnHitWith(other.gameObject.GetComponent<Finish>());
    }
    //SkillUp
    public void SkillUp(int bulletPlus)
    {
        BulletCount += bulletPlus;
        UpdateBulletText();
    }

    public void SkillUp(float jumpPlus)
    {
        JumpCount += jumpPlus;
        UpdateJumpText();
    }

    void UpdateBulletText()
    {
        bulletText.text = $"x{BulletCount}";
    }
    void UpdateJumpText()
    {
        jumpText.text = $"x{JumpCount}";
    }
}
