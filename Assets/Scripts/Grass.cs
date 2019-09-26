using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private float destroyTime;

    //폭발력 세기
    [SerializeField]
    private float force; 

    //피격효과
    [SerializeField]
    private GameObject go_hit_effect_prefab;

    private Rigidbody[] rigidbodies;
    private BoxCollider[] boxColliders;


    [SerializeField]
    private string hit_sound;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = this.transform.GetComponentsInChildren<Rigidbody>();
        boxColliders = transform.GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Damage()
    {
        hp--;
        Hit();
        if(hp<=0)
        {
            Destruction();
        }
    }

    private void Hit()
    {
        SoundManager.instance.PlaySE(hit_sound);
        var clone = Instantiate(go_hit_effect_prefab, transform.position + Vector3.up, Quaternion.identity);
        Destroy(clone, destroyTime);
    }

    private void Destruction()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].useGravity = true;
            rigidbodies[i].AddExplosionForce(force, transform.position,1f);//폭발 세기,폭발 위치, 폭발 반경
            boxColliders[i].enabled = true;
        }
        Destroy(this.gameObject, destroyTime);
    }
}
