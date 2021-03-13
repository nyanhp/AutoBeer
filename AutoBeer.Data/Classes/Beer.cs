using AutoBeer.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBeer.Data.Classes
{
    public class Beer
    {
        [Key]
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
        [DisplayName("Gravity °P")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double OriginalGravityPlato
        {
            get
            {
                return (668.72 * OriginalGravity) - 463.37 - (205.347 * Math.Pow(OriginalGravity, 2));
            }
        }
        [DisplayName("Specific gravity (SG)")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double OriginalGravity { get; set; }
        [DisplayName("Final gravity (SG)")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double FinalGravity { get; set; }

        //public virtual ICollection<BeerMeasurementInfo> Measurements { get; set; }
        [DisplayName("ABV % (measured)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double AlcoholMeasured
        {
            get
            {
                return 4.5;
                /*if (Measurements.Count > 0)
                {
                    Measurements.ToList().Sort();
                    var first = Measurements.First();
                    var last = Measurements.Last();
                    return 131 * (first.SpecificGravity - last.SpecificGravity);
                }

                return 131 * (OriginalGravity - FinalGravity);  */
            }
        }
        [DisplayName("ABV % (manual entry)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Alcohol { get; set; }

        public Beer()
        {
            OriginalGravity = 1.055;
            //Measurements = new List<BeerMeasurementInfo>();
        }
    }
}
