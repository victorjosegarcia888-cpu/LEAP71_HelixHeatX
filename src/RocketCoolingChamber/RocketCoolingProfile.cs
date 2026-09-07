using System;

namespace Leap71.RocketCooling
{
    /// <summary>
    /// Continuous axial profile for chamber, convergent section, throat and nozzle.
    /// </summary>
    public sealed class RocketCoolingProfile
    {
        private readonly RocketCoolingParameters m_parameters;

        public float TotalLength => m_parameters.ChamberLength
            + m_parameters.ConvergentLength
            + m_parameters.DivergentLength;

        public RocketCoolingProfile(RocketCoolingParameters parameters)
        {
            m_parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            m_parameters.Validate();
        }

        /// <summary>
        /// Returns the hot-gas radius at an axial position in millimetres.
        /// </summary>
        public float GetInnerRadius(float z)
        {
            if (z <= 0f)
                return m_parameters.ChamberRadius;
            if (z <= m_parameters.ChamberLength)
                return m_parameters.ChamberRadius;

            float convergentEnd = m_parameters.ChamberLength + m_parameters.ConvergentLength;
            if (z <= convergentEnd)
            {
                float ratio = (z - m_parameters.ChamberLength) / m_parameters.ConvergentLength;
                return SmoothStep(m_parameters.ChamberRadius, m_parameters.ThroatRadius, ratio);
            }

            float divergentRatio = (z - convergentEnd) / m_parameters.DivergentLength;
            return SmoothStep(m_parameters.ThroatRadius, m_parameters.NozzleExitRadius, divergentRatio);
        }

        /// <summary>
        /// Returns the centreline radius of a cooling channel at an axial position.
        /// </summary>
        public float GetChannelRadius(float z)
        {
            return GetInnerRadius(z)
                + m_parameters.LinerThickness
                + 0.5f * m_parameters.ChannelHeight;
        }

        /// <summary>
        /// Returns the outer material radius at an axial position.
        /// </summary>
        public float GetOuterRadius(float z)
        {
            return GetInnerRadius(z)
                + m_parameters.LinerThickness
                + m_parameters.ChannelHeight
                + m_parameters.OuterWallThickness;
        }

        private static float SmoothStep(float start, float end, float ratio)
        {
            ratio = Math.Clamp(ratio, 0f, 1f);
            float smoothRatio = ratio * ratio * (3f - 2f * ratio);
            return start + (end - start) * smoothRatio;
        }
    }
}
