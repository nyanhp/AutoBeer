using AutoBeer.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBeer.Data.Classes
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BeerStyle Style { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Brewed { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Bottled { get; set; }
        [DisplayName("Total bottles")]
        public UInt16 TotalBottles { get; set; }
        [DisplayName("Remaining bottles")]
        public UInt16 RemainingBottles { get; set; }

        [DisplayName("Batch size (l)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double BatchSizeLiters { get; set; }
        [DisplayName("IBU")]
        public UInt16 BitternessUnits { get; set; }
        [DisplayName("EBC")]
        public UInt16 Color { get; set; }
        [DisplayName("Gravity °P measured")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double OriginalGravityPlato
        {
            get
            {
                return (668.72 * OriginalGravity) - 463.37 - (205.347 * Math.Pow(OriginalGravity, 2));
            }
        }
        [DisplayName("Gravity °P")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double OriginalGravityPlatoMeasured
        {
            get
            {
                return (668.72 * OriginalGravityMeasured) - 463.37 - (205.347 * Math.Pow(OriginalGravityMeasured, 2));
            }
        }

        [DisplayName("Specific gravity (SG)")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double OriginalGravity { get; set; }
        [DisplayName("Final gravity (SG)")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double FinalGravity { get; set; }


        [DisplayName("Specific gravity (SG) measured")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double OriginalGravityMeasured
        {
            get
            {
                if (Measurements.Count > 0)
                {
                    Measurements.Sort();
                    return Measurements.First().SpecificGravity;
                }

                return OriginalGravity;
            }
        }
        [DisplayName("Final gravity (SG) measured")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double FinalGravityMeasured
        {
            get
            {
                if (Measurements.Count > 0)
                {
                    Measurements.Sort();
                    return Measurements.Last().SpecificGravity;
                }

                return FinalGravity;
            }
        }

        public List<BeerMeasurementInfo> Measurements { get; set; }
        [DisplayName("ABV % (measured)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double AlcoholMeasured
        {
            get
            {
                if (Measurements.Count > 0)
                {
                    return 131 * (OriginalGravityMeasured - FinalGravityMeasured);
                }

                return 131 * (OriginalGravity - FinalGravity);
            }
        }
        [DisplayName("ABV % (manual entry)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Alcohol { get; set; }

        public Beer()
        {
            OriginalGravity = 1.055;
            Measurements = new List<BeerMeasurementInfo>();
        }
    }
}
