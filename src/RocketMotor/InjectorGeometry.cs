using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class InjectorGeometry
    {
        public int nCount = 24;
        public float fSwirlAngleDeg = 45.0f;
        public float fInnerRadius = 0.045f;
        public float fOuterRadius = 0.190f;
        public float fChannelWidth = 0.0015f;
        public float fChannelDepth = 0.0025f;

        public Voxels voxInjectorPlate(float fVoxelSize)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseCylinder oPlate = new BaseCylinder(oFrame, fOuterRadius * 2.0f, fChannelDepth);
            Voxels voxPlate = oPlate.voxConstruct(fVoxelSize);
            return voxPlate;
        }

        public Voxels voxInjectorElements(float fVoxelSize)
        {
            Lattice oLattice = new Lattice();
            float fAngleStep = 2.0f * MathF.PI / nCount;
            float fRadius = (fInnerRadius + fOuterRadius) / 2.0f;

            for (int i = 0; i < nCount; i++)
            {
                float fAngle = i * fAngleStep;
                float fX = fRadius * MathF.Cos(fAngle);
                float fY = fRadius * MathF.Sin(fAngle);
                Vector3 vecPos = new Vector3(fX, fY, 0);
                oLattice.AddSphere(vecPos, fChannelWidth * 2.0f);
            }

            Voxels voxInjectors = new Voxels(oLattice, fVoxelSize);
            return voxInjectors;
        }

        public Voxels voxInjectorAssembly(float fVoxelSize)
        {
            Voxels voxPlate = voxInjectorPlate(fVoxelSize);
            Voxels voxElements = voxInjectorElements(fVoxelSize);
            Voxels voxAssembly = Sh.voxUnion(voxPlate, voxElements);
            return voxAssembly;
        }

        public float fSwirlAngleRad => fSwirlAngleDeg * MathF.PI / 180.0f;
    }
}
