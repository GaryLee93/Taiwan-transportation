using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShooter : MonoBehaviour
{
    [SerializeField] GameObject shooterBullet;
    
    public void shoot_circle_bullet(Vector2 direction, int count, bool aimingPlayer){
        Vector2 tmp_direction;
        if(aimingPlayer)
            tmp_direction = ourTool.vectorToPlayer(gameObject).normalized * direction.magnitude;
        else
            tmp_direction = direction;
        
        for(int i=0; i<count; i++){
            GameObject newBullet = objectPooler.spawnFromPool(
                shooterBullet.name,transform.position,transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = tmp_direction;
            tmp_direction = ourTool.rotate_vector(tmp_direction, 360f/count);
        }
    }
    public void shoot_sector_bullet(Vector2 direction, int count, float angle, bool aimingPlayer){
        if(count > 1){
            Vector2 tmp_direction;
            if(aimingPlayer)
                tmp_direction = ourTool.vectorToPlayer(gameObject).normalized * direction.magnitude;
            else
                tmp_direction = direction;
            
            tmp_direction = ourTool.rotate_vector(tmp_direction, -angle);
            for(int i=0; i<count; i++){
                GameObject newBullet = objectPooler.spawnFromPool(
                    shooterBullet.name,transform.position,transform.rotation);
                newBullet.GetComponent<Rigidbody2D>().velocity = tmp_direction;
                tmp_direction = ourTool.rotate_vector(tmp_direction, 2*angle/(count-1));
            }
        }
        else{
            Vector2 tmp_direction;
            if(aimingPlayer)
                tmp_direction = ourTool.vectorToPlayer(gameObject).normalized * direction.magnitude;
            else
                tmp_direction = direction;

            GameObject newBullet = objectPooler.spawnFromPool(shooterBullet.name,
                transform.position,transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = tmp_direction;
        }
    }
}
