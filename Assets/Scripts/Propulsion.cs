using UnityEngine;

public class Propulsion : MonoBehaviour
{
    private Animator myAnimator;

    private const string BOOST_ANIMATION_FLAG = "Boosting";

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BoostAnimate();
    }

    void BoostAnimate()
    {
        if (!GameManager.instance.GamePaused())
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                myAnimator.SetBool(BOOST_ANIMATION_FLAG, true);
                if (!FindObjectOfType<AudioManager>().IsPlaying(AudioManager.BOOST_SFX))
                    FindObjectOfType<AudioManager>().Play(AudioManager.BOOST_SFX);
            }
            else
            {
                myAnimator.SetBool(BOOST_ANIMATION_FLAG, false);
                if (FindObjectOfType<AudioManager>().IsPlaying(AudioManager.BOOST_SFX))
                    FindObjectOfType<AudioManager>().Stop(AudioManager.BOOST_SFX);
            }
        }
        else
        {
            if (FindObjectOfType<AudioManager>().IsPlaying(AudioManager.BOOST_SFX))
                FindObjectOfType<AudioManager>().Stop(AudioManager.BOOST_SFX);
        }
        
    }
}
