using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class BoomerangBullet : MonoBehaviour
    {
        public GameObject Owner { get; set; }
        public GameObject Parent { get; set; }
        bool go;

        [SerializeField]GameObject boomerangWeapon;
        [SerializeField] GameObject player;
        [SerializeField] GameObject boomerang;
        [SerializeField] Transform itemToRotate;

        Vector3 locationInFrontOfPlayer;

        void Start()
        {
            go = false;

            player = Owner.GetComponent<IWeapon>().Owner;
            boomerang = GameObject.Find("Boomeranger");

            boomerang.GetComponent<MeshRenderer>().enabled = false;
            itemToRotate = gameObject.transform.GetChild(0);

            locationInFrontOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 1, 0) + player.transform.forward * 10f;

            StartCoroutine(Boom());
        }

        IEnumerator Boom()
        {
            go = true;
            yield return new WaitForSeconds(0.6f);
            go = false;
        }

        void Update()
        {
            itemToRotate.transform.Rotate(0, 0, Time.deltaTime * 600);
            if (go)
            {
                transform.position = Vector3.MoveTowards(transform.position, locationInFrontOfPlayer, Time.deltaTime * 20);
            }
            if (!go)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1, 0), Time.deltaTime * 20);
            }
            if (!go && Vector3.Distance(player.transform.position, transform.position) < 1.5f)
            {
                boomerang.GetComponent<MeshRenderer>().enabled = true;
                Destroy(this.gameObject);
            }
        }
        Vector3 offset = new Vector3(0.5f,0,0);
        Vector3 negativeOffset = new Vector3(-0.5f, 0, 0);
        public void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Wall")
            {
                if(transform.position.x >= 0)
                {
                    Parent.transform.position = transform.position + negativeOffset;
                }
                else
                {
                    Parent.transform.position = transform.position + offset;
                }

                player.GetComponent<NewControllerInputs>().DropWeapon();
                boomerang.GetComponent<MeshRenderer>().enabled = true;
                Destroy(this.gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerHit = other.GetComponent<PlayerScript>();
            if (playerHit != null)
            {
                playerHit.TakeDamage(Parent.GetComponent<IWeapon>().Damage);
                playerHit.gameObject.GetComponent<ControllerMovement>().KnockBack(transform.position - rb.velocity, 10);
            }

            if (other.tag == "Player")
            {
                if (transform.position.x >= 0)
                {
                    Parent.transform.position = transform.position + negativeOffset;
                }
                else
                {
                    Parent.transform.position = transform.position + offset;
                }

                player.GetComponent<NewControllerInputs>().DropWeapon();
                boomerang.GetComponent<MeshRenderer>().enabled = true;
                Destroy(this.gameObject);
            }
        }
    }
}
