using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialDeJohnDoe.Entidades
{
    public class Ortoedro
    {
        public int AristaA { get; set; }
        public int AristaB { get; set; }
        public int AristaC { get; set; }
        public Relleno Relleno { get; set; }

        public Ortoedro()
        {

        }

        public int Area()
        {
            return 2 * (AristaA * AristaB + AristaB * AristaC + AristaC * AristaA);
        }
        public int Volumen()
        {
            return AristaA * AristaB * AristaC;
        }

        public bool Validar()
        {
            bool valido = true;
            if ((AristaA == AristaB) && (AristaB == AristaC) && (AristaA == AristaC))
            {
                valido = false;
            }
            if (AristaA < 0 || AristaB < 0 || AristaC < 0)
            {
                valido = false;
            }
            return valido;
        }
    }
}
