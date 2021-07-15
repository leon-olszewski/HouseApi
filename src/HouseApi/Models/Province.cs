using System.Collections.Generic;
using System.Linq;

namespace HouseApi.Models
{
    public class Province
    {
        private static List<Province> _provinces;

        static Province()
        {
            _provinces = new List<Province>
            {
                BritishColumbia!,
                Alberta!,
                Saskatchewan!,
                Manitoba!,
                Ontario!,
                Quebec!,
                Newfoundland!,
                PrinceEdwardIsland!,
                Novascotia!,
                Newbrunswick!,
                YukonTerritory!,
                NorthWestTerritories!,
                Nunavut!
            };
        }

        private Province(string nameCode)
        {
            NameCode = nameCode;
        }

        public string NameCode { get; }

        public static Province BritishColumbia { get; } = new Province("BC");
        public static Province Alberta { get; } = new Province("AB");
        public static Province Saskatchewan { get; } = new Province("SK");
        public static Province Manitoba { get; } = new Province("MB");
        public static Province Ontario { get; } = new Province("ON");
        public static Province Quebec { get; } = new Province("QC");
        public static Province Newfoundland { get; } = new Province("NL");
        public static Province PrinceEdwardIsland { get; } = new Province("PE");
        public static Province Novascotia { get; } = new Province("NS");
        public static Province Newbrunswick { get; } = new Province("NB");
        public static Province YukonTerritory { get; } = new Province("YT");
        public static Province NorthWestTerritories { get; } = new Province("NT");
        public static Province Nunavut { get; } = new Province("NU");

        public static Province GetProvinceByNameCode(string province) => _provinces.Single(p => p.NameCode == province);

        public static implicit operator string(Province province) => province.NameCode;
    }
}
