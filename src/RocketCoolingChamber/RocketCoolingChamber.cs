using System;
using System.Numerics;
using Leap71.ShapeKernel;
using PicoGK;

namespace Leap71.RocketCooling
{
    /// <summary>
    /// Generates the cooling-channel domain for a regenerative chamber.
    /// The original CoolCube.HelixHeatX generator remains unchanged.
    /// </summary>
    public sealed class RocketCoolingChamber
    {
        private readonly RocketCoolingParameters m_parameters;
        private readonly RocketCoolingProfile m_profile;

        public RocketCoolingChamber(RocketCoolingParameters parameters)
        {
            m_parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            m_profile = new RocketCoolingProfile(m_parameters);
        }

        /// <summary>
        /// Builds the connected helical coolant-channel volume.
        /// </summary>
        public Voxels VoxGetCoolingChannels()
        {
            Lattice channels = new Lattice();
            float sampleStepMm = m_parameters.SampleStepMm;
            uint sampleCount = (uint)Math.Ceiling(m_profile.TotalLength / sampleStepMm);
            float beamRadius = 0.5f * m_parameters.ChannelWidth;
            float helixAngleRad = m_parameters.HelixAngleDeg * MathF.PI / 180f;

            for (uint channelIndex = 0u; channelIndex < m_parameters.ChannelCount; channelIndex++)
            {
                Vector3 previousPoint = new Vector3();
                bool hasPreviousPoint = false;
                float phase = 2f * MathF.PI * channelIndex / m_parameters.ChannelCount;

                for (uint sampleIndex = 0u; sampleIndex <= sampleCount; sampleIndex++)
                {
                    float z = MathF.Min(sampleIndex * sampleStepMm, m_profile.TotalLength);
                    float radius = m_profile.GetChannelRadius(z);
                    float phi = phase + z * MathF.Tan(helixAngleRad) / radius;
                    Vector3 point = new Vector3(
                        radius * MathF.Cos(phi),
                        radius * MathF.Sin(phi),
                        z);

                    if (hasPreviousPoint)
                        channels.AddBeam(previousPoint, beamRadius, point, beamRadius);

                    previousPoint = point;
                    hasPreviousPoint = true;
                }
            }

            return new Voxels(channels);
        }

        /// <summary>
        /// Returns the axial profile used by the channel generator.
        /// </summary>
        public RocketCoolingProfile GetProfile()
        {
            return m_profile;
        }
    }
}
