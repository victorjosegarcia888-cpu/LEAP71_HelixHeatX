using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class TurbopumpGeometry
    {
        public float fInletDiameter = 0.045f;
        public float fOutletDiameter = 0.134f;
        public int nBladeCount = 12;
        public float fHelixAngleDeg = 83.0f;
        public float fBladeThickness = 0.00152f;
        public float fHubRatio = 0.4f;
        public float fWallThickness = 0.002f;
        public float fSpeedRpm = 35000.0f;

        public float fHubDiameter => fHubRatio * fOutletDiameter;

        public float fShroudDiameter => fOutletDiameter;

        public Voxels voxImpeller(float fVoxelSize, int nAxialSteps = 8)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseCylinder oHub = new BaseCylinder(oFrame, fHubDiameter, fOutletDiameter * 0.5f);
            Voxels voxHub = oHub.voxConstruct(fVoxelSize);

            Lattice oLattice = new Lattice();
            float fAngleRad = fHelixAngleDeg * MathF.PI / 180.0f;

            for (int i = 0; i <= nAxialSteps; i++)
            {
                float fT = (float)i / nAxialSteps;
                float fR = Lerp(fHubDiameter / 2.0f, fShroudDiameter / 2.0f, fT);
                float fZ = fT * fOutletDiameter * 0.5f;
                float fAngle = fAngleRad + fT * 0.1f;

                Vector3 vecStart = new Vector3(0, 0, fZ);
                Vector3 vecEnd = new Vector3(fR * MathF.Cos(fAngle), fR * MathF.Sin(fAngle), fZ);
                oLattice.AddBeam(vecStart, fBladeThickness, vecEnd, fBladeThickness, false);
            }

            Voxels voxBlades = new Voxels(oLattice, fVoxelSize);
            Voxels voxImpeller = Sh.voxUnion(voxHub, voxBlades);
            return voxImpeller;
        }

        public Voxels voxDisk(float fVoxelSize)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseCylinder oDisk = new BaseCylinder(oFrame, fShroudDiameter, fWallThickness * 5.0f);
            Voxels voxDisk = oDisk.voxConstruct(fVoxelSize);
            return voxDisk;
        }

        private float Lerp(float fA, float fB, float fT)
        {
            return fA + (fB - fA) * fT;
        }
    }
}
