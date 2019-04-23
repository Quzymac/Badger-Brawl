using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

    public class GameManager : MonoBehaviour
    {
        List<PlayerScript> badgers = new List<PlayerScript>(); //hålla koll på badgers
        public List<PlayerScript> Badgers { get { return badgers; } set { badgers = value; } } //hålla koll på badgers

        List<PlayerScript> humans = new List<PlayerScript>(); //hålla koll på humans
        public List<PlayerScript> Humans { get { return humans; } set { humans = value; } } //hålla koll på humans

        [SerializeField] MultipleTargetCam multipleTargetCam;
        //CAM_CamerMovement cameraMovement;
        float cameraValue = 25f;

        int winningPlayer;

        public SoundManager soundManager;

        public Transform top;
        public Transform bottom;
        [SerializeField] Transform target;

        float yTop;
        float yBottom;
        float totalDistance;
        public float pctDamage;
        public float pctValue;
        [SerializeField] int camRoundPosition = 0;
        public int _camRoundPosition { get { return camRoundPosition; }}
        [SerializeField] ProgressBar progs;
        WinnerScript winnerScript;

        bool lastRoundBadger = false;
        bool lastRoundHuman = false;


        private void Update()
        {
            if(multipleTargetCam.targets.Count == 0)
            {
                multipleTargetCam.UpdateCamPos();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {

                foreach (var badger in badgers)
                {
                    if(badger != null)
                    {
                        Destroy(badger.gameObject);
                    }
                }
                foreach (var human in humans)
                {
                    if(human != null)
                    {
                        Destroy(human.gameObject);
                    }
                }
                gameObject.GetComponent<SpawnPlayers>().Spawn();
            }
        }

        void Start()
        {           
            //cameraMovement = FindObjectOfType<CAM_CamerMovement>();
            winnerScript = FindObjectOfType<WinnerScript>().GetComponent<WinnerScript>();
            yTop = top.position.y;
            yBottom = bottom.position.y;
            totalDistance = yTop - yBottom;
        }


        public bool TeamIsDead(PlayerScript.PlayerTeam team)
        {
            if (team == PlayerScript.PlayerTeam.human)
            {
                for (int i = 0; i < humans.Count; i++)
                {
                    if (humans[i].dead == false)
                    {
                        return false;
                    }
                }
                NewRound(PlayerScript.PlayerTeam.badger);
                return true;
            }
            else
            {
                for (int i = 0; i < badgers.Count; i++)
                {
                    if (badgers[i].dead == false)
                    {
                        return false;
                    }
                }
                NewRound(PlayerScript.PlayerTeam.human);
                return true;
            }
        }


        public void NewRound(PlayerScript.PlayerTeam winner)
        {
            
            
            if (winner == PlayerScript.PlayerTeam.badger)
            {
                camRoundPosition--;
                ChangeCameraPos(-cameraValue);
            }
            else if (winner == PlayerScript.PlayerTeam.human)
            {
                camRoundPosition++;
                ChangeCameraPos(cameraValue);
            }

            foreach (var badger in badgers)
            {
                if (badger != null)
                {
                    multipleTargetCam.targets.Clear();
                    Destroy(badger.gameObject);
                }
            }
            foreach (var human in humans)
            {
                if (human != null)
                {
                    multipleTargetCam.targets.Clear();
                    Destroy(human.gameObject);
                }
            }
            StartCoroutine(Wait());

            //gameObject.GetComponent<WeaponSpawning>().ClearWeapons();
            //gameObject.GetComponent<SpawnPlayers>().Spawn();
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2f);

            gameObject.GetComponent<WeaponSpawning>().ClearWeapons();
            gameObject.GetComponent<SpawnPlayers>().Spawn();
        }
        private void CalculatePosition() //Calcutes the position to which the camera should move to. Win condition needs to be here
        {
            if (pctDamage >= 100)
            {
                pctDamage = 100;
                if (lastRoundHuman)
                {
                    winnerScript.humansWon();
                    lastRoundHuman = false;
                }
                lastRoundHuman = true;
            }
            if (pctDamage <= 0)
            {
                pctDamage = 0;
                if (lastRoundBadger)
                {
                    winnerScript.badgersWon();
                    lastRoundBadger = false;
                }
                lastRoundBadger = true;
            }
            pctValue = totalDistance * (pctDamage / 100);
            Debug.Log(pctValue);
            target.position = new Vector3(0, yBottom + pctValue, -20);
            progs.dpctDamage = pctDamage;
            progs.CalculatePosition();
        }
        public void ChangeCameraPos(float value)
        {
            if (value > 0 && lastRoundBadger)
            {
                lastRoundBadger = false;
            }
            if (value < 0 && lastRoundHuman)
            {
                lastRoundHuman = false;
            }
            pctDamage += value;
            CalculatePosition();
        }

        public Vector3 GetTargetPosition()
        {
            return target.position;
        }

        public int GetCamCount()
        {
            return camRoundPosition;
        }
        //public void UpdateCamPos()
        //{
        //    Vector3 desiredPos = target.position;
        //    Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, camSpeed * Time.deltaTime);
        //    transform.position = smoothPos;
        //}
    }
}
