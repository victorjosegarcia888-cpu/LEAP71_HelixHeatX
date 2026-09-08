using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class NozzleGeometry
    {
        public float fThroatRadius = 0.099f;
        public float fExitRadius = 0.530f;
        public float fExpansionRatio = 400.0f;
        public float fThetaInDeg = 25.0f;
        public float fThetaExpDeg = 34.0f;
        public float fThetaExitDeg = 10.5f;
        public float fWallThickness = 0.003f;
        public float fLength = 0.600f;

        public float fRadiusAt(float fZ)
        {
            if (fZ < 0.0f) return fThroatRadius;
            if (fZ > fLength) return fExitRadius;

            float fT = fZ / fLength;
            if (fT < 0.2f)
            {
                return fThroatRadius + (fExitRadius - fThroatRadius) * EaseInQuadratic(fT / 0.2f);
            }
            else if (fT < 0.8f)
            {
                return fThroatRadius + (fExitRadius - fThroatRadius) * (0.3f + 0.7f * EaseInOutCubic((fT - 0.2f) / 0.6f));
            }
            else
            {
                return fThroatRadius + (fExitRadius - fThroatRadius) * EaseOutQuadratic((fT - 0.8f) / 0.2f);
            }
        }

        public float fWallOuterRadiusAt(float fZ)
        {
            return fRadiusAt(fZ) + fWallThickness;
        }

        public Voxels voxNozzleProfile(float fVoxelSize, int nSamples = 64)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseCylinder oCylinder = new BaseCylinder(oFrame, fThroatRadius * 2.0f, fLength);
            oCylinder.SetLengthSteps(nSamples);
            oCylinder.SetRadius(new SurfaceModulation(fGetNozzleRadiusModulation));
            Voxels voxNozzle = oCylinder.voxConstruct(fVoxelSize);
            return voxNozzle;
        }

        public Voxels voxNozzleWithWall(float fVoxelSize, int nSamples = 64)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseCylinder oInner = new BaseCylinder(oFrame, fThroatRadius * 2.0f, fLength);
            oInner.SetLengthSteps(nSamples);
            oInner.SetRadius(new SurfaceModulation(fGetNozzleRadiusModulation));
            Voxels voxInner = oInner.voxConstruct(fVoxelSize);

            BaseCylinder oOuter = new BaseCylinder(oFrame, fExitRadius * 2.0f + fWallThickness * 2.0f, fLength);
            oOuter.SetLengthSteps(nSamples);
            oOuter.SetRadius(new SurfaceModulation(fGetNozzleOuterRadiusModulation));
            Voxels voxOuter = oOuter.voxConstruct(fVoxelSize);

            Voxels voxNozzle = Sh.voxSubtract(voxOuter, voxInner);
            return voxNozzle;
        }

        private float fGetNozzleRadiusModulation(float fPhi, float fLengthRatio)
        {
            float fZ = fLengthRatio * fLength;
            return fRadiusAt(fZ) * 2.0f;
        }

        private float fGetNozzleOuterRadiusModulation(float fPhi, float fLengthRatio)
        {
            float fZ = fLengthRatio * fLength;
            return fWallOuterRadiusAt(fZ) * 2.0f;
        }

        private float EaseInQuadratic(float fT)
        {
            return fT * fT;
        }

        private float EaseOutQuadratic(float fT)
        {
            return fT * (2.0f - fT);
        }

        private float EaseInOutCubic(float fT)
        {
            return fT < 0.5f ? 4.0f * fT * fT * fT : 1.0f - MathF.Pow(-2.0f * fT + 2.0f, 3.0f) / 2.0f;
        }
    }
}
