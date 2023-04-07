namespace Structural_analysis.Models.SteelBeams
{
    public class StrengthSteelISectionOrtPlate: SteelISectionOrtPlate
    {
        const int MConvert = 100000; //перевод момента из тс*м в кгс*см
        const int QConvert = 1000; //перевод поперечной силы из тс в кгс
        public double Mmax { get; set; }
        public double Q1 { get; set; }
        public double Mmin { get; set; }
        public double Q2 { get; set; }

        public double m { get; set; } //коэффициент условий работы
        double[][] table8_16 = {
                                new double[] { 1.243, 1.248, 1.253, 1.258, 1.264, 1.269, 1.274, 1.279, 1.283, 1.267, 1.243 },
                                new double[] { 1.187, 1.191, 1.195, 1.199, 1.202, 1.206, 1.209, 1.212, 1.214, 1.160},
                                new double[] { 1.152, 1.155, 1.158, 1.162, 1.165, 1.168, 1.170, 1.172, 1.150 },
                                new double[] { 1.128, 1.131, 1.133, 1.136, 1.139, 1.142, 1,144, 1.145, 1.097 },
                                new double[] { 1.110, 1.113, 1.115, 1.118, 1.120, 1.123, 1.125, 1.126, 1.069 },
                                new double[] { 1.097, 1.099, 1.102, 1.104, 1.106, 1.109, 1.110, 1.106, 1.061 },
                                new double[] { 1.087, 1.089, 1.091, 1.093, 1.095, 1.097, 1.099, 1.079 },
                                new double[] { 1.078, 1.080, 1.082, 1.084, 1.086, 1.088, 1.090, 1.055 },
                                new double[] { 1.071, 1.073, 1.075, 1.077, 1.079, 1.081, 1.082, 1.044 },
                                new double[] { 1.065, 1.067, 1.069, 1.071, 1.073, 1.074, 1.076, 1.036 },
                                new double[] { 1.060, 1.062, 1.064, 1.066, 1.067, 1.069, 1.071, 1.031 },
                                new double[] { 1.035, 1.036, 1.037, 1.038, 1.039, 1.040, 1.019 },
                                new double[] { 1.024, 1.025, 1.026, 1.027, 1.028, 1.029, 1.017 },
                                new double[] { 1.019, 1.019, 1.020, 1.021, 1.021, 1.022, 1.015 },
                                new double[] { 1.015, 1.015, 1.016, 1.017, 1.018, 1.018 }
                               };

        public StrengthSteelISectionOrtPlate(double width1Top, double thick1Top, double width2Top, double thick2Top, double width3Top,
            double thick3Top, double width1Bottom, double thick1Bottom, double width2Bottom, double thick2Bottom, double width3Bottom,
            double thick3Bottom, double wallHeight, double wallThick, double widthOrtPlateTopLeft, double thickOrtPlateTopLeft, double widthOrtPlateTopRight,
            double thickOrtPlateTopRight, double widthOrtPlateBottomLeft, double thickOrtPlateBottomLeft, double widthOrtPlateBottomRight,
            double thickOrtPlateBottomRight, double hStringerTop, double thickStringerTop, int numberOfTopStringersLeft, int numberOfTopStringersRight,
            double hStringerBottom, double thickStringerBottom, int numberOfBottomStringersLeft, int numberOfBottomStringersRight,
            double yOrtPlateTop, double yOrtPlateBottom, int steelType, double Mmax, double Q1, double Mmin, double Q2, double m)
            : base(width1Top, thick1Top, width2Top, thick2Top, width3Top, thick3Top, width1Bottom, thick1Bottom, width2Bottom, thick2Bottom,
                  width3Bottom, thick3Bottom, wallHeight, wallThick, widthOrtPlateTopLeft, thickOrtPlateTopLeft, widthOrtPlateTopRight,
            thickOrtPlateTopRight, widthOrtPlateBottomLeft, thickOrtPlateBottomLeft, widthOrtPlateBottomRight, thickOrtPlateBottomRight, 
            hStringerTop, thickStringerTop, numberOfTopStringersLeft, numberOfTopStringersRight, hStringerBottom, thickStringerBottom, 
            numberOfBottomStringersLeft, numberOfBottomStringersRight, yOrtPlateTop, yOrtPlateBottom, steelType)
        {
            this.Mmax = Mmax * MConvert;
            this.Q1 = Q1 * QConvert;
            this.Mmin = Mmin * MConvert;
            this.Q2 = Q2 * QConvert;
            this.m = m;
        }

        public double TauM(double Q) // SP number 8.26
        {
            double TauM = Q / (wallHeight * wallThick);
            return TauM;
        }

        public double Qcritical(double Q) // SP formula 8.8
        {
            double TauMinEf, TauMaxEf, Qcritical;
            double kappa2; // see SP formula 8.27        

            TauMaxEf = Q * StaticMomentHalfSection / (MomentOfInertionX * wallThick);
            TauMinEf = Q * StaticMomentBottomOfWall / (MomentOfInertionX * wallThick);

            kappa2 = 1.25 - 0.25 * TauMinEf / TauMaxEf;

            Qcritical = kappa2 * (Rs * m * MomentOfInertionX * wallThick) / StaticMomentHalfSection;
            return Qcritical;
        }

        public double kappa1() // SP table 8.16
        {
            double Arows;
            double Acolumns;

            if (AreaBottom <= AreaTop)
            {
                Arows = AreaBottom / AreaWall;
            }
            else
            {
                Arows = AreaTop / AreaWall;
            }

            if (AreaBottom <= AreaTop)
            {
                Acolumns = (AreaBottom + AreaWall) / Area;
            }
            else
            {
                Acolumns = (AreaTop + AreaWall) / Area;
            }


            double kappa1;
            double ArowsUp = 0;
            double ArowsDown = 0;
            double AcolumnsLeft = 0;
            double AcolumnsRight = 0;
            int i1 = 0;
            int i2 = 0;
            int j1 = 0;
            int j2 = 0;
            if (Arows >= 0 && Arows < 0.1)
            {
                ArowsUp = 0;
                ArowsDown = 0.1;
                i1 = 0;
                i2 = 1;
            }
            else if (Arows >= 0.1 && Arows < 0.2)
            {
                ArowsUp = 0.1;
                ArowsDown = 0.2;
                i1 = 1;
                i2 = 2;
            }
            else if (Arows >= 0.2 && Arows < 0.3)
            {
                ArowsUp = 0.2;
                ArowsDown = 0.3;
                i1 = 2;
                i2 = 3;
            }
            else if (Arows >= 0.3 && Arows < 0.4)
            {
                ArowsUp = 0.3;
                ArowsDown = 0.4;
                i1 = 3;
                i2 = 4;
            }
            else if (Arows >= 0.4 && Arows < 0.5)
            {
                ArowsUp = 0.4;
                ArowsDown = 0.5;
                i1 = 4;
                i2 = 5;
            }
            else if (Arows >= 0.5 && Arows < 0.6)
            {
                ArowsUp = 0.5;
                ArowsDown = 0.6;
                i1 = 5;
                i2 = 6;
            }
            else if (Arows >= 0.6 && Arows < 0.7)
            {
                ArowsUp = 0.6;
                ArowsDown = 0.7;
                i1 = 6;
                i2 = 7;
            }
            else if (Arows >= 0.7 && Arows < 0.8)
            {
                ArowsUp = 0.7;
                ArowsDown = 0.8;
                i1 = 7;
                i2 = 8;
            }
            else if (Arows >= 0.8 && Arows < 0.9)
            {
                ArowsUp = 0.8;
                ArowsDown = 0.9;
                i1 = 8;
                i2 = 9;
            }
            else if (Arows >= 0.9 && Arows <= 1.0)
            {
                ArowsUp = 0.9;
                ArowsDown = 1.0;
                i1 = 9;
                i2 = 10;
            }
            else if (Arows >= 1.0 && Arows < 2.0)
            {
                ArowsUp = 1.0;
                ArowsDown = 2.0;
                i1 = 10;
                i2 = 11;
            }
            else if (Arows >= 2.0 && Arows < 3.0)
            {
                ArowsUp = 2.0;
                ArowsDown = 3.0;
                i1 = 11;
                i2 = 12;
            }
            else if (Arows >= 3.0 && Arows < 4.0)
            {
                ArowsUp = 3.0;
                ArowsDown = 4.0;
                i1 = 12;
                i2 = 13;
            }
            else if (Arows >= 4.0 && Arows <= 5.0)
            {
                ArowsUp = 4.0;
                ArowsDown = 5.0;
                i1 = 13;
                i2 = 14;
            }
            else
            {
                return kappa1 = 1;
            }

            if (Acolumns >= 0.01 && Acolumns < 0.1)
            {
                AcolumnsLeft = 0.01;
                AcolumnsRight = 0.1;
                j1 = 0;
                j2 = 1;
            }
            else if (Acolumns >= 0.1 && Acolumns < 0.2)
            {
                AcolumnsLeft = 0.1;
                AcolumnsRight = 0.2;
                j1 = 1;
                j2 = 2;
            }
            else if (Acolumns >= 0.2 && Acolumns < 0.3)
            {
                AcolumnsLeft = 0.2;
                AcolumnsRight = 0.3;
                j1 = 2;
                j2 = 3;
            }
            else if (Acolumns >= 0.3 && Acolumns < 0.4)
            {
                AcolumnsLeft = 0.3;
                AcolumnsRight = 0.4;
                j1 = 3;
                j2 = 4;
            }
            else if (Acolumns >= 0.4 && Acolumns < 0.5)
            {
                AcolumnsLeft = 0.4;
                AcolumnsRight = 0.5;
                j1 = 4;
                j2 = 5;
            }
            else if (Acolumns >= 0.5 && Acolumns < 0.6)
            {
                AcolumnsLeft = 0.5;
                AcolumnsRight = 0.6;
                j1 = 5;
                j2 = 6;
            }
            else if (Acolumns >= 0.6 && Acolumns < 0.7)
            {
                AcolumnsLeft = 0.6;
                AcolumnsRight = 0.7;
                j1 = 6;
                j2 = 7;
            }
            else if (Acolumns >= 0.7 && Acolumns < 0.8)
            {
                AcolumnsLeft = 0.7;
                AcolumnsRight = 0.8;
                j1 = 7;
                j2 = 8;
            }
            else if (Acolumns >= 0.8 && Acolumns < 0.9)
            {
                AcolumnsLeft = 0.8;
                AcolumnsRight = 0.9;
                j1 = 8;
                j2 = 9;
            }
            else if (Acolumns >= 0.9 && Acolumns <= 1.0)
            {
                AcolumnsLeft = 0.9;
                AcolumnsRight = 1.0;
                j1 = 9;
                j2 = 10;
            }
            else
            {
                return kappa1 = 1;
            }

            try
            {
                double value1 = table8_16[i1][j1];
                double value2 = table8_16[i1][j2];
                double value3 = table8_16[i2][j1];
                double value4 = table8_16[i2][j2];

                double kappaMedium1 = value3 - (value3 - value1) * (ArowsDown - Arows) / (ArowsDown - ArowsUp);
                double kappaMedium2 = value4 - (value4 - value2) * (ArowsDown - Arows) / (ArowsDown - ArowsUp);
                kappa1 = kappaMedium2 - (kappaMedium2 - kappaMedium1) * (AcolumnsRight - Acolumns) / (AcolumnsRight - AcolumnsLeft);
            }
            catch (IndexOutOfRangeException e)
            {
                return kappa1 = 1;
            }
            return kappa1;
        }

        public double kappa(double Q) //SP number 8.26
        {
            double kappa;

            double Alpha = Q / Qcritical(Q);

            double a = (AreaTop + AreaBottom) / AreaWall;

            double b = Math.Sqrt(1 - 0.0625 * Alpha * Alpha);

            if (TauM(Q) <= 0.25 * Rs)
            {
                return kappa = kappa1();
            }
            else if (TauM(Q) >= 0.25 * Rs && TauM(Q) <= Rs)
            {
                return kappa = kappa1() * ((Math.Sqrt(1 - Alpha * Alpha) + 2 * a * b) / (1 + 2 * a));
            }
            else
            {
                return kappa = 1.0;
            }
        }

        public double SigmaXBottom(double M, double Q) //SP formula 8.5
        {
            double SigmaXBottom;

            SigmaXBottom = M * YMassCentre / (kappa(Q) * MomentOfInertionX);

            return SigmaXBottom;
        }
        public double SigmaXTop(double M, double Q) //SP formula 8.5
        {
            double SigmaXTop;
            double totalHeight = thick3Bottom + thick2Bottom + thick1Bottom + wallHeight + thick3Top + thick2Top + thick1Top;

            SigmaXTop = -1 * M * (totalHeight - YMassCentre) / (kappa(Q) * MomentOfInertionX);

            return SigmaXTop;
        }
    }
}
