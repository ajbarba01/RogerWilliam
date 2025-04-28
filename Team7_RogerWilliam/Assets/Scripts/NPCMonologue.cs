
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NPCMonologue : MonoBehaviour {
       //private Animator anim;
       private NPCMonologueManager monologueMNGR;
       public string[] monologue; //enter monologue lines into the inspector for each NPC
       public bool playerInRange = false; //could be used to display an image: hit [e] to talk
       public int monologueLength;

       public AudioClip dialogueClip;
       private AudioSource _audioSource;
       // protected GameObject gameHandler;

       void Start(){
              //anim = gameObject.GetComponentInChildren<Animator>();
              monologueLength = monologue.Length;
              if (GameObject.FindWithTag("MonologueManager")!= null){
                     monologueMNGR = GameObject.FindWithTag("MonologueManager").GetComponent<NPCMonologueManager>();
              }
              // gameHandler = GameObject.FindWithTag("GameController");
       }

       private void OnTriggerEnter2D(Collider2D other){
              if (other.gameObject.tag == "Player") {
                     playerInRange = true;

                     if (dialogueClip != null) {
                            _audioSource.PlayOneShot(dialogueClip);
                     }

                     
                     List<string> lines = new List<string>(monologue);
                     if(GameHandler.tutorialBossDefeated == false)
                     {
                            Debug.Log("False");
                     }
                     if(GameHandler.tutorialBossDefeated)
                     {
                            Array.Clear(monologue, 0, monologueLength);
                            lines.Add("Congrats on beating Big Money!");
                            lines.Add("That guy really liked to hit you far huh.");
                            updateMonologue(lines);
                            lines.Add("Hopefully Big Money is the name you're looking for.");
                            lines.Add("It isn't? Wow you're really looking hard for this nickname.");
                            lines.Add("I guess you can have a shot at my friend Hot Rod. He's a high temper guy who really likes the tropical weather.");
                            lines.Add("Good Luck!");
                            
                     }
                     monologue = lines.ToArray();
                     monologueLength = monologue.Length;
                     monologueMNGR.LoadMonologueArray(monologue, monologueLength);
                     monologueMNGR.OpenMonologue();
                     //anim.SetBool("Chat", true);
                     //Debug.Log("Player in range");
              }
       }

       private void OnTriggerExit2D(Collider2D other){
              if (other.gameObject.tag =="Player") {
                     playerInRange = false;
                     monologueMNGR.CloseMonologue();
                     //anim.SetBool("Chat", false);
                     //Debug.Log("Player left range");
              }
       }
       private void updateMonologue(List<string> lines)
       {
              if(LoadoutManager.Instance == null)
              {
                     lines.Add("Nothing");
              } else
              {
                     lines.Add("Here's a look at what you used to beat the boss.");
                     if(LoadoutManager.Instance.currentWeapon.name != null)
                     {
                            lines.Add("Weapon used:" + LoadoutManager.Instance.currentWeapon.name);
                     }
                     if(LoadoutManager.Instance.currentAbility.name != null)
                     {
                            lines.Add("Ability used:" + LoadoutManager.Instance.currentAbility.name);
                     }
                     if(LoadoutManager.Instance.currentPassive.name != null)
                     {
                            lines.Add("Passive used:" + LoadoutManager.Instance.currentPassive.name);
                     } 
              }
       }
}