using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
public class NPCGuide : MonoBehaviour
{
    public static bool talkedTo = false;
    public static int level = 1;

    [SerializeField] private GameObject exclamationMark;

    private DialogueTrigger dialogue;

    private string[] monologue;
    [SerializeField] private Transform[] positions;

    [SerializeField] private AudioClip dialogueClip;
    private AudioSource _audioSource;

    private void Awake() {
        dialogue = GetComponent<DialogueTrigger>();
        dialogue.onFinishTalk.AddListener(OnFinishTalk);
        dialogue.onTalk.AddListener(UpdateLines);
    }

    private void Start() {
        if (LevelManager.currentLevel != level) {
            level = LevelManager.currentLevel;
            talkedTo = false;
        }

        exclamationMark.SetActive(!talkedTo);
        transform.position = positions[LevelManager.currentLevel - 1].position;
        UpdateLines();
        LevelManager.Instance.UpdateDoors();
    }

    private void UpdateLines() {
        GetLines();
        dialogue.SetDialogueOptions(monologue);
    }

    private void OnFinishTalk() {
        if (!talkedTo) {
            talkedTo = true;
            exclamationMark.SetActive(false);
            LevelManager.Instance.UpdateDoors();
        }
    }

    private void GetLines() {
        if (dialogueClip != null) {
            _audioSource.PlayOneShot(dialogueClip);
        }
        
        List<string> lines = new List<string>();
        Debug.Log(LevelManager.currentLevel);
        switch (LevelManager.currentLevel) {
            case 1:
                lines.Add("Hello " + GetNickname() + "!");
                lines.Add("I'm here to help you on your journey. Ready to begin?");
                lines.Add("I know you are looking for the one true nickname to help find yourself.");
                lines.Add("I've created 5 different portals for you to go and fight nicknames so you can concur the best one.");
                lines.Add("Use WASD to move, left click to attack, and right click to use your ability. You'll unlock more powers as you go.");
                lines.Add("Continue to the end of the level to get your first nickname!");
                lines.Add("Your first challenge will be to fight Big Money.");
                lines.Add("he's going to be in the door at the bottom of the room.");
                lines.Add("Good Luck!");
                break;

            case 2:
                lines.Add("Welcome back " + GetNickname() + "!");
                lines.Add("Congrats on beating Big Money!");
                lines.Add("That guy really liked to hit you far huh.");
                updateMonologue(lines);
                lines.Add("Hopefully Big Money is the name you're looking for.");
                lines.Add("It isn't? Wow you're really looking hard for this nickname.");
                lines.Add("You can go try and take the nickname of my old pal Rock.");
                lines.Add("He's a pretty tough guy to crack so be warned.");
                lines.Add("Head to the room at the bottom right.");
                lines.Add("Good Luck!");
                break;
            case 3:
                lines.Add("Welcome back " + GetNickname() + "!");
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
                break;
            case 4:
                lines.Add("Welcome back " + GetNickname() + "!");
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
                break;

            case 5:
                lines.Add("Welcome back " + GetNickname() + "!");
                lines.Add("Congrats on beating Salami Sam!");
                lines.Add("A bit sad to see his name go, but it is what it is.");
                updateMonologue(lines);
                lines.Add("Salami Sam has to be the one now.");
                lines.Add("No? You've got to be kidding me.");
                lines.Add("I guess you can have a shot at my old friend Ice Cube.");
                lines.Add("It would be a good day if you took his nickname. Better bring no vaseline!");
                lines.Add("Sorry for that, just go ahead to the door at the top right.");
                lines.Add("Good Luck!");
                break;

            case 6:
                lines.Add("Welcome back " + GetNickname() + "!");
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
                break;
            case 7:
                lines.Add("Well, you should know what your true nickname is now.");
                lines.Add("If you can defeat even yourself, you can find your true nickname.");
                lines.Add("Sometimes your name itself is the greatest nickname.");
                lines.Add("Roger Williams is the best nickname for you, Roger Williams.");
                lines.Add("You should not have been trying to imitate others, rather you should have been trying to make a name for yourself.");
                lines.Add("You are not Hot Rod or Ice Cube, you are the first and only Roger \"Roger Williams\" Williams");
                break;

            default:
                lines.Add("DIALOGUE NOT IMPLEMENTED YET");
                break;
        }
            // Array.Clear(monologue, 0, monologueLength);
            // lines.Add("Congrats on beating Guitar George!");
            // lines.Add("It always feels good seeing him get beat up.");
            // updateMonologue(lines);
            // lines.Add("While personally I don't like Guitar George, that name could work for you.");
            // lines.Add("Wow you are really making this difficult for me.");
            // lines.Add("Maybe you don't need a nickname. Why isn't Roger Williams good enough?");
            // lines.Add("Fine sorry I didn't mean to offend your entire \"quest\".");
            

            // lines.Add("I mean, maybe you need to reflect a little bit on yourself.");
            // lines.Add("What is the true reason you are looking for this nickname?");
            // lines.Add("Why are you trying to change yourself?");
            // lines.Add("Once you have found your answer, meet me in the room all the way at the front.");
            // lines.Add("If you come in unprepared, you will die.");
            // lines.Add("Good Luck!");
        
        if (lines.Count > 0)
        {
            monologue = lines.ToArray();
        }
    }

    private void updateMonologue(List<string> lines)
    {
        if(LoadoutManager.Instance == null)
        {
            lines.Add("Nothing");
        } 
        else {
            lines.Add("Here's a look at what you used to beat the boss.");
            if(LoadoutManager.Instance.currentWeapon != null)
            {
                lines.Add("Weapon used: " + LoadoutManager.Instance.currentWeapon.name);
            }
            if(LoadoutManager.Instance.currentAbility != null)
            {
                lines.Add("Ability used: " + LoadoutManager.Instance.currentAbility.name);
            }
            if(LoadoutManager.Instance.currentPassive != null)
            {
                lines.Add("Passive used: " + LoadoutManager.Instance.currentPassive.name);
            } 
        }
    }

    private string GetNickname() {
        return LoadoutManager.Instance.GetNickname();
    }
}
