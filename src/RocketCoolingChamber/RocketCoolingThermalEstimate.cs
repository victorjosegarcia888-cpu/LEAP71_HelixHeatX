using System;

namespace Leap71.RocketCooling
{
    /// <summary>
    /// Preliminary one-dimensional hydraulic and thermal estimate for one channel.
    /// It is a screening model, not a CFD or material qualification result.
    /// </summary>
    public sealed class RocketCoolingThermalEstimate
    {
        public float ReynoldsNumber { get; }
        public float HydraulicDiameterMm { get; }
        public float CoolantOutletTemperature { get; }
        public float PressureLossPa { get; }
        public float HeatRateW { get; }

        private RocketCoolingThermalEstimate(
            float reynoldsNumber,
            float hydraulicDiameterMm,
            float coolantOutletTemperature,
            float pressureLossPa,
            float heatRateW)
        {
            ReynoldsNumber = reynoldsNumber;
            HydraulicDiameterMm = hydraulicDiameterMm;
            CoolantOutletTemperature = coolantOutletTemperature;
            PressureLossPa = pressureLossPa;
            HeatRateW = heatRateW;
        }

        /// <summary>
        /// Estimates flow and heat pickup using constant fluid properties.
        /// </summary>
        public static RocketCoolingThermalEstimate Calculate(
            RocketCoolingParameters parameters,
            float densityKgPerM3,
            float dynamicViscosityPaS,
            float heatCapacityJPerKgK,
            float conductivityWPerMK,
            float channelLengthMm,
            float frictionFactor,
            float heatTransferCoefficientWPerM2K)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (densityKgPerM3 <= 0f || dynamicViscosityPaS <= 0f || heatCapacityJPerKgK <= 0f
                || conductivityWPerMK <= 0f || channelLengthMm <= 0f
                || frictionFactor <= 0f || heatTransferCoefficientWPerM2K <= 0f)
                throw new ArgumentOutOfRangeException(nameof(densityKgPerM3));

            float widthM = parameters.ChannelWidth * 0.001f;
            float heightM = parameters.ChannelHeight * 0.001f;
            float areaM2 = widthM * heightM;
            float wettedPerimeterM = 2f * (widthM + heightM);
            float hydraulicDiameterM = 4f * areaM2 / wettedPerimeterM;
            float channelMassFlow = parameters.CoolantMassFlow / parameters.ChannelCount;
            float velocityMPerS = channelMassFlow / (densityKgPerM3 * areaM2);
            float reynolds = densityKgPerM3 * velocityMPerS * hydraulicDiameterM / dynamicViscosityPaS;
            float wettedAreaM2 = wettedPerimeterM * channelLengthMm * 0.001f;
            float heatRate = heatTransferCoefficientWPerM2K
                * wettedAreaM2
                * (parameters.HotGasTemperature - parameters.CoolantInletTemperature);
            float outletTemperature = parameters.CoolantInletTemperature
                + heatRate / (channelMassFlow * heatCapacityJPerKgK);
            float pressureLoss = frictionFactor
                * (channelLengthMm * 0.001f / hydraulicDiameterM)
                * densityKgPerM3 * velocityMPerS * velocityMPerS * 0.5f;

            return new RocketCoolingThermalEstimate(
                reynolds,
                hydraulicDiameterM * 1000f,
                outletTemperature,
                pressureLoss,
                heatRate);
        }
    }
}
