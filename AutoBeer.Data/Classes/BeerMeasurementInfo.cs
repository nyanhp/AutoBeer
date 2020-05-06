using AutoBeer.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBeer.Data.Classes
{
    public class BeerMeasurementInfo : IEquatable<BeerMeasurementInfo>, IComparable<BeerMeasurementInfo>
    {
        [DisplayName("Tilt color")]
        public TiltColor TiltColor { get; set; }
        [DisplayName("Beer name")]
        public string BeerName { get; set; }
        [DisplayName("Time stamp")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Timestamp { get; set; }
        [DisplayName("Specific gravity (SG)")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double SpecificGravity { get; set; }
        [DisplayName("Temperature (°F)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double TemperatureFahrenheit { get; set; }
        [DisplayName("Temperature (°C)")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double TemperatureCelsius
        {
            get
            {
                return (TemperatureFahrenheit - 32) * 5/9;
            }
        }
        [DisplayName("Comment")]
        public string Comment { get; set; }

        public long Id
        {
            get { return Timestamp.Ticks; }
        }

        public int CompareTo([AllowNull] BeerMeasurementInfo other)
        {
            if (other.Id == Id) return 1;

            return Id.CompareTo(other.Id);
        }

        public bool Equals([AllowNull] BeerMeasurementInfo other)
        {
            if (null == other) return false;

            return Id.Equals(other.Id);
        }
    }
}
