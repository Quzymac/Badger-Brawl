using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class BoomerangBullet : MonoBehaviour
    {
        public GameObject Owner { get; set; }
        bool go;
        [SerializeField] GameObject player;
        [SerializeField] GameObject boomerang;
        [SerializeField] Transform itemToRotate;

        Vector3 locationInFrontOfPlayer;
        // Start is called before the first frame update
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
        // Update is called once per frame
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

        }
    }
