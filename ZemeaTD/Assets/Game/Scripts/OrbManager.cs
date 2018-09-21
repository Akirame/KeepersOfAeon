using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour {    
    private void Start() {
        ElementalOrb.OnConsumption += DeactivateOrb;
        PlayerDetector.ReturnOrb += ReturnOrbToPlayer;
    }
    private void DeactivateOrb(ElementalOrb e) {
        e.gameObject.transform.position = this.transform.position;        
    }
    private void ReturnOrbToPlayer(PlayerDetector p, ElementalOrb e) {
        e.transform.position = p.orbPosition.transform.position;
        e.Throw();
    }
}
