using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    public Text coinText;
    public Text scoreText;
    private int coinCount;
    private CharacterController characterController;
    private readonly float speed = 5.0f;
    private readonly float gravity = 12.0f;
    private float verticalVelocity = 0.0f;
    Vector3 moveVector;
    private float animationDuration = 2.0f;
    private float speedMultiplier = 1;
    private int score = 0;
    private int scoreToNextLevel = 10;
    private float touchStartX = 0f;
    private float touchStartY = 0f;
    private bool dead = false;
    private bool isJumping = false;

    void Start()
    {
        scoreText.text = "Distance: 0";
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (dead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            touchStartX = Input.mousePosition.x;
            touchStartY = Input.mousePosition.y;
        }
        float dx = 0f;
        if (Input.GetMouseButtonUp(0))
        {
            bool isMovingX = false;
            float deltaX = Input.mousePosition.x - touchStartX;
            if (deltaX < -50f)
            {
                dx = -10;
                isMovingX = true;
            }
            else if (deltaX > 50f)
            {
                dx = 10;
                isMovingX = true;
            }
            if (!isMovingX)
            {
                float deltaY = Input.mousePosition.y - touchStartY;
                if (deltaY > -30f)
                {
                    Jump();
                }
            }
        }

        if (Time.time < animationDuration)
        {
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        if (isJumping)
        {
            verticalVelocity = 6f; //dy * Time.deltaTime;
        }
        else if (characterController.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * 4 * Time.deltaTime;
        }

        moveVector.y = verticalVelocity;
        moveVector.x = dx * speed;
        moveVector.z = speed * speedMultiplier;

        score = (int)transform.position.z;
        scoreText.text = "Distance: " + score.ToString();

        characterController.Move(moveVector * Time.deltaTime);

        if (score - scoreToNextLevel > 0)
        {
            LevelUp();
        }

        if (characterController.gameObject.transform.position.y < -30)
        {
            SceneManager.LoadScene("MainMenuScene");
            dead = true;
        }
    }

    void LevelUp()
    {
        speedMultiplier *= 1.5f;
        scoreToNextLevel = scoreToNextLevel * 2 + score;
    }

    public void Jump()
    {
        if (isJumping)
        {
            return;
        }

        isJumping = true;
        GameObject go = GetPlayerAvatar();
        Animator animator = go.GetComponent<Animator>();
        animator.Play("FreeVoxelGirl-jump");
        StartCoroutine(RunAgain());
    }

    private IEnumerator RunAgain()
    {
        GameObject go = GetPlayerAvatar();
        yield return new WaitForSeconds(0.7f);
        isJumping = false;
        if (!dead)
        {
            Animator animator = go.GetComponent<Animator>();
            animator.Play("FreeVoxelGirl-run");
        }
    }

    public void Die()
    {
        dead = true;
        StartCoroutine(DoDeath());
    }

    public void Earn(int amount)
    {
        coinCount += amount;
        coinText.text = "Coins: " + coinCount;
    }

    private IEnumerator DoDeath()
    {
        GameObject go = GetPlayerAvatar();
        Animator animator = go.GetComponent<Animator>();
        animator.Play("FreeVoxelGirl-death");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenuScene");
        yield return null;
    }

    private GameObject GetPlayerAvatar()
    {
        Transform t = gameObject.transform;
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == "PlayerAvatar")
            {
                return t.GetChild(i).gameObject;
            }
        }
        return null;
    }
}