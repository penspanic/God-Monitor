using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
public class Goat : MonoBehaviour
{
    const float moveMinX = -5f;
    const float moveMaxX = 5f;


    Vector2 targetPos;
    Animator animator;
    SpriteRenderer renderer;
    AudioSource audioSource;
    bool isMoveRight = false;

    static AudioClip[] goatSounds;
    void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(GoatProcess());

        if (goatSounds == null)
            goatSounds = Resources.LoadAll<AudioClip>("Sounds/Goat/voice");
    }

    bool moveEnd = true;
    IEnumerator GoatProcess()
    {
        int soundWaitTime = 5;
        int elapsedTime = 0;
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (moveEnd)
                SetTargetPos();
            elapsedTime += 1;
            if (elapsedTime > soundWaitTime)
            {
                elapsedTime = 0;
                soundWaitTime = Random.Range(5, 10);
                audioSource.PlayOneShot(goatSounds[Random.Range(0, goatSounds.Length)]);

            }
            yield return new WaitForSeconds(1f);

        }
    }

    void SetTargetPos()
    {
        moveEnd = false;
        targetPos.x = Random.Range(moveMinX, moveMaxX);
        targetPos.y = transform.position.y;

        if (targetPos.x < transform.position.x)
            isMoveRight = false;
        else
            isMoveRight = true;
        renderer.flipX = isMoveRight;
        StartCoroutine(MoveProcess());
    }

    IEnumerator MoveProcess()
    {
        animator.Play("Goat_Walk");
        float elapsedTime = 0f;
        float moveTime = Random.Range(3, 5);
        Vector2 startPos = transform.position;
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / moveTime);
            yield return null;
        }

        animator.Play("Goat_Breathe");
        yield return new WaitForSeconds(3f);
        moveEnd = true;

    }
}