namespace Structural_analysis.Models.SteelBeams
{
    public class SteelISectionOrtPlate: ICrossSection
    {
        const int mmInCm = 10; //переводим ммилиметры в сантиметры
        protected double width1Top, thick1Top;
        protected double width2Top, thick2Top;
        protected double width3Top, thick3Top;
        protected double width1Bottom, thick1Bottom;
        protected double width2Bottom, thick2Bottom;
        protected double width3Bottom, thick3Bottom;
        protected double wallHeight, wallThick;
        protected double widthOrtPlateTopLeft, thickOrtPlateTopLeft;
        protected double widthOrtPlateTopRight, thickOrtPlateTopRight;
        protected double widthOrtPlateBottomLeft, thickOrtPlateBottomLeft;
        protected double widthOrtPlateBottomRight, thickOrtPlateBottomRight;
        protected double hStringerTop, thickStringerTop;
        protected int numberOfTopStringersLeft;
        protected int numberOfTopStringersRight;
        protected double hStringerBottom, thickStringerBottom;
        protected int numberOfBottomStringersLeft;
        protected int numberOfBottomStringersRight;
        protected double yOrtPlateTop, yOrtPlateBottom;

        protected int steelType;
        public double Rs { get; set; }
        public double Ry { get; set; }
        public double Area { get; set; }
        public double AreaTop { get; set; }
        public double AreaBottom { get; set; }
        public double AreaWall { get; set; }
        public double StaticMoment { get; set; }
        public double StaticMomentHalfSection { get; set; }
        public double StaticMomentBottomOfWall { get; set; }
        public double StaticMomentTopOfWall { get; set; }
        public double YMassCentre { get; set; }
        public double MomentOfInertionX { get; set; }
        public SteelISectionOrtPlate(double width1Top, double thick1Top, double width2Top, double thick2Top, double width3Top, double thick3Top,
            double width1Bottom, double thick1Bottom, double width2Bottom, double thick2Bottom, double width3Bottom, double thick3Bottom,
            double wallHeight, double wallThick, double widthOrtPlateTopLeft, double thickOrtPlateTopLeft, double widthOrtPlateTopRight, 
            double thickOrtPlateTopRight, double widthOrtPlateBottomLeft, double thickOrtPlateBottomLeft, double widthOrtPlateBottomRight,
            double thickOrtPlateBottomRight, double hStringerTop, double thickStringerTop, int numberOfTopStringersLeft, int numberOfTopStringersRight,
            double hStringerBottom, double thickStringerBottom, int numberOfBottomStringersLeft, int numberOfBottomStringersRight,
            double yOrtPlateTop, double yOrtPlateBottom, int steelType)
        {
            this.width1Top = width1Top / mmInCm;
            this.thick1Top = thick1Top / mmInCm;
            this.width2Top = width2Top / mmInCm;
            this.thick2Top = thick2Top / mmInCm;
            this.width3Top = width3Top / mmInCm;
            this.thick3Top = thick3Top / mmInCm;
            this.width1Bottom = width1Bottom / mmInCm;
            this.thick1Bottom = thick1Bottom / mmInCm;
            this.width2Bottom = width2Bottom / mmInCm;
            this.thick2Bottom = thick2Bottom / mmInCm;
            this.width3Bottom = width3Bottom / mmInCm;
            this.thick3Bottom = thick3Bottom / mmInCm;
            this.wallHeight = wallHeight / mmInCm;
            this.wallThick = wallThick / mmInCm;
            this.widthOrtPlateTopLeft = widthOrtPlateTopLeft / mmInCm;
            this.thickOrtPlateTopLeft = thickOrtPlateTopLeft / mmInCm;
            this.widthOrtPlateTopRight = widthOrtPlateTopRight / mmInCm;
            this.thickOrtPlateTopRight = thickOrtPlateTopRight / mmInCm;
            this.widthOrtPlateBottomLeft = widthOrtPlateBottomLeft / mmInCm;
            this.thickOrtPlateBottomLeft = thickOrtPlateBottomLeft / mmInCm;
            this.widthOrtPlateBottomRight = widthOrtPlateBottomRight / mmInCm;
            this.thickOrtPlateBottomRight = thickOrtPlateBottomRight / mmInCm;
            this.hStringerTop = hStringerTop / mmInCm;
            this.thickStringerTop = thickStringerTop / mmInCm;
            this.numberOfTopStringersLeft = numberOfTopStringersLeft;
            this.numberOfTopStringersRight = numberOfTopStringersRight;
            this.hStringerBottom = hStringerBottom / mmInCm;
            this.thickStringerBottom = thickStringerBottom / mmInCm;
            this.numberOfBottomStringersLeft = numberOfBottomStringersLeft;
            this.numberOfBottomStringersRight = numberOfBottomStringersRight;
            this.yOrtPlateTop = yOrtPlateTop / mmInCm;
            this.yOrtPlateBottom = yOrtPlateBottom/ mmInCm;

            if (steelType == 1)
            {
                Ry = 2090;
                Rs = 0.58 * 2294 / 1.09;
            }
            else if (steelType == 2)
            {
                Ry = 3000;
                Rs = 0.58 * 3517 / 1.165;
            }
            else
            {
                Ry = 3550;
                Rs = 0.58 * 3976.83 / 1.125;
            }

            Area = area();
            AreaTop = areaTop();
            AreaBottom = areaBottom();
            AreaWall = areaWall();
            StaticMoment = staticMoment();
            YMassCentre = yMassCentre();
            StaticMomentHalfSection = staticMomentHalfSection();
            StaticMomentBottomOfWall = staticMomentBottomOfWall();
            StaticMomentTopOfWall = staticMomentTopOfWall();
            MomentOfInertionX = momentOfInertionX();
        }

        public double area() //полная площадь !!!ПРОВЕРЕНО
        {
            double A = width1Top * thick1Top + width2Top * thick2Top + width3Top * thick3Top + width1Bottom * thick1Bottom +
                width2Bottom * thick2Bottom + width3Bottom * thick3Bottom + wallHeight * wallThick + widthOrtPlateTopLeft * thickOrtPlateTopLeft +
                widthOrtPlateTopRight * thickOrtPlateTopRight + widthOrtPlateBottomLeft * thickOrtPlateBottomLeft +
                widthOrtPlateBottomRight * thickOrtPlateBottomRight +
                hStringerTop * thickStringerTop * (numberOfTopStringersLeft + numberOfTopStringersRight) +
                hStringerBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight);
            return A;
        }

        public double areaTop() //площадь верхнего пояса
        {
            double A = width1Top * thick1Top + width2Top * thick2Top + width3Top * thick3Top + widthOrtPlateTopLeft * thickOrtPlateTopLeft+
                widthOrtPlateTopRight * thickOrtPlateTopRight + hStringerTop * thickStringerTop * (numberOfTopStringersLeft + numberOfTopStringersRight);
            return A;
        }

        public double areaBottom() //площадь нижнего пояса
        {
            double A = width1Bottom * thick1Bottom + width2Bottom * thick2Bottom + width3Bottom * thick3Bottom +
                widthOrtPlateBottomLeft * thickOrtPlateBottomLeft + widthOrtPlateBottomRight * thickOrtPlateBottomRight +
                hStringerBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight);
            return A;
        }

        public double areaWall() //площадь стенки
        {
            double A = wallHeight * wallThick;
            return A;
        }

        public double staticMoment() //статический момент инерции относительно осм 0-0 !!!ПРОВЕРЕНО
        {
            double S;
            S = width3Bottom * thick3Bottom * thick3Bottom / 2 + width2Bottom * thick2Bottom * (thick2Bottom / 2 + thick3Bottom) +
                width1Bottom * thick1Bottom * (thick1Bottom / 2 + thick2Bottom + thick3Bottom) + wallHeight * wallThick * (wallHeight / 2 +
                thick1Bottom + thick2Bottom + thick3Bottom) + width1Top * thick1Top * (thick1Top / 2 + wallHeight + thick1Bottom + thick2Bottom
                + thick3Bottom) + width2Top * thick2Top * (thick2Top / 2 + thick1Top + wallHeight + thick1Bottom + thick2Bottom
                + thick3Bottom) + width3Top * thick3Top * (thick3Top / 2 + thick2Top + thick1Top + wallHeight + thick1Bottom + thick2Bottom +
                thick3Bottom) + widthOrtPlateBottomLeft * thickOrtPlateBottomLeft * (thick1Bottom + thick2Bottom + thick3Bottom -
                yOrtPlateBottom - thickOrtPlateBottomLeft / 2) + widthOrtPlateBottomRight * thickOrtPlateBottomRight * (thick1Bottom +
                thick2Bottom + thick3Bottom - yOrtPlateBottom - thickOrtPlateBottomRight / 2) +
                hStringerBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight) * (thick1Bottom +
                thick2Bottom + thick3Bottom - yOrtPlateBottom + hStringerBottom / 2) +
                widthOrtPlateTopLeft * thickOrtPlateTopLeft * (thick1Bottom + thick2Bottom + thick3Bottom + wallHeight + yOrtPlateTop +
                thickOrtPlateTopLeft / 2) +
                widthOrtPlateTopRight * thickOrtPlateTopRight * (thick1Bottom + thick2Bottom + thick3Bottom + wallHeight + yOrtPlateTop +
                thickOrtPlateTopRight / 2) +
                hStringerTop * thickStringerTop * (numberOfTopStringersLeft + numberOfTopStringersRight) * (thick1Bottom + thick2Bottom +
                thick3Bottom + wallHeight + yOrtPlateTop - hStringerTop / 2);
                ;
            return S;
        }

        public double staticMomentHalfSection() //отсеченный момент инерции по центру тяжести нижней части  !!!ПРОВЕРЕНО
        {
            double S0, S, yCentre;
            S0 = width3Bottom * thick3Bottom * thick3Bottom / 2 + width2Bottom * thick2Bottom * (thick2Bottom / 2 + thick3Bottom) +
                width1Bottom * thick1Bottom * (thick1Bottom / 2 + thick2Bottom + thick3Bottom) +
                widthOrtPlateBottomLeft * thickOrtPlateBottomLeft * (thick1Bottom + thick2Bottom + thick3Bottom -
                yOrtPlateBottom - thickOrtPlateBottomLeft / 2) + widthOrtPlateBottomRight * thickOrtPlateBottomRight * (thick1Bottom +
                thick2Bottom + thick3Bottom - yOrtPlateBottom - thickOrtPlateBottomRight / 2) +
                hStringerBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight) * (thick1Bottom +
                thick2Bottom + thick3Bottom - yOrtPlateBottom + hStringerBottom / 2) +
                (YMassCentre - thick3Bottom - thick2Bottom - thick1Bottom) * wallThick *
                ((YMassCentre - thick3Bottom - thick2Bottom - thick1Bottom) / 2 + thick3Bottom + thick2Bottom + thick1Bottom);
            double area = width3Bottom * thick3Bottom + width2Bottom * thick2Bottom + width1Bottom * thick1Bottom +
                widthOrtPlateBottomLeft * thickOrtPlateBottomLeft + widthOrtPlateBottomRight * thickOrtPlateBottomRight +
                hStringerBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight) +
                (YMassCentre - thick3Bottom - thick2Bottom - thick1Bottom) * wallThick;
            yCentre = S0 / area;
            S = area * (YMassCentre - yCentre);
            return S;
        }

        public double staticMomentBottomOfWall()
        {
            double S0, S, yCentre;
            S0 = width3Bottom * thick3Bottom * thick3Bottom / 2 + width2Bottom * thick2Bottom * (thick2Bottom / 2 + thick3Bottom) +
                width1Bottom * thick1Bottom * (thick1Bottom / 2 + thick2Bottom + thick3Bottom) +
                widthOrtPlateBottomLeft * thickOrtPlateBottomLeft * (thick1Bottom + thick2Bottom + thick3Bottom -
                yOrtPlateBottom - thickOrtPlateBottomLeft / 2) + widthOrtPlateBottomRight * thickOrtPlateBottomRight * (thick1Bottom +
                thick2Bottom + thick3Bottom - yOrtPlateBottom - thickOrtPlateBottomRight / 2) + 
                yOrtPlateBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight) * (thick1Bottom +
                thick2Bottom + thick3Bottom - yOrtPlateBottom / 2);
            double area = width3Bottom * thick3Bottom + width2Bottom * thick2Bottom + width1Bottom * thick1Bottom +
                widthOrtPlateBottomLeft * thickOrtPlateBottomLeft + widthOrtPlateBottomRight * thickOrtPlateBottomRight +
                yOrtPlateBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight);
            yCentre = S0 / area;
            S = area * (YMassCentre - yCentre);
            return S;
        }

        public double staticMomentTopOfWall()
        {
            double S0, S, yCentre, yCentreTop;
            S0 = width3Top * thick3Top * thick3Top / 2 + width2Top * thick2Top * (thick2Top / 2 + thick3Top) +
                width1Top * thick1Top * (thick1Top / 2 + thick2Top + thick3Top) + 
                widthOrtPlateTopLeft * thickOrtPlateTopLeft * (thick1Top + thick2Top + thick3Top - yOrtPlateTop - thickOrtPlateTopLeft / 2) +
                widthOrtPlateTopRight * thickOrtPlateTopRight * (thick1Top + thick2Top + thick3Top - yOrtPlateTop - thickOrtPlateTopRight / 2) +
                yOrtPlateTop * thickStringerTop * (numberOfTopStringersLeft + numberOfTopStringersRight) * (thick1Top + thick2Top + thick3Top
                - yOrtPlateTop / 2);
            double area = width3Top * thick3Top + width2Top * thick2Top + width1Top * thick1Top + widthOrtPlateTopLeft * thickOrtPlateTopLeft +
                widthOrtPlateTopRight * thickOrtPlateTopRight + yOrtPlateTop * thickStringerTop * (numberOfTopStringersLeft + numberOfTopStringersRight);
            yCentreTop = thick3Bottom + thick2Bottom + thick1Bottom + wallHeight + thick1Top + thick2Top + thick3Top - yMassCentre();
            yCentre = S0 / area;
            S = area * (yCentreTop - yCentre);
            return S;
        }

        public double yMassCentre()
        {
            double y;
            y = StaticMoment / Area;
            return y;
        }

        public double xMassCentre()
        {
            return 0;
        }

        public double momentOfInertionX() //!!!ПРОВЕРЕНО
        {
            double totalHeight = thick3Bottom + thick2Bottom + thick1Bottom + wallHeight + thick1Top + thick2Top + thick3Top;
            double y1 = YMassCentre;
            double y2 = totalHeight - y1;
            double I;


            I = width3Bottom * Math.Pow(thick3Bottom, 3) / 12 + width2Bottom * Math.Pow(thick2Bottom, 3) / 12 +
                width1Bottom * Math.Pow(thick1Bottom, 3) / 12 + wallThick * Math.Pow(wallHeight, 3) / 12 +
                width1Top * Math.Pow(thick1Top, 3) / 12 + width2Top * Math.Pow(thick2Top, 3) / 12 + width3Top * Math.Pow(thick3Top, 3) / 12 +
                widthOrtPlateTopLeft * Math.Pow(thickOrtPlateTopLeft, 3) /12 +
                widthOrtPlateTopRight * Math.Pow(thickOrtPlateTopRight, 3) / 12 +
                thickStringerTop * (numberOfTopStringersLeft + numberOfTopStringersRight) * Math.Pow(hStringerTop, 3) / 12 +
                widthOrtPlateBottomLeft * Math.Pow(thickOrtPlateBottomLeft, 3) / 12 +
                widthOrtPlateBottomRight * Math.Pow(thickOrtPlateBottomRight, 3) / 12 +
                thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight) * Math.Pow(hStringerBottom, 3) /12 +
                width3Bottom * thick3Bottom * Math.Pow((y1 - thick3Bottom / 2), 2) +
                width2Bottom * thick2Bottom * Math.Pow((y1 - thick3Bottom - thick2Bottom / 2), 2) +
                width1Bottom * thick1Bottom * Math.Pow((y1 - thick3Bottom - thick2Bottom - thick1Bottom / 2), 2) +
                wallHeight * wallThick * Math.Pow((y1 - thick3Bottom - thick2Bottom - thick1Bottom - wallHeight / 2), 2) +
                width1Top * thick1Top * Math.Pow((y2 - thick3Top - thick2Top - thick1Top / 2), 2) +
                width2Top * thick2Top * Math.Pow((y2 - thick3Top - thick2Top / 2), 2) +
                width3Top * thick3Top * Math.Pow((y2 - thick3Top / 2), 2) +
                widthOrtPlateTopLeft * thickOrtPlateTopLeft * Math.Pow((y2 - thick3Top - thick2Top - thick1Top + yOrtPlateTop + thickOrtPlateTopLeft / 2), 2) +
                widthOrtPlateTopRight * thickOrtPlateTopRight * Math.Pow((y2 - thick3Top - thick2Top - thick1Top + yOrtPlateTop + thickOrtPlateTopRight / 2), 2) +
                hStringerTop * thickStringerTop * (numberOfTopStringersLeft + numberOfTopStringersRight) * Math.Pow((y2 - thick3Top - thick2Top - thick1Top + yOrtPlateTop - hStringerTop / 2) , 2) +
                widthOrtPlateBottomLeft * thickOrtPlateBottomLeft * Math.Pow((y1 - thick3Bottom - thick2Bottom - thick1Bottom + yOrtPlateBottom + thickOrtPlateBottomLeft / 2) , 2) +
                widthOrtPlateBottomRight * thickOrtPlateBottomRight * Math.Pow((y1 - thick3Bottom - thick2Bottom - thick1Bottom + yOrtPlateBottom + thickOrtPlateBottomRight / 2), 2) +
                hStringerBottom * thickStringerBottom * (numberOfBottomStringersLeft + numberOfBottomStringersRight) * Math.Pow((y1 - thick3Bottom - thick2Bottom - thick1Bottom + yOrtPlateBottom - hStringerBottom / 2), 2);
            return I;
        }

        public double momentOfInertionY()
        {
            return 0;
        }
    }
}
