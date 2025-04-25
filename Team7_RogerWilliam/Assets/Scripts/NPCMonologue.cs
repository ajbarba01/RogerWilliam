
using System.Collections.Generic;
using System.Collections;
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
                            if(LoadoutManager.Instance == null)
                            {
                                   lines.Add("Nothing");
                            } else
                            {
                                   lines.Add("Congrats on using these to defeat the boss!");
                                   if(LoadoutManager.Instance.unlockedWeapons.Count > 0)
                                   {
                                          foreach (var weapon in LoadoutManager.Instance.unlockedWeapons)
                                          {
                                                 lines.Add(" " + weapon.name);
                                          }
                                   }
                                   if (LoadoutManager.Instance.unlockedAbilities.Count > 0)
                                   {
                                          foreach (var ability in LoadoutManager.Instance.unlockedAbilities)
                                          {
                                                 lines.Add("- " + ability.name);
                                          }
                                   }

                                   if (LoadoutManager.Instance.unlockedPassives.Count > 0)
                                   {
                                          foreach (var passive in LoadoutManager.Instance.unlockedPassives)
                                          {
                                                 lines.Add("- " + passive.name);
                                          }
                                   }      
                            }
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
}