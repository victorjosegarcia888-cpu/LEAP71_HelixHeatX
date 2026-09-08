using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class CoolingChannels
    {
        public int nChannelCount = 120;
        public float fHelixAngleDeg = 35.0f;
        public float fChannelWidth = 0.002f;
        public float fChannelDepth = 0.003f;
        public float fChannelLength = 0.880f;
        public float fChannelRadius = 0.200f;

        public float fHelixAngleRad => fHelixAngleDeg * MathF.PI / 180.0f;

        public Voxels voxHelicalChannel(float fVoxelSize, int nChannelIndex)
        {
            Lattice oLattice = new Lattice();
            int nSteps = 100;
            float fAngleStep = fHelixAngleRad / nSteps;
            float fZStep = fChannelLength / nSteps;

            for (int i = 0; i <= nSteps; i++)
            {
                float fZ = i * fZStep;
                float fAngle = i * fAngleStep + 2.0f * MathF.PI * nChannelIndex / nChannelCount;
                float fX = fChannelRadius * MathF.Cos(fAngle);
                float fY = fChannelRadius * MathF.Sin(fAngle);
                Vector3 vecPos = new Vector3(fX, fY, fZ);
                oLattice.AddSphere(vecPos, fChannelWidth);
            }

            Voxels voxChannel = new Voxels(oLattice, fVoxelSize);
            return voxChannel;
        }

        public Voxels voxAllCoolingChannels(float fVoxelSize)
        {
            Voxels voxAll = new Voxels(fVoxelSize);
            for (int i = 0; i < nChannelCount; i++)
            {
                Voxels voxChannel = voxHelicalChannel(fVoxelSize, i);
                voxAll = Sh.voxUnion(voxAll, voxChannel);
            }
            return voxAll;
        }
    }
}
