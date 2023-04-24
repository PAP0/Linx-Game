using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class FoamGun : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    [SerializeField] private GameObject BulletEmitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    [SerializeField] private GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    [SerializeField] private float BulletForwardForce;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Shoot()
    {
        //The Bullet instantiation happens here.
        GameObject TemporaryBulletHandler;
        TemporaryBulletHandler = Instantiate(Bullet, BulletEmitter.transform.position, BulletEmitter.transform.rotation) as GameObject;

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody TemporaryRigidBody;
        TemporaryRigidBody = TemporaryBulletHandler.GetComponent<Rigidbody>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
        TemporaryRigidBody.AddForce(transform.forward * BulletForwardForce);

        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        Destroy(TemporaryBulletHandler, 10.0f);
    }
}
