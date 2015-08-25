using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts {
    public class GunCrewPosition : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        public void AssignGunCrew( GameObject gc ) {
            this.GunCrew = gc;
            gc.transform.parent = this.transform;
            gc.GetComponent<GunCrew>().Deck = this.Deck;
        }

        #endregion


        #region Fields

        public GunCrewDeckAssignment Deck;
        public GameObject GunCrew;

        #endregion


    }
}
