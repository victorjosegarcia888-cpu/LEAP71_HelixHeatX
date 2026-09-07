using System;

namespace Leap71.RocketCooling
{
    /// <summary>
    /// Small executable smoke test for the parameter, profile and thermal layers.
    /// It intentionally does not initialize PicoGK.
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            RocketCoolingParameters parameters = new RocketCoolingParameters();
            RocketCoolingProfile profile = new RocketCoolingProfile(parameters);

            float chamberRadius = profile.GetInnerRadius(0f);
            float throatRadius = profile.GetInnerRadius(parameters.ChamberLength + parameters.ConvergentLength);
            float exitRadius = profile.GetInnerRadius(profile.TotalLength);

            if (chamberRadius <= throatRadius || exitRadius <= throatRadius)
                throw new InvalidOperationException("The axial chamber profile is not convergent-divergent.");

            RocketCoolingThermalEstimate estimate = RocketCoolingThermalEstimate.Calculate(
                parameters,
                densityKgPerM3: 800f,
                dynamicViscosityPaS: 0.0002f,
                heatCapacityJPerKgK: 2200f,
                conductivityWPerMK: 0.15f,
                channelLengthMm: profile.TotalLength,
                frictionFactor: 0.03f,
                heatTransferCoefficientWPerM2K: 1500f);

            Console.WriteLine("RocketCoolingChamber smoke test");
            Console.WriteLine($"Profile length: {profile.TotalLength:F1} mm");
            Console.WriteLine($"R chamber/throat/exit: {chamberRadius:F1}/{throatRadius:F1}/{exitRadius:F1} mm");
            Console.WriteLine($"Hydraulic diameter: {estimate.HydraulicDiameterMm:F2} mm");
            Console.WriteLine($"Reynolds number: {estimate.ReynoldsNumber:F0}");
            Console.WriteLine($"Coolant outlet temperature: {estimate.CoolantOutletTemperature:F1} K");
            Console.WriteLine($"Estimated pressure loss: {estimate.PressureLossPa:F0} Pa");
        }
    }
}
