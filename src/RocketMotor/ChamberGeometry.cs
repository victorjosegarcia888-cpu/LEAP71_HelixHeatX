using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class ChamberGeometry
    {
        public float fChamberRadius = 0.190f;
        public float fChamberLength = 0.200f;
        public float fThroatRadius = 0.099f;
        public float fConvergentLength = 0.080f;
        public float fNozzleExitRadius = 0.530f;
        public float fNozzleLength = 0.600f;
        public float fLinerThickness = 0.002f;
        public float fOuterWallThickness = 0.003f;
        public int nChannelCount = 120;
        public float fChannelWidth = 0.002f;
        public float fChannelHeight = 0.003f;
        public float fHelixAngleDeg = 35.0f;

        public float fInnerRadiusAt(float fZ)
        {
            float fLc = fChamberLength;
            float fLconv = fConvergentLength;
            float fLtotal = fChamberLength + fConvergentLength + fNozzleLength;

            if (fZ < 0.0f) return fChamberRadius;
            if (fZ <= fLc) return fChamberRadius;

            float fT = (fZ - fLc) / fLconv;
            if (fT > 1.0f) fT = 1.0f;
            float fRconv = Lerp(fChamberRadius, fThroatRadius, fT);

            if (fZ <= fLc + fLconv) return fRconv;

            float fTnoz = (fZ - fLc - fLconv) / fNozzleLength;
            if (fTnoz > 1.0f) fTnoz = 1.0f;
            return Lerp(fThroatRadius, fNozzleExitRadius, fTnoz);
        }

        public float fOuterRadiusAt(float fZ)
        {
            return fInnerRadiusAt(fZ) + fLinerThickness + fChannelHeight + fOuterWallThickness;
        }

        public float fChannelRadiusAt(float fZ)
        {
            return fInnerRadiusAt(fZ) + fLinerThickness + 0.5f * fChannelHeight;
        }

        public float fHelixAngleRad => fHelixAngleDeg * MathF.PI / 180.0f;

        public (float r, float fTheta, float fZ) oChannelPosition(int nChannelIndex, float fZ)
        {
            float fPhi = 2.0f * MathF.PI * nChannelIndex / nChannelCount + fZ * MathF.Tan(fHelixAngleRad) / fChannelRadiusAt(fZ);
            return (fChannelRadiusAt(fZ), fPhi, fZ);
        }

        public Voxels voxChamberShell(float fVoxelSize)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseCylinder oCylinder = new BaseCylinder(oFrame, fChamberRadius * 2.0f, fChamberLength);
            oCylinder.SetLengthSteps(500);
            oCylinder.SetRadius(new SurfaceModulation(fGetChamberRadiusModulation));
            Voxels voxChamber = oCylinder.voxConstruct(fVoxelSize);
            return voxChamber;
        }

        public Voxels voxNozzleShell(float fVoxelSize)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, fChamberLength));
            BaseCylinder oCylinder = new BaseCylinder(oFrame, fThroatRadius * 2.0f, fNozzleLength);
            oCylinder.SetLengthSteps(500);
            oCylinder.SetRadius(new SurfaceModulation(fGetNozzleRadiusModulation));
            Voxels voxNozzle = oCylinder.voxConstruct(fVoxelSize);
            return voxNozzle;
        }

        public Voxels voxCombinedChamberNozzle(float fVoxelSize)
        {
            Voxels voxChamber = voxChamberShell(fVoxelSize);
            Voxels voxNozzle = voxNozzleShell(fVoxelSize);
            Voxels voxCombined = Sh.voxUnion(voxChamber, voxNozzle);
            return voxCombined;
        }

        private float fGetChamberRadiusModulation(float fPhi, float fLengthRatio)
        {
            float fZ = fLengthRatio * fChamberLength;
            float fR = fInnerRadiusAt(fZ) + fLinerThickness;
            return fR * 2.0f;
        }

        private float fGetNozzleRadiusModulation(float fPhi, float fLengthRatio)
        {
            float fZ = fConvergentLength + fLengthRatio * fNozzleLength;
            float fR = fInnerRadiusAt(fZ) + fLinerThickness;
            return fR * 2.0f;
        }

        private float Lerp(float fA, float fB, float fT)
        {
            return fA + (fB - fA) * fT;
        }
    }
}
