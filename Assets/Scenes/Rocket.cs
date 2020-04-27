using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] bool collisionDisabled = false;
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainFowardThrust = 50f;
    [SerializeField] float mainBackwardThrust = -50f;
    [SerializeField] float levelLoaddelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    //[SerializeField] ParticleSystem RocketFire;
    [SerializeField] ParticleSystem CollisionParticle;
    [SerializeField] ParticleSystem LevelUpPlayParticle;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LevelFinish();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                collisionDisabled = !collisionDisabled;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        print(collisionDisabled);
        if (state != State.Alive || collisionDisabled) { return; }
        
        switch (collision.gameObject.tag)
        {

            case "Safe": break;
            case "Start": break;
            case "Finish":
                //print("FInished");
                LevelFinish();
                break;
            default:
                print("Dead");
                Death_Sequence();
                break;
        }
    }

    private void Death_Sequence()
    {
        state = State.Dying;
        CollisionParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadNextScene", levelLoaddelay);

    }

    private void LevelFinish()
    {
        state = State.Transcending;
        LevelUpPlayParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScene", levelLoaddelay);
    }

    private void LoadNextScene()
    {

        if (state == State.Transcending)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            print(currentSceneIndex);
            currentSceneIndex = currentSceneIndex + 1;
            if (currentSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (state == State.Dying)
        {
            GetComponent<DeathSequence>().DeathHandel();
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainFowardThrust);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainBackwardThrust);
        }

        Sound();
    }

    private void Sound()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        
        rigidBody.freezeRotation = true;//take manual control of rotation ****REDUCE SPIN***
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward* rotationThisFrame);
        }
        rigidBody.freezeRotation = false;//resumes  physics control of rotation
    }
}
