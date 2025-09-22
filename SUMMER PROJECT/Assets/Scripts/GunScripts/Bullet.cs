using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float Damage;

    public float bulletLifeTime;

    public bool Testing;

    public bool HitOnce = false;

    public string[] TagsToDamage;

    public ParticleSystem poofEffect;

    public GameObject StoredObj;

    public AudioClip[] clips;

    public AudioClip poofNoise;

    AudioSource s;

    public bool InstantKillPlayer = false;

    [Header("DamageOverTime")]
    public float DamageOverTimer = 1;
    public float DOTTimer = 5;
    public bool InflictDamageOverTime = false;

    Rigidbody rb;
    public Vector3 V;
    public float shootF;
    public float UpwardF;

    [Header("InflictStatusAffectsOnPlayer")]
    public bool SlowDown;
    public bool SpeedUp;

    bool AppliedForce = true;

    [Range(0.1f, 0.9f)]
    public float SlowSpeed;

    [Range(1.1f, 3f)]
    public float SpedupSpeed;

    private void Awake()
    {


        if (clips.Length > 0)
        {
            int i = Random.RandomRange(0, clips.Length);
            if (clips[i] != null)
            {
                s = GetComponent<AudioSource>();
                s.clip = clips[i];
                s.Play();
            }
        }
          
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            V = rb.GetAccumulatedForce();
        }

    }

    private void Update()
    {
        if (InstantKillPlayer == true)
        {
            Collider collider = gameObject.GetComponent<Collider>();
            if (collider.enabled == true)
            {

                Health PH = GameObject.FindWithTag("Player").GetComponent<Health>();
                PH.CurrentHealth = 0;
            }
        }

        if (rb != null)
        {
            if (PauseManager.Instance != null)
            {
                if (PauseManager.Instance.IsPaused)
                {
                    AppliedForce = false;
                    rb.isKinematic = true;
                    //rb.constraints = RigidbodyConstraints.FreezeAll;
                }
                else
                {
                    if (!Testing)
                    {
                        bulletLifeTime -= Time.deltaTime;
                        if (bulletLifeTime <= 0)
                        {
                            Destroy(gameObject);
                        }
                    }

                    rb.isKinematic = false;
                    if (AppliedForce == false)
                    {

                        rb.AddForce(V.normalized * shootF, ForceMode.Impulse);
                        AppliedForce = true;
                    }

                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

                if (poofNoise != null)
                {
                    s.clip = poofNoise;
                    s.Play();
                }

                for (int i = 0; i < TagsToDamage.Length; i++)
                {
                    if (collision.gameObject.CompareTag(TagsToDamage[i]))
                    {
                        // Health should be on the hit object or one of its parents
                        Health tempHP = collision.gameObject.GetComponent<Health>();
                        HealthBarMultiple tempHPM = null;
                        if (tempHP == null)
                        {
                            tempHP = collision.gameObject.GetComponentInParent<Health>();
                        }
                        
                        if(TagsToDamage[i] == "Player")
                        {
                            Movement M = GameObject.FindWithTag(TagsToDamage[i]).GetComponent<Movement>();
                            if(SlowDown == true)
                            {
                                M.SlowSpeed = SlowSpeed;
                                M.slowed = true;
                            }

                            if (SpeedUp == true)
                            {
                                M.SpedUpSpeed = SpedupSpeed;
                                M.onFire = true;

                            }


                }

                        if (tempHP == null)
                        {

                            tempHPM = collision.gameObject.GetComponent<HealthBarMultiple>();
                            if (tempHPM == null)
                            {
                                tempHPM = collision.gameObject.GetComponentInParent<HealthBarMultiple>();


                                Debug.Log("Hit");
                            }

                        }

                        // Check if the actual collider that was hit has the WeakSpot
                        WeakSpot WS = collision.collider.GetComponent<WeakSpot>();

                        if (tempHP != null)
                        {
                            if (WS != null)
                            {
                                tempHP.CRITDamage(Damage);
                            }
                            else
                            {
                                tempHP.Damage(Damage);
                                if(InflictDamageOverTime == true)
                                    {
                                        tempHP.DamageInflictedOverTime = DamageOverTimer;
                                        tempHP.DOTTimer = DOTTimer;
                                        tempHP.startDottTimer = true;

                                    }

                            }
                        }
                        if (tempHPM != null)
                        {
                            if (WS != null)
                            {
                                tempHPM.CRITDamage(Damage);
                            }
                            else
                            {
                                tempHPM.Damage(Damage);
                            }
                        }

                        break;
                    }
                }

                if (!Testing)
                {
                    poofEffect.transform.parent = null;
                    poofEffect.Play();



                    Destroy(gameObject);
                }
                else if (HitOnce)
                {
                    Collider c = gameObject.GetComponent<Collider>();
                    if (c != null)
                    {
                        c.enabled = false;
                    }
                }

    }
    
    private void OnParticleCollision(GameObject other)
    {
        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
            {

                for (int i = 0; i < TagsToDamage.Length; i++)
                {
                    if (other.CompareTag(TagsToDamage[i]))
                    {
                        // Health should be on the hit object or one of its parents
                        Health tempHP = other.GetComponent<Health>();
                        //  Debug.Log(tempHP);
                        HealthBarMultiple tempHPM = null;
                        if (tempHP == null)
                        {
                            tempHP = other.GetComponentInParent<Health>();
                        }

                        if (tempHP == null)
                        {

                            tempHPM = other.GetComponent<HealthBarMultiple>();
                            if (tempHPM == null)
                            {
                                tempHPM = other.GetComponentInParent<HealthBarMultiple>();
                            }

                        }

                        if (TagsToDamage[i] == "Player")
                        {
                            Movement M = GameObject.FindWithTag(TagsToDamage[i]).GetComponent<Movement>();
                            if (SlowDown == true)
                            {
                                M.SlowSpeed = SlowSpeed;
                                M.slowed = true;
                            }

                            if(SpeedUp == true)
                            {
                                M.SpedUpSpeed = SpedupSpeed;
                                M.onFire = true;

                            }


                        }

                        // Check if the actual collider that was hit has the WeakSpot


                        if (tempHP != null)
                        {

                            tempHP.Damage(Damage);

                            if (InflictDamageOverTime == true)
                            {
                                tempHP.DamageInflictedOverTime = DamageOverTimer;
                                tempHP.DOTTimer = DOTTimer;
                                tempHP.startDottTimer = true;

                            }


                        }
                        if (tempHPM != null)
                        {

                            tempHPM.Damage(Damage);


                        }

                        break;
                    }

                }


                if (!Testing)
                {
                    poofEffect.transform.parent = null;
                    poofEffect.Play();

                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnParticleTrigger()
    {

    }



    }



