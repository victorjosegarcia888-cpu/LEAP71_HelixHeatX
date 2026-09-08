using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class PreburnerGeometry
    {
        public float fMajorRadius = 0.060f;
        public float fPipeRadius = 0.010f;
        public float fWallThickness = 0.002f;
        public float fLength = 0.120f;
        public int nFeedPorts = 2;

        public float fOuterRadius => fMajorRadius + fPipeRadius + fWallThickness;

        public Voxels voxToroidalManifold(float fVoxelSize)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseTorus oTorus = new BaseTorus(oFrame, fMajorRadius, fPipeRadius + fWallThickness);
            Voxels voxManifold = oTorus.voxConstruct(fVoxelSize);
            return voxManifold;
        }

        public Voxels voxToroidalCore(float fVoxelSize)
        {
            LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
            BaseTorus oCore = new BaseTorus(oFrame, fMajorRadius, fPipeRadius);
            Voxels voxCore = oCore.voxConstruct(fVoxelSize);
            return voxCore;
        }

        public Voxels voxPreburnerBody(float fVoxelSize)
        {
            Voxels voxManifold = voxToroidalManifold(fVoxelSize);
            Voxels voxCore = voxToroidalCore(fVoxelSize);
            Voxels voxBody = Sh.voxSubtract(voxManifold, voxCore);
            return voxBody;
        }

        public Voxels voxFeedPorts(float fVoxelSize)
        {
            Lattice oLattice = new Lattice();
            for (int i = 0; i < nFeedPorts; i++)
            {
                float fAngle = (float)i / nFeedPorts * 2.0f * MathF.PI;
                Vector3 vecPos = new Vector3(fMajorRadius * MathF.Cos(fAngle), fMajorRadius * MathF.Sin(fAngle), 0);
                oLattice.AddSphere(vecPos, fPipeRadius);
            }
            Voxels voxPorts = new Voxels(oLattice, fVoxelSize);
            return voxPorts;
        }
    }
}
