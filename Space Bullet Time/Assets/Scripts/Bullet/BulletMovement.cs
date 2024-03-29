﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	private CharacterController _charController;
	private Vector3 bulletDirection = Vector3.zero;
	private float bulletTime = 1f;
	private float speed = 80f;
	
	//Trail
	[SerializeField]
	private GameObject bulletTrail_prefab;
	private float trailTimer = 0.3f;
	public float trailCoolDown = 0.003f;
	
	
	// Time Manager
	private TimeManager _timemanager;
	
	void Start(){
		
		_charController = gameObject.AddComponent<CharacterController>();
		_timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
	}
	
	public void SetBulletTime(float _bulletTime){
		bulletTime = _bulletTime;
	}
	//set bullet direction with a Vector3
    public void SetBulletDirection(Vector3 direction){
		bulletDirection = direction;
	}
	private void UptadeBulletTime(){
		bulletTime = _timemanager.GetBulletTime();
	}
	private void ProduceTrail(){
		if(trailTimer > trailCoolDown){
			//smoothly randomize the position of the trail
			Vector3 newPos = new Vector3(transform.position.x,transform.position.y + Random.Range(-0.8f, 0.8f),transform.position.z);
			trailTimer = 0;
			GameObject o = Instantiate(bulletTrail_prefab,newPos,transform.rotation);
		}
		trailTimer += Time.deltaTime*bulletTime;
	}
	
    // Update is called once per frame
    void Update()
    {
		UptadeBulletTime();
		ProduceTrail();
		//movement of Bullet
		if(bulletDirection != Vector3.zero)_charController.Move(bulletDirection * Time.deltaTime * speed * bulletTime); // Apply movement only if its not zero
        
    }
}
