using System;
using UnityEngine;

namespace CareBoo.Serially.Samples.Animals
{
    public class InspectorExample
    {
        [TypeFilter(typeof(IPet))]
        public SerializableType MyFavoriteTypeOfPet;

        [TypeFilter(typeof(Dog))]
        public SerializableType MyFavoriteTypeOfDog;

        [ShowSerializeReference]
        [SerializeReference]
        public IPet MyCurrentPet;

        [ShowSerializeReference]
        [SerializeReference]
        public IPet[] ArrayOfMyPets;

        public GameObject GameObjectReference;
    }
}
