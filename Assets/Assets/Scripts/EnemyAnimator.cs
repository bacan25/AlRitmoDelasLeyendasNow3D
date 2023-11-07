using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
   public Animator anim; 

  private void Start()
  {
    anim.GetComponent<Animator>();
  }

  public void Hit()
  {
    anim.SetTrigger("hit");

  }

  public void Dead()
  {
    anim.SetBool("dead", true);

  }
   
   
}
