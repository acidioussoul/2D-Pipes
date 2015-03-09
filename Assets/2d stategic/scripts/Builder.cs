using UnityEngine;
using System.Collections;
using System;

public class Builder : MonoBehaviour
{
  //Define all variables needed in class.
    //movement variables.
    public float speed;
    private Vector3 target;
    public float turnSpeed = 1.0F;
    public Vector3 position;
    public Quaternion rot;
    public float turn;     //variable also used for animation parameter check. ??rethink this solution??
    //Sound variables.
    public AudioSource[] sounds;
    public AudioSource click;
    public AudioSource clack;
    public AudioSource wurr;
    public AudioSource idle;
    public AudioSource start;
    public AudioSource acc;
    public AudioSource dcc;
    //Animation variables.
    Animator anim;
    
    void Start()
    {
        //Get audio sources and place into an array.
        sounds = GetComponents<AudioSource>();
        click = sounds[0];
        clack = sounds[1];
        wurr = sounds[2];
        idle = sounds[3];
        start = sounds[4];
        acc = sounds[5];
        dcc = sounds[6];
        //??I'm not sure what this is doing?? Maybe places vector3 value into a value that can be used in a float.
        target = transform.position;
        //Get animator controller and place into anim.
        anim = GetComponent<Animator>();
        start.Play();
        //start.volume = 0.2F;
       // wurr.volume = 0.2F;
       // idle.volume = 0.2F;

    }

    void FixedUpdate()
        {            
        //Get mouse left click position and store variable in target.
            if (Input.GetMouseButtonDown(1))
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              //Insure that z position is maintained.
                target.z = transform.position.z;
              //Gte mouse left click position and store variable in mousePosition.
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              //Move mousePosition variable into postion for use outside of if statement.
                position = mousePosition;
              //Define the amount of rotation needed to face target destination and store variable in rot.
                rot = Quaternion.LookRotation(transform.position - position, Vector3.forward);
                //play click sound at begining and wurr sound during vehicle movement.
                //click.volume = 1.0F;
                click.Play();
                if (!wurr.isPlaying)
                {
                    acc.Play();
                    wurr.PlayDelayed(1);
                }
            }
          //If statement to control how long rotation is needed until facing target destination.
            if (transform.position != target)
            {
              //Create a float value that can be used inside of Quaternion.Slerp as well as anim.SetFloat.
                turn = Time.deltaTime * turnSpeed;
            }
          //Else statement to set turn to 0 at destination and stop wurr sound.
            else 
            {
                turn = 0;

                if (!dcc.isPlaying && wurr.isPlaying)
                {
                    wurr.Stop();
                    dcc.Play();
                }

            }
        //If statement checking that wurr is not playing as well as idle is not playing (! is reverse true/false)
            if (!wurr.isPlaying && !idle.isPlaying && !start.isPlaying && !acc.isPlaying && !dcc.isPlaying)
            {
                idle.Play();
            }
        //checking to see if wurr is running to keep wurr and idle from running at the same time. Needs to be else if so it doesn't constantly toggle idle on/off with the previous if statement ie: it stops running commands with the audio is running.
            else if (wurr.isPlaying)
            {
                idle.Stop();
            }

              //Stops entity from tilting when mouse is clicked on a destination.
                rot[1] = 0F;
                rot[0] = 0F;
          //Rotates entity by using rot and turn variables.     
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turn);
          //Moves entity from starting point to destination by using target and speed variables.
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
          /*??Since Quanternion.LookRotaion produces strange results this allows the use of the variable turn as 
          a movement variable until it is reset to 0 at destination. The variable turn is used to set the speed
          parameter for anim.SetFloat which controls the motion animation on the entity.??*/
            anim.SetFloat("speed", Mathf.Abs(turn));
        }
}