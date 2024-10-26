using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrip : MonoBehaviour
{
    /*
    public float player_stamina = 100f;
    [SerializeField] private float max_stamina =100f;

    public float player_health = 100f;
    [SerializeField] private float max_health =100f;

    public Clippy cli;
    GameObject Player;

    [SerializeField] private Image staminaprogressUI;
    [SerializeField] private Image healthprogressUI;
    [SerializeField] private CanvasGroup slidercanvas = null; 


    private float health_regen = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        cli = Player.GetComponent<Clippy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        update_health();
        
        if(cli.is_sprinting)
        {
            player_stamina=player_stamina-0.5f;
            update_stamina(1);
        }
        
        if(cli.rollcounter==true)
        {
            player_stamina=player_stamina-20f;
            update_stamina(1);
            cli.rollcounter=false;
        }


        if(player_stamina<max_stamina && !cli.is_sprinting )
        {
            player_stamina=player_stamina+0.2f;
            update_stamina(1);
            
        }

        if(player_health<max_health)
        {
            player_health=player_health+health_regen;
            update_health();
            
        }

        if(player_stamina>=max_stamina && player_health>=max_health)
        {
            update_stamina(0);
        }
    }

    void update_stamina(int val)
    {
        staminaprogressUI.fillAmount = player_stamina / max_stamina;

        
    }

    void update_health()
    {


        player_health-=cli.dmgtaken;
        cli.dmgtaken=0;
        healthprogressUI.fillAmount = (player_health / max_health);
        
        
    }

    */


}
