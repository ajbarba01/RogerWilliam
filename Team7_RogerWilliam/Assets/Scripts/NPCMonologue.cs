
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
                     if(GameHandler.tutorialBossDefeated && !GameHandler.rockBossDefeated)
                     {
                            Array.Clear(monologue, 0, monologueLength);
                            lines.Add("Congrats on beating Big Money!");
                            lines.Add("That guy really liked to hit you far huh.");
                            updateMonologue(lines);
                            lines.Add("Hopefully Big Money is the name you're looking for.");
                            lines.Add("It isn't? Wow you're really looking hard for this nickname.");
                            lines.Add("You can go try and take the nickname of my old pal Rock.");
                            lines.Add("He's a pretty tough guy to crack so be warned.");
                            lines.Add("Head to the room at the bottom right.");
                            lines.Add("Good Luck!");
                            
                     }
                     if(GameHandler.rockBossDefeated && !GameHandler.hotRodDefeated)
                     {
                            Array.Clear(monologue, 0, monologueLength);
                            lines.Add("Congrats on beating the Rock!");
                            lines.Add("Bet you're pretty jealous I was friends with him.");
                            lines.Add("Hopefully he wasn't too hard on you.");
                            updateMonologue(lines);
                            lines.Add("The rock sounds like a good nickname for you.");
                            lines.Add("Not this one either? You're a picky one.");
                            lines.Add("I didn't know a nickname was this important to anyone.");
                            lines.Add("I guess you can have a shot at my friend Hot Rod. He's a high temper fiery guy!");
                            lines.Add("Head to the room at the bottom left.");
                            lines.Add("Good luck with him!");
                     }
                     if(GameHandler.hotRodDefeated && !GameHandler.salamiSamDefeated)
                     {
                            Array.Clear(monologue, 0, monologueLength);
                            lines.Add("Congrats on beating Rod \"Hot Rod\" Johnson!");
                            lines.Add("Hope the heat didn't get to you in there.");
                            updateMonologue(lines);
                            lines.Add("Hopefully Hot Rod is the name you're looking for.");
                            lines.Add("You're really looking hard for this nickname huh.");
                            lines.Add("Maybe you should look a little bit deeper into your soul.");
                            lines.Add("Me personally i think hot rod fits just fine.");
                            lines.Add("My recommendation is a butcher I used to buy from, Salami Sam.");
                            lines.Add("He's a bit of a nutcase but he's got a good nickname.");
                            lines.Add("If this isn't the one then I don't know what to do with you.");
                            lines.Add("Head to the room at the top left.");
                            lines.Add("Good Luck!");
                            
                     }
                     if(GameHandler.salamiSamDefeated && !GameHandler.iceCubeDefeated)
                     {
                            Array.Clear(monologue, 0, monologueLength);
                            lines.Add("Congrats on beating Salami Sam!");
                            lines.Add("A bit sad to see his name go, but it is what it is.");
                            updateMonologue(lines);
                            lines.Add("Salami Sam has to be the one now.");
                            lines.Add("No? You've got to be kidding me.");
                            lines.Add("I guess you can have a shot at my old friend Ice Cube.");
                            lines.Add("It would be a good day if you took his nickname. Better bring no vaseline!");
                            lines.Add("Sorry for that, just go ahead to the door at the top right.");
                            lines.Add("Good Luck!");
                     }
                     if(GameHandler.iceCubeDefeated && !GameHandler.finalBossDefeated)
                     {
                            Array.Clear(monologue, 0, monologueLength);
                            lines.Add("Congrats on beating Ice Cube!");
                            lines.Add("Bit of a change going from heat to cold, hope you didn't get too chilly.");
                            updateMonologue(lines);
                            lines.Add("Ice Cube has got to be the nickname for you!");
                            lines.Add("Wow, if hot rod and ice cube both don't work for you, I'm not sure what will.");
                            lines.Add("I'm starting to think you're just taking these nicknames just for fun.");
                            lines.Add("Nothing wrong with that, you just don't have to beat up my friends.");
                            lines.Add("I mean, maybe you need to reflect a little bit on yourself.");
                            lines.Add("What is the true reason you are looking for this nickname?");
                            lines.Add("Why are you trying to change yourself?");
                            lines.Add("Once you have found your answer, meet me in the room all the way at the front.");
                            lines.Add("If you come in unprepared, you will die.");
                            lines.Add("Good Luck!");
                     }
                     if(GameHandler.finalBossDefeated)
                     {
                            Array.Clear(monologue, 0, monologueLength);
                            lines.Add("Well, you should know what your true nickname is now.");
                            lines.Add("If you can defeat even yourself, you can find your true nickname.");
                            lines.Add("Sometimes your name itself is the greatest nickname.");
                            lines.Add("Roger Williams is the best nickname for you, Roger Williams.");
                            lines.Add("You should not have been trying to imitate others, rather you should have been trying to make a name for yourself.");
                            lines.Add("You are not Hot Rod or Ice Cube, you are the first and only Roger \"Roger Williams\" Williams");
                     }
                     
                     if(lines.Count > 0)
                     {
                            monologue = lines.ToArray();
                            monologueLength = monologue.Length;
                            monologueMNGR.LoadMonologueArray(monologue, monologueLength);
                            monologueMNGR.OpenMonologue();
                     }
                     
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
                     if(LoadoutManager.Instance.currentWeapon != null)
                     {
                            lines.Add("Weapon used:" + LoadoutManager.Instance.currentWeapon.name);
                     }
                     if(LoadoutManager.Instance.currentAbility != null)
                     {
                            lines.Add("Ability used:" + LoadoutManager.Instance.currentAbility.name);
                     }
                     if(LoadoutManager.Instance.currentPassive != null)
                     {
                            lines.Add("Passive used:" + LoadoutManager.Instance.currentPassive.name);
                     } 
              }
       }
}