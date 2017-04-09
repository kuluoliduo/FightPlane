using UnityEngine;
using System.Collections;

namespace ClashGame
{
    public class BuildingMark : MonoBehaviour
    {
        private uint buildingInsID;

        public uint BuildingInsID
        {
            get { return buildingInsID; }
            set { buildingInsID = value; }
        }
    }
}
