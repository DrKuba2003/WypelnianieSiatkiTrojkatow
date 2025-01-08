using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow
{
    public class DrawingParams
    {
        public int m { get; set; }
        public float kd { get; set; }
        public float ks { get; set; }

        public Color[,] textureArr { get; set; }
        public int textureWidth { get; set; }
        public int textureHeight { get; set; }

        public Color[,] normalMapArr { get; set; }

        public Vector3 objectColor { get; set; }
        public Vector3 lightColor { get; set; }

        public bool isDrawFilling {  get; set; }
        public bool isSolidColor { get; set; }
        public bool isModifyNormalVec { get; set; }
        public bool isDrawMesh { get; set; }
        public bool isDrawControlPts { get; set; }
        public bool isDrawLightPos { get; set; }
        public bool isDrawLightReflektor { get; set; }
        public int reflektorM { get; set; }

        public DrawingParams(float kd, float ks, int m,
            Vector3 objectColor, Vector3 lightColor,
            bool isDrawFilling, bool isSolidColor, bool isModifyNormalVec,
            bool isDrawMesh, bool isDrawControlPts, bool isDrawLightPos,
            bool isDrawLightReflektor, int reflektorM)
        {
            this.kd = kd;
            this.ks = ks;
            this.m = m;
            this.objectColor = objectColor;
            this.lightColor = lightColor;
            this.isDrawFilling = isDrawFilling;
            this.isSolidColor = isSolidColor;
            this.isModifyNormalVec = isModifyNormalVec;
            this.isDrawMesh = isDrawMesh;
            this.isDrawControlPts = isDrawControlPts;
            this.isDrawLightPos = isDrawLightPos;
            this.isDrawLightReflektor = isDrawLightReflektor;
            this.reflektorM = reflektorM;
        }
    }
}
