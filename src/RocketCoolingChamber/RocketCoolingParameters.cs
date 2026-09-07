using System;

namespace Leap71.RocketCooling
{
    /// <summary>
    /// Geometric and preliminary operating parameters for a regenerative cooling chamber.
    /// Geometric values use millimetres; thermal values use SI units.
    /// </summary>
    public sealed class RocketCoolingParameters
    {
        public float ChamberRadius { get; set; } = 35f;
        public float ChamberLength { get; set; } = 70f;
        public float ThroatRadius { get; set; } = 12f;
        public float NozzleExitRadius { get; set; } = 28f;
        public float ConvergentLength { get; set; } = 20f;
        public float DivergentLength { get; set; } = 45f;
        public float LinerThickness { get; set; } = 1.5f;
        public float ChannelHeight { get; set; } = 3f;
        public float ChannelWidth { get; set; } = 2f;
        public float OuterWallThickness { get; set; } = 3f;
        public float MinimumWebThickness { get; set; } = 1f;
        public float VoxelSizeMm { get; set; } = 0.5f;
        public float SampleStepMm { get; set; } = 1f;
        public uint ChannelCount { get; set; } = 24;
        public float HelixAngleDeg { get; set; } = 35f;
        public float CoolantMassFlow { get; set; } = 0.05f;
        public float CoolantInletTemperature { get; set; } = 300f;
        public float HotGasTemperature { get; set; } = 2500f;
        public float CoolantPressure { get; set; } = 2.0e6f;
        public float MaximumWallTemperature { get; set; } = 1100f;

        public float CoolantPressurePsi => CoolantPressure / 6894.757f;

        /// <summary>
        /// Validates dimensions before geometry generation.
        /// </summary>
        public void Validate()
        {
            if (ChamberRadius <= 0f || ChamberLength <= 0f)
                throw new ArgumentOutOfRangeException(nameof(ChamberRadius));
            if (ThroatRadius <= 0f || NozzleExitRadius <= 0f)
                throw new ArgumentOutOfRangeException(nameof(ThroatRadius));
            if (ConvergentLength <= 0f || DivergentLength <= 0f)
                throw new ArgumentOutOfRangeException(nameof(ConvergentLength));
            if (LinerThickness <= 0f || ChannelHeight <= 0f || ChannelWidth <= 0f)
                throw new ArgumentOutOfRangeException(nameof(LinerThickness));
            if (OuterWallThickness <= 0f || MinimumWebThickness <= 0f || ChannelCount < 1u)
                throw new ArgumentOutOfRangeException(nameof(OuterWallThickness));
            if (VoxelSizeMm <= 0f || SampleStepMm < 2f * VoxelSizeMm)
                throw new ArgumentOutOfRangeException(nameof(VoxelSizeMm));
            if (HelixAngleDeg <= 0f || HelixAngleDeg >= 80f)
                throw new ArgumentOutOfRangeException(nameof(HelixAngleDeg));

            float minimumChannelRadius = ThroatRadius + LinerThickness + 0.5f * ChannelHeight;
            float channelPitch = 2f * MathF.PI * minimumChannelRadius / ChannelCount;
            if (channelPitch <= ChannelWidth + MinimumWebThickness)
                throw new InvalidOperationException("Channel count leaves insufficient web thickness at the throat.");
        }
    }
}
