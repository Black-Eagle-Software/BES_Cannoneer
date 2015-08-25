using Assets.managers;
using UnityEngine;

namespace Assets.factories {
    public class ProjectileFactory : Singleton<ProjectileFactory> {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        public void SpawnRoundShot(Vector2 origin, Vector2 velocity) {
            var rs = (GameObject) Instantiate(this.RoundShot, origin, Quaternion.identity);
            var rsrb = rs.GetComponent<Rigidbody2D>();
            rsrb.velocity += velocity;
        }

        #endregion


        #region Fields

        public GameObject RoundShot;
        public GameObject ExplosiveShot;

        #endregion

    }
}
