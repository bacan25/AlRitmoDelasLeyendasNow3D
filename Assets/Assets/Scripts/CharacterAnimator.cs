using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator anim;
    private int random;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Press1()
    {
        anim.SetTrigger("press1");
    }
    
    void Press2()
    {
        anim.SetTrigger("press2");
    }

    void Press3()
    {
        anim.SetTrigger("press3");
    }

    void Press4()
    {
        anim.SetTrigger("press4");
    }

    public void AnimSelector()
    {
        random = Random.Range(0,4);
        switch(random){
            case 0:
                Press1();
                break;
            case 1:
                Press2();
                break;
            case 2:
                Press3();
                break;
            case 3:
                Press4();
                break;
            default:
                print("Error");
                break;  
        }
        
    }

    public void MissANote()
    {
        anim.SetTrigger("miss");
    }

    public void LoseGame()
    {
        anim.SetBool("lose", true);
        
    }

    public void Won()
    {
        anim.SetBool("won", true);
    }

    




   
    

}
