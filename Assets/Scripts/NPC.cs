using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    bool saved;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!saved && collision.CompareTag("Player"))
        {
            animator.SetTrigger("Saved");
            HUD.people++;
            saved = true;
        }
    }
}
