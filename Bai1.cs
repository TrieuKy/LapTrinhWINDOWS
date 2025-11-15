using System;
namespace GiaiPhuongTrinh
{
    public class GiaiPTBac1
    {
    protected double a;
    protected double b;
    public GiaiPTBac1(double a, double b)
    {
        this.a = a;
        this.b = b;
    }
    protected string GiaiBac1(double heSoA, double heSoB)
    {
        if (heSoA == 0)
        {
            if (heSoB == 0)
            {
                return "Phuong trinh vo so nghiem.";
            }
            else
            {
                return "Phuong trinh vo nghiem.";
            }
        }
        else
        {
            double x = -heSoB / heSoA;
            return $"Phuong trinh co 1 nghiem : x= {x}";
        }
    }
    public virtual string Giai() {
        return GiaiBac1(this.a, this.b);
    }
}
    public class GiaiPTBac2 : GiaiPTBac1
    {
        protected double c;
        public GiaiPTBac2(double a, double b, double c) : base(a, b)
        {
            this.c = c;
        }
        public override string Giai()
        {
            if (this.a == 0)
            {
                return GiaiBac1(this.b, this.c);
            }
            else
            {
                double delta = (this.b * this.b) - (4 * this.a * this.c);
                if (delta < 0)
                {
                    return "Phuong trinh vo nghiem.";
                }
                if (delta == 0)
                {
                    double x = -this.b / (2 * this.a);
                    return $"Phuong trinh co nghiem kep X1 = X2 = {x}";
                }
                else
                {
                    double x1 = (-this.b + Math.Sqrt(delta)) / (2 * this.a);
                    double x2 = (-this.b - Math.Sqrt(delta)) / (2 * this.a);
                    return $"Phuong trinh co 2 nghiem phan biet: \nX1= {x1}\nX2= {x2}";
                }
            }
        } 
    }
    class Bai1
    {
        static void Main(string[] args)
        {
            double a, b, c;
            Console.WriteLine("(ax^2 + bx + c = 0)");
            a = NhapHeSo("Nhap he so a: ");
            b = NhapHeSo("Nhap he so b: ");
            c = NhapHeSo("Nhap he so c: ");
            GiaiPTBac2 phuongTrinh = new GiaiPTBac2(a, b, c);
            string ketQua = phuongTrinh.Giai();

            Console.WriteLine(ketQua);

            Console.ReadKey(); 
        }
        static double NhapHeSo(string message)
        {
            double number;
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Du lieu khong hop le. Vui long nhap lai mot SO.");
                Console.Write(message);
            }
            return number;
        }
    }
} 
